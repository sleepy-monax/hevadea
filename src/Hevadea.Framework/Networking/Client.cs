using Hevadea.Framework.Utils;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public sealed class Client : Peer
    {
        private const int CONNECTION_TIMEOUT = 2000;

        public bool Connected => Socket.Connected();

        public delegate void ConnectionChangeHandler();

        public ConnectionChangeHandler ConnectionLost;

        public Client(bool noDelay = false) : base(noDelay)
        {
        }

        public bool Connect(string ip, int port, byte attemptCount)
        {
            for (var i = 0; i < attemptCount; i++)
            {
                try
                {
                    Socket = new Socket(Socket.AddressFamily, Socket.SocketType, Socket.ProtocolType);
                    Socket.NoDelay = NoDelay;


                    IAsyncResult result = Socket.BeginConnect(ip, port, null, null);

                    if (result.AsyncWaitHandle.WaitOne(CONNECTION_TIMEOUT, true) && Socket.Connected)
                    {
                        new Thread(x => BeginReceiving(Socket)).Start();
                        Logger.Log<Client>("Connection established with " + ip + ":" + port);
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
            Socket = null;
        }

        public void Send(byte[] data) => Send(Socket, data);

        public override void Disconnected(Socket socket)
        {
            ConnectionLost?.Invoke();

            Socket.Dispose();
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = base.NoDelay
            };
        }
    }
}