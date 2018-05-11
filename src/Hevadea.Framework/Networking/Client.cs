using System;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public sealed class Client : Peer
    {
        private const int CONNECTION_TIMEOUT = 2000;
        private bool NoDelay { get; set; }

        public bool Connected => GetIsConnected(Socket);

        public delegate void ConnectionChangeHandler();

        public ConnectionChangeHandler ConnectionLost;

        public Client(bool noDelay = false) : base(noDelay)
        {
            NoDelay = noDelay;
        }

        public bool Connect(string ip, int port, byte attemptCount)
        {
            for (var i = 0; i < attemptCount; i++)
            {
                try
                {
                    Socket = new Socket(Socket.AddressFamily, Socket.SocketType, Socket.ProtocolType)
                    {
                        NoDelay = this.NoDelay
                    };

                    var result = Socket.BeginConnect(ip, port, null, null);

                    if (result.AsyncWaitHandle.WaitOne(CONNECTION_TIMEOUT, true) && Socket.Connected)
                    {
                        new Thread(x => BeginReceiving(Socket, 0)).Start();
                        Console.WriteLine("[NetClient] Connection established with " + ip + ":" + port);
                        Socket.EndConnect(result);
                        return true;
                    }
                }
                catch (SocketException) { }
                catch (InvalidOperationException) { }

                Socket.Close();
            }
            return false;
        }

        public void Disconnect()
        {
            Socket.Dispose();
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = this.NoDelay
            };
        }

        public void SendData(DataBuffer packet) => SendData(Socket, packet);
    }
}