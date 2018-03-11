using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public abstract class NetPeer
    {
        private const int SOCKET_POLL_TIME = 1000;
        private const int PACKET_HEADER_LENGTH = 4;

        private readonly List<Packet> _registeredPackets;
        private readonly bool _noDelay;

        public delegate void PacketRecivedHandler(Socket socket, Packet packet);
        public PacketRecivedHandler PacketRecived;

        protected Socket MainSocket;

        protected NetPeer(bool noDelay = false)
        {
            // Create a new Generic List instance which will store our Registered Packets; assign this new instance to the variable _registeredPackets.
            _registeredPackets = new List<Packet>();

            // Invoke the method which will register our packets.
            RegisterPackets();

            // Create a new instance of the class Socket; assign this new instance to the variable _mainSocket.
            // We're using a InterNetwork, TCP, Stream Based connection.
            this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = noDelay
            };

            // Disable Nagle's Algo. depedending on the value of noDelay (default false).

            this._noDelay = noDelay;
        }

        protected bool GetIsConnected(Socket socket)
        {
            try
            {
                bool part1 = socket.Poll(SOCKET_POLL_TIME, SelectMode.SelectRead);
                bool part2 = socket.Poll(SOCKET_POLL_TIME, SelectMode.SelectWrite);
                bool part3 = (socket.Available == 0);
                if (part1 && part2 && part3)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }

        /// <summary>
        /// Registeres a packet for later use.
        /// </summary>
        /// <param name="packet">Packet to be registered.</param>
        public void RegisterPacket(Packet packet)
        {
            // Output the details of the packet that we're registering.
            Console.WriteLine("Registering packet: " + packet.PacketID);

            // Add this new packet instance to our registered packets list (_registeredPackets).
            _registeredPackets.Add(packet);
        }

        private void RegisterPackets()
        {
            var usedPacketIds = new List<int>();

            // Loop through the assembly that is using this library.
            // Find each Class that extends the abstract class Packet, and register the classes that we find.
            foreach (var packet in from type in Assembly.GetEntryAssembly().GetTypes() where type.IsSubclassOf(typeof(Packet)) select Activator.CreateInstance(type) as Packet)
            {
                if (usedPacketIds.Contains(packet.PacketID))
                    throw new Exception(packet.ToString() + " is using an existing packet identifier!");

                // Register the new packet.
                RegisterPacket(packet);

                usedPacketIds.Add(packet.PacketID);
            }

            usedPacketIds.Clear();
        }

        /// <summary>
        /// Returns a packet at the specified index.
        /// </summary>
        /// <param name="index">Index value at which the packet is stored.</param>
        /// <returns></returns>
        public Packet GetPacket(int index)
        {
            // If the index is greater than the amount of packets that we currently have, throw an error.
            if (index > _registeredPackets.Count) throw new Exception("Invalid Packet ID!");

            // Return the packet at the given index.
            return _registeredPackets[index];
        }

        protected void BeginReceiving(Socket socket, int socketIndex)
        {
            // Continue the attempts to receive data so long as the connection is open.
            while (this.GetIsConnected(socket))
            {
                try
                {
                    // Stores our packet header length.
                    int pLength = PACKET_HEADER_LENGTH;
                    // Stores the current amount of bytes that have been read.
                    int curRead = 0;
                    // Stores the bytes that we have read from the socket.
                    var data = new byte[pLength];

                    // Attempt to read from the socket.
                    curRead = socket.Receive(data, 0, pLength, SocketFlags.None);

                    // Read any remaining bytes.
                    while (curRead < pLength)
                        curRead += socket.Receive(data, curRead, pLength - curRead, SocketFlags.None);

                    // Set the current read to 0.
                    curRead = 0;
                    // Get the packet length (32 bit integer).
                    pLength = BitConverter.ToInt32(data, 0);
                    // Set the data (byte-buffer) to the size of the packet -- determined by pLength.
                    data = new byte[pLength];

                    // Attempt to read from the socket.
                    curRead = socket.Receive(data, 0, pLength, SocketFlags.None);

                    // Read any remaining bytes.
                    while (curRead < pLength)
                        curRead += socket.Receive(data, curRead, pLength - curRead, SocketFlags.None);

                    var dataBuffer = new DataBuffer();

                    // Fill our DataBuffer.
                    dataBuffer.FillBuffer(data);

                    // Get the unique packetID.
                    int packetID = dataBuffer.ReadInteger();

                    // Create a new Packet instance by finding the unique packet in our registered packets by using the packetID.

                    var packet = (from p in _registeredPackets
                                  where p.PacketID == packetID
                                  select p).FirstOrDefault();

                    // Create a new instance of Packet based on the registered packet that matched our unique packet id.
                    if (packet != null)
                    {
                        var execPacket = Activator.CreateInstance(packet.GetType()) as Packet;
                        // Fill the packet's DataBuffer.
                        execPacket.DataBuffer.FillBuffer(data);
                        // Set the DataBuffer's offset to the value of readOffset.
                        execPacket.DataBuffer.SetOffset(4);
                        execPacket.SocketIndex = socketIndex;

                        // Execute the packet.
                        if (PacketRecived != null)
                            PacketRecived.Invoke(socket, execPacket);
                    }
                    else
                    {
                        Console.WriteLine("Invalid packet with ID: " + packetID + " received.");
                        continue;
                    }
                }

                catch (ObjectDisposedException)
                {
                    break;
                }

                catch (SocketException)
                {
                    break;
                }
            }

            this.HandleDisconnectedSocket(socket);
        }

        protected void HandleDisconnectedSocket(Socket socket)
        {
            // If this is our client's incoming data listener,
            // we should just allow the socket to disconnect, and then notify the deriving class
            // that the socket has disconnected!
            if (this is NetClient)
            {
                // Create a new reference of this object and cast it to NettyClient.
                var nettyClient = this as NetClient;

                // Invoke the SocketDisconnected method.
                if (nettyClient.ConnectionLost != null)
                    nettyClient.ConnectionLost.Invoke();

                MainSocket.Dispose();
                MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    NoDelay = this._noDelay
                };

                // We've cleaned up everything here; there's no need to notify the end user.
                return;
            }

            // Create a new reference of this object and cast it to NetServer.
            var nettyServer = this as NetServer;

            int socketIndex = nettyServer.GetConnectionIndex(socket);

            if (socketIndex != -1)
                nettyServer.RemoveConnection(socketIndex);
        }

        protected void SendPacket(Socket socket, Packet packet)
        {
            try
            {
                var dataBuffer = new DataBuffer();
                dataBuffer.WriteInteger(packet.PacketID);
                dataBuffer.WriteBytes(packet.DataBuffer.ReadBytes());

                byte[] data = dataBuffer.ReadBytes();

                byte[] packetHeader = BitConverter.GetBytes(data.Length);

                int sent = socket.Send(packetHeader);

                while (sent < packetHeader.Length)
                    sent += socket.Send(packetHeader, sent, packetHeader.Length - sent, SocketFlags.None);

                sent = socket.Send(data);

                while (sent < data.Length)
                    sent += socket.Send(data, sent, data.Length - sent, SocketFlags.None);
            }
            catch (NullReferenceException) {}
            catch (SocketException) {}
            catch (ObjectDisposedException) {}
        }
    }
}