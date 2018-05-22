using System;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public static class NetHelper
    {
        public const int SOCKET_POLL_TIME = 1000;
        public const int PACKET_HEADER_LENGTH = 4;

        public static bool IsConnected(this Socket socket)
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
    }


    public abstract class Peer
    {


        public bool NoDelay { get; }

        protected Socket Socket;

        public delegate void DataReceivedHandler(Socket socket, byte[] data);
        public DataReceivedHandler DataReceived;

        protected Peer(bool noDelay = false)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = noDelay
            };

            this.NoDelay = noDelay;
        }


        protected void BeginReceiving(Socket socket, int socketIndex)
        {
            // Continue the attempts to receive data so long as the connection is open.
            while (socket.IsConnected())
            {
                try
                {
                    // Stores our packet header length.
                    int pLength = NetHelper.PACKET_HEADER_LENGTH;
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
                        
                    DataReceived?.Invoke(socket, data);
                }
                catch (ObjectDisposedException) { break; }
                catch (SocketException) { break; }
            }

            HandleDisconnectedSocket(socket);
        }

        public abstract void HandleDisconnectedSocket(Socket socket);


        protected void SendData(Socket socket, PacketBuilder data) => SendData(socket, data.GetBuffer());

        public void SendData(Socket socket, byte[] packet)
        {
            try
            {
                byte[] packetHeader = BitConverter.GetBytes(packet.Length);

                SendRawData(socket, packetHeader);
                SendRawData(socket, packet);
            }
            catch (NullReferenceException) { }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
        }

        private void SendRawData(Socket socket, byte[] data)
        {
            int sent = socket.Send(data);
            while (sent < data.Length)
                sent += socket.Send(data, sent, data.Length - sent, SocketFlags.None);
        }

		public byte[] WaitForData()
		{
			bool recived = false;
			byte[] recivedData = null;

			DataReceivedHandler eventHandler = (socket, data) => 
			{
				if (!recived)
				{            
                    recived = true;
                    recivedData = data;
				}
			};

			DataReceived += eventHandler;

			while (!recived)
			{
				Thread.Sleep(10);
			}

			DataReceived -= eventHandler;
            
			return recivedData;
		}
    }
}