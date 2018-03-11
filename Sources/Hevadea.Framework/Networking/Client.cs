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
            for (int i = 0; i < attemptCount; i++)
            {
                try
                {

                    Socket = new Socket(Socket.AddressFamily, Socket.SocketType, Socket.ProtocolType)
                    {
                        NoDelay = this.NoDelay
                    };

                    var result = Socket.BeginConnect(ip, port, null, null);

                    if (result.AsyncWaitHandle.WaitOne(CONNECTION_TIMEOUT, true))
                    {
                        if (Socket.Connected)
                        {
                            new Thread(x => BeginReceiving(Socket, 0)).Start();
                            Console.WriteLine("[NetClient] Connection established with " + ip + ":" + port);
                            Socket.EndConnect(result);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Socket.Close();
                    }
                }
                catch (SocketException)
                {
                    Socket.Close();
                    continue;
                }
                catch (InvalidOperationException)
                {
                    Socket.Close();
                    continue;
                }
            }
            return false;
        }

        public void Disconnect()
        {
            var noDelay = Socket.NoDelay;
            Socket.Dispose();
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = noDelay
            };
        }

        public void SendData(DataBuffer packet) => this.SendData(Socket, packet);
    }
}