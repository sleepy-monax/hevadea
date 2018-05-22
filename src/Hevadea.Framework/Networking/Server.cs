using Hevadea.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public class ClientConnection
    {
		public Socket Socket { get; private set; }
		public Server Server { get; }

        public bool Connected => Socket.Connected();

		public ClientConnection( Socket socket, Server server)
        {
            Socket = socket;
            Server = server;
        }

        public void Send(byte[] packet)
        {
            Server.Send(Socket, packet);
        }

		public void Close()
		{
            Server.Connections.Remove(this);
			Socket.Dispose();
			Socket = null;
		}
    }

    public sealed class Server : Peer
    {
        public List<ClientConnection> Connections { get; private set; }

        public delegate void HandleConnectionChange(ClientConnection connection);
        public HandleConnectionChange ClientConnected;
        public HandleConnectionChange ClientLost;

        public Server(string ip, int port, bool noDelay = false) : base(noDelay)
        {
            Connections = new List<ClientConnection>();
            Socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
        }

        public void Start(int backLog = 25, int maximumConnections = 60)
        {
            var listenerThread = new Thread(() =>
            {


                Socket.Listen(backLog);

                Logger.Log<Server>("Listening on address: " + Socket.LocalEndPoint);
                
                while (true)
                {
                    Socket incomingSocket = Socket.Accept();
					incomingSocket.NoDelay = NoDelay;
                    var connection = new ClientConnection(incomingSocket, this);

                    Connections.Add(connection);


                    Logger.Log<Server>("Received a connection from: " + incomingSocket.RemoteEndPoint);

                    ClientConnected?.Invoke(connection);

                    try
                    {
						var connectionThread = new Thread(x => BeginReceiving(incomingSocket));
                        connectionThread.Name = incomingSocket.RemoteEndPoint + ": incoming data thread.";
                        connectionThread.Start();
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }
            })
            { Name = "NetServer Incoming Connection Thread" };

            listenerThread.Start();
        }


        public void Stop()
        {
            Connections.ForEach((c) => c.Close());
            Socket.Shutdown(SocketShutdown.Receive);
        }

        public void Broadcast(byte[] data)
        {
            Connections.ForEach((c) => c.Send(data));
        }

        public override void Disconnected(Socket socket)
        {
            var connection = Connections.Where((c) => c.Socket == socket).First();

            connection.Close();
            ClientLost?.Invoke(connection);
        }
    }
}