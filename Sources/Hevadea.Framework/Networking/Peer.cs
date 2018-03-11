using System;
using System.Net.Sockets;

namespace Hevadea.Framework.Networking
{
    public abstract class Peer
    {
        private const int SOCKET_POLL_TIME = 1000;
        private const int PACKET_HEADER_LENGTH = 4;

        private readonly bool _noDelay;
        protected Socket Socket;

        public delegate void DataReceivedHandler(Socket socket, DataBuffer packet);
        public DataReceivedHandler DataReceived;


        protected Peer(bool noDelay = false)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = noDelay
            };

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

        protected void BeginReceiving(Socket socket, int socketIndex)
        {
            // Continue the attempts to receive data so long as the connection is open.
            while (GetIsConnected(socket))
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

                    if (DataReceived != null)
                        DataReceived.Invoke(socket, dataBuffer);
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
            if (this is Client)
            {
                // Create a new reference of this object and cast it to NettyClient.
                var nettyClient = this as Client;

                // Invoke the SocketDisconnected method.
                if (nettyClient.ConnectionLost != null)
                    nettyClient.ConnectionLost.Invoke();

                Socket.Dispose();
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    NoDelay = this._noDelay
                };

                // We've cleaned up everything here; there's no need to notify the end user.
                return;
            }

            // Create a new reference of this object and cast it to NetServer.
            var nettyServer = this as Server;

            int socketIndex = nettyServer.GetConnectionIndex(socket);

            if (socketIndex != -1)
                nettyServer.RemoveConnection(socketIndex);
        }

        protected void SendData(Socket socket, DataBuffer packet)
        {
            try
            {
                var dataBuffer = new DataBuffer();
                dataBuffer.WriteBytes(packet.ReadBytes());

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