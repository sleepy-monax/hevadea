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

                    var dataBuffer = new DataBuffer(data);
                    DataReceived?.Invoke(socket, dataBuffer);
                }

                catch (ObjectDisposedException) { break; }
                catch (SocketException) { break; }
            }

            HandleDisconnectedSocket(socket);
        }

        protected void HandleDisconnectedSocket(Socket socket)
        {
            if (this is Client)
            {
                var client = this as Client;
                client.ConnectionLost?.Invoke();

                Socket.Dispose();
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    NoDelay = _noDelay
                };

                return;
            }

            // Create a new reference of this object and cast it to NetServer.
            var server = this as Server;

            int socketIndex = server.GetConnectionIndex(socket);

            if (socketIndex != -1)
                server.RemoveConnection(socketIndex);
        }

        protected void SendData(Socket socket, DataBuffer packet)
        {
            try
            {
                byte[] packetBody = packet.ReadBytes();
                byte[] packetHeader = BitConverter.GetBytes(packetBody.Length);

                SendRawData(socket, packetHeader);
                SendRawData(socket, packetBody);
            }
            catch (NullReferenceException) {}
            catch (SocketException) {}
            catch (ObjectDisposedException) {}
        }

        private void SendRawData(Socket socket, byte[] data)
        {
            int sent = socket.Send(data);
            while (sent < data.Length)
                sent += socket.Send(data, sent, data.Length - sent, SocketFlags.None);
        }
    }
}