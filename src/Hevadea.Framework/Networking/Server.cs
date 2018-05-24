using Hevadea.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public class ConnectedClient
    {
        public Socket Socket { get; private set; }
        public Server Server { get; }

        public bool Connected => Socket.Connected();

        public ConnectedClient( Socket socket, Server server)
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
            Server.Clients.Remove(this);
            Socket.Dispose();
            Socket = null;
        }
    }

    public sealed class Server : Peer
    {
        public List<ConnectedClient> Clients { get; private set; }

        public delegate void HandleConnectionChange(ConnectedClient client);
        public HandleConnectionChange ClientConnected;
        public HandleConnectionChange ClientLost;

        public Server(string ip, int port, bool noDelay = false) : base(noDelay)
        {
            Clients = new List<ConnectedClient>();
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
                    var connection = new ConnectedClient(incomingSocket, this);

                    Clients.Add(connection);


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

        public ConnectedClient GetClientFrom(Socket socket)
        {
            return Clients.Where((c) => c.Socket == socket).First();
        }

        public void Stop()
        {
            Clients.ForEach((c) => c.Close());
            Socket.Shutdown(SocketShutdown.Receive);
        }

        public void Broadcast(byte[] data)
        {
            Clients.ForEach((c) => c.Send(data));
        }

        public override void Disconnected(Socket socket)
        {
            var client = GetClientFrom(socket);

            if (client != null)
            {
                client.Close();
                ClientLost?.Invoke(client);
                Logger.Log<Server>($"Client disconnected!");
            }
        }
    }
}