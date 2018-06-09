using System;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public static class NetHelper
    {
        public const int SOCKET_POLL_TIME = 1000;
        public const int PACKET_HEADER_LENGTH = 4;

        public static bool Connected(this Socket socket)
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

        public static byte[] Receive(this Socket socket, int size)
        {
            int ptr = 0;

            var data = new byte[size];

            ptr = socket.Receive(data, 0, size, SocketFlags.None);

            while (ptr < size)
                ptr += socket.Receive(data, ptr, size - ptr, SocketFlags.None);

            return data;
        }

        public static void Send(this Socket socket, byte[] data)
        {
            int sent = socket.Send(data);
            while (sent < data.Length)
                sent += socket.Send(data, sent, data.Length - sent, SocketFlags.None);
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

        protected void BeginReceiving(Socket socket)
        {
            while (socket.Connected())
            {
                try
                {
                    int dataLenght = BitConverter.ToInt32(socket.Receive(4), 0);
                    byte[] data = socket.Receive(dataLenght);

                    DataReceived?.Invoke(socket, data);
                }
                catch (ObjectDisposedException) { break; }
                catch (SocketException) { break; }
            }

            Disconnected(socket);
        }

        public abstract void Disconnected(Socket socket);

        public void Send(Socket socket, byte[] data)
        {
            try
            {
                byte[] packetHeader = BitConverter.GetBytes(data.Length);

                socket.Send(packetHeader);
                socket.Send(data);
            }
            catch (NullReferenceException) { }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
        }

        public byte[] Wait()
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