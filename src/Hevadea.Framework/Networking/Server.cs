using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public sealed class Server : Peer
    {
        public class Connection
        {
            private readonly Server _server;

            public Socket Socket { get; internal set; }
            public bool Connected => _server.GetIsConnected(Socket);

            public Connection(Socket socket, Server nettyServer)
            {
                Socket = socket;
                _server = nettyServer;
            }

            public void SendData(DataBuffer packet)
            {
                _server.SendData(packet, Socket);
            }
        }

        private int _socketPort;
        private IPEndPoint _socketAddress;
        private Connection[] _connections;

        public delegate void HandleConnectionChange(int socketIndex);

        public HandleConnectionChange ClientConnected;
        public HandleConnectionChange ClientLost;

        public Server(bool noDelay = false)
            : base(noDelay)
        {
        }

        public Connection GetConnection(int index)
        {
            if (_connections[index] == null || _connections[index].Socket == null)
            {
                return null;
            }

            return _connections[index];
        }

        public Connection[] GetConnections()
        {
            return _connections;
        }

        public int GetConnectionIndex(Connection connection)
        {
            for (int i = 0; i < _connections.Length; i++)
            {
                if (_connections[i] == connection)
                {
                    return i;
                }
            }

            return -1;
        }

        public int GetConnectionIndex(Socket socket)
        {
            for (int i = 0; i < _connections.Length; i++)
            {
                if (_connections[i] == null) continue;

                if (_connections[i].Socket == socket)
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
                if (index >= _connections.Length || index < 0)
                    return;

                if (_connections[index] == null)
                    return;

                _connections[index].Socket.Dispose();

                _connections[index].Socket = null;
                _connections[index] = null;

                ClientLost?.Invoke(index);
            }
            catch (NullReferenceException)
            {
            }
        }

        private void SetAddress(string ip, int port)
        {
            if (Socket.IsBound)
            {
                throw new Exception("[NetServer] The socket is already bound!");
            }

            _socketPort = port;
            _socketAddress = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void StopListening()
        {
            Socket.Shutdown(SocketShutdown.Receive);
        }

        public void StartListening(int backLog = 25, int maximumConnections = 60)
        {
            _connections = new Connection[maximumConnections];

            var listenerThread = new Thread(() =>
            {
                int index = -1;

                if (_socketAddress == null) throw new Exception("You must specifiy the socket address before calling the listen method!");
                if (!Socket.IsBound) throw new Exception("You must bind the socket before calling the listen method!");

                Socket.Listen(backLog);

                Console.WriteLine("[NetServer] Server listening on address: " + Socket.LocalEndPoint);

                while (true)
                {
                    var incomingSocket = Socket.Accept();

                    for (var i = 0; i < maximumConnections; i++)
                    {
                        if (_connections[i] == null)
                        {
                            _connections[i] = new Connection(incomingSocket, this)
                            {
                                Socket = { NoDelay = Socket.NoDelay }
                            };

                            index = i;
                            break;
                        }
                    }

                    Console.WriteLine("[NetServer] Received a connection from: " + incomingSocket.RemoteEndPoint);

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

        public void BindSocket(string ip, int port)
        {
            SetAddress(ip, port);
            Socket.Bind(_socketAddress);
        }

        public void SendData(DataBuffer packet, int socketIndex)
        {
            SendData(packet, GetConnection(socketIndex).Socket);
        }

        public void BroadcastPacket(DataBuffer packet)
        {
            foreach (var connection in _connections)
            {
                if (connection != null)
                    SendData(packet, connection.Socket);
            }
        }

        internal void SendData(DataBuffer packet, Socket socket)
        {
            SendData(socket, packet);
        }
    }
}