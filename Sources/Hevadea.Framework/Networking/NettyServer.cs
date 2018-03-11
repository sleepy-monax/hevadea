using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Hevadea.Framework.Networking
{
    public sealed class NetServer : NetPeer
    {
        public class Connection
        {
            private Socket _socket;
            private readonly NetServer _nettyServer;

            public Socket Socket { get => _socket; internal set { _socket = value; } }

            public bool Connected => _nettyServer.GetIsConnected(_socket);

            public Connection(Socket socket, NetServer nettyServer)
            {
                _socket = socket;
                _nettyServer = nettyServer;
            }

            public void SendPacket(Packet packet)
            {
                _nettyServer.SendPacket(packet, _socket);
            }
        }

        private int _socketPort;
        private IPEndPoint _socketAddress;
        private Connection[] _connections;

        public delegate void HandleConnectionChange(int socketIndex);

        public HandleConnectionChange ClientConnected;
        public HandleConnectionChange ClientLost;

        public NetServer(bool noDelay = false)
            : base(noDelay)
        {
        }

        /// <summary>
        /// Returns the socket at the specified index.
        /// </summary>
        /// <param name="index">Index value at which the socket is stored.</param>
        /// <returns></returns>
        public Connection GetConnection(int index)
        {
            if (_connections[index] == null || _connections[index].Socket == null)
            {
                throw new Exception("[NetServer] Invaid Socket Request!");
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

        /// <summary>
        /// Removes a connection at the specified index.
        /// </summary>
        /// <param name="index">Index (unique id) of the connected to be removed.</param>
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

                if (this.ClientLost != null)
                    this.ClientLost(index);
            }
            catch (NullReferenceException)
            {
            }
        }
        /// <summary>
        /// Sets the Main-Socket's address to the specified IP & Port.
        /// </summary>
        /// <param name="ip">IP Address that the socket will listen on.</param>
        /// <param name="port">Port value that the socket will listen on.</param>
        private void SetAddress(string ip, int port)
        {
            if (MainSocket.IsBound)
            {
                throw new Exception("[NetServer] The socket is already bound!");
            }

            _socketPort = port;
            _socketAddress = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        /// <summary>
        /// Halts the server.
        /// </summary>
        public void StopListening()
        {
            MainSocket.Shutdown(SocketShutdown.Receive);
        }

        /// <summary>
        /// Begins listening for and accepting socket connections.
        /// </summary>
        /// <param name="backLog">Maximum amount of pending connections</param>
        public void Listen(int backLog = 25, int maximumConnections = 60)
        {
            _connections = new Connection[maximumConnections];

            var listenerThread = new Thread(() =>
            {
                int index = -1;

                if (_socketAddress == null) throw new Exception("You must specifiy the socket address before calling the listen method!");
                if (!MainSocket.IsBound) throw new Exception("You must bind the socket before calling the listen method!");

                MainSocket.Listen(backLog);

                Console.WriteLine("[NetServer] Server listening on address: " + MainSocket.LocalEndPoint);

                while (true)
                {
                    var incomingSocket = MainSocket.Accept();

                    for (int i = 0; i < maximumConnections; i++)
                    {
                        if (_connections[i] == null)
                        {
                            _connections[i] = new Connection(incomingSocket, this)
                            {
                                Socket = { NoDelay = MainSocket.NoDelay }
                            };

                            index = i;
                            break;
                        }
                    }

                    Console.WriteLine("[NetServer] Received a connection from: " + incomingSocket.RemoteEndPoint);

                    if (ClientConnected != null) ClientConnected.Invoke(index);

                    try
                    {
                        Thread recThread = new Thread(x => BeginReceiving(incomingSocket, index));
                        recThread.Name = incomingSocket.RemoteEndPoint + ": incoming data thread.";
                        recThread.Start();
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }
            }) { Name = "NetServer Incoming Connection Thread" };

            listenerThread.Start();
        }

        /// <summary>
        /// Binds the socket to the specified address
        /// </summary>
        public void BindSocket(string ip, int port)
        {
            SetAddress(ip, port);
            MainSocket.Bind(_socketAddress);
        }

        /// <summary>
        /// Sends a packet to the designated remote socket connnection.
        /// </summary>
        /// <param name="packet">Object containing the packet's unique information</param>
        /// <param name="this.SocketIndex">Socket ID of the desired remote socket that the packet will be sent to.</param>
        /// <param name="forceSend">Force the current Message Buffer to be sent and flushed.</param>
        public void SendPacket(Packet packet, int socketIndex)
        {
            this.SendPacket(packet, this.GetConnection(socketIndex).Socket);
        }

        /// <summary>
        /// Sends a packet to every active connection.
        /// </summary>
        /// <param name="packet">Object containing the packet's unique information</param>
        public void BroadcastPacket(Packet packet)
        {
            foreach (var connection in _connections)
            {
                if (connection != null)
                    this.SendPacket(packet, connection.Socket);
            }
        }

        internal void SendPacket(Packet packet, Socket socket)
        {
            this.SendPacket(socket, packet);
        }
    }
}