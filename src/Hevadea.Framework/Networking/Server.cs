using Hevadea.Framework.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public class ClientConnection
    {
        private readonly Server _server;

        public Socket Socket { get; internal set; }
        public bool Connected => Socket.IsConnected();

        public ClientConnection(Socket socket, Server nettyServer)
        {
            Socket = socket;
            _server = nettyServer;
        }

        public void SendData(byte[] packet)
        {
            _server.SendData(Socket, packet);
        }
    }

    public sealed class Server : Peer
    {
        public int Port { get; private set; }
        public ClientConnection[] Connections { get; private set; }

        private IPEndPoint _socketAddress;

        public delegate void HandleConnectionChange(int connectionIndex);
        public HandleConnectionChange ClientConnected;
        public HandleConnectionChange ClientLost;

        public Server(bool noDelay = false) : base(noDelay) {}

        public ClientConnection GetConnection(int index)
        {
            if (Connections[index] == null || Connections[index].Socket == null)
            {
                return null;
            }

            return Connections[index];
        }

        public int GetConnectionIndex(ClientConnection connection)
        {
            for (int i = 0; i < Connections.Length; i++)
            {
                if (Connections[i] == connection)
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetConnectionIndex(Socket socket)
        {
            for (int i = 0; i < Connections.Length; i++)
            {
                if (Connections[i] == null) continue;

                if (Connections[i].Socket == socket)
                {
                    return i;
                }
            }

            return -1;
        }

        public void RemoveConnection(int index)
        {
            try
            {
                if (index >= Connections.Length || index < 0)
                    return;

                if (Connections[index] == null)
                    return;

                Connections[index].Socket.Dispose();

                Connections[index].Socket = null;
                Connections[index] = null;

                ClientLost?.Invoke(index);
            }
            catch (NullReferenceException)
            {
            }
        }

        public void BindSocket(string ip, int port)
        {
            if (Socket.IsBound)
            {
                throw new Exception("The socket is already bound!");
            }

            Port = port;
            _socketAddress = new IPEndPoint(IPAddress.Parse(ip), port);

            Socket.Bind(_socketAddress);
        }

        public void StartListening(int backLog = 25, int maximumConnections = 60)
        {
            Connections = new ClientConnection[maximumConnections];

            var listenerThread = new Thread(() =>
            {
                int index = -1;

                if (_socketAddress == null) throw new Exception("You must specifiy the socket address before calling the listen method!");
                if (!Socket.IsBound) throw new Exception("You must bind the socket before calling the listen method!");

                Socket.Listen(backLog);

                Logger.Log<Server>("Listening on address: " + Socket.LocalEndPoint);

                while (true)
                {
                    var incomingSocket = Socket.Accept();

                    for (var i = 0; i < maximumConnections; i++)
                    {
                        if (Connections[i] == null)
                        {
                            Connections[i] = new ClientConnection(incomingSocket, this)
                            {
                                Socket = { NoDelay = Socket.NoDelay }
                            };

                            index = i;
                            break;
                        }
                    }

                    Logger.Log<Server>("Received a connection from: " + incomingSocket.RemoteEndPoint);

                    ClientConnected?.Invoke(index);

                    try
                    {
                        var recThread = new Thread(x => BeginReceiving(incomingSocket, index));
                        recThread.Name = incomingSocket.RemoteEndPoint + ": incoming data thread.";
                        recThread.Start();
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }
            })
            { Name = "NetServer Incoming Connection Thread" };

            listenerThread.Start();
        }


        public void StopListening()
        {
            Socket.Shutdown(SocketShutdown.Receive);
        }

        public void SendData(int socketIndex, byte[] data) => SendData(GetConnection(socketIndex).Socket, data);

        public void BroadcastData(byte[] data)
        {
            foreach (var connection in Connections)
            {
                if (connection != null)
                    SendData(connection.Socket, data);
            }
        }

        public override void HandleDisconnectedSocket(Socket socket)
        {
            int socketIndex = GetConnectionIndex(socket);

            if (socketIndex != -1)
                RemoveConnection(socketIndex);
        }
    }
}