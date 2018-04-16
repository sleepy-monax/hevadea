using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Hevadea.Framework;
using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Entities.Blueprints.Legacy;
using Hevadea.Networking;

namespace Hevadea.GameManager
{
    public class ConnectedClient
    {
        private readonly Server _server;
        
        public int Index { get; }
        public string Name { get; set; }
        public int Token { get; set; }
        public EntityPlayer Entity;
        
        public bool IsConnected => _server.GetConnection(Index) != null;
        
        public ConnectedClient(int index, Server server)
        {
            Index = index;
            _server = server;
        }
    }
    
    public partial class GameManager
    {
        public Server Server { get; set; }
        public List<ConnectedClient> ConnectedClients = new List<ConnectedClient>();
        public bool IsServer => Client == null && Server != null;
        
        public void StartServer(int port = PORT)
        {
            if (IsRemote) return;
            Server = new Server();
            
            Dispacher = new PacketDispacher<PacketType>(Server);
            Dispacher.UnknowPacket += DispacherOnUnknowPacket;
            
            Dispacher.RegisterHandler(PacketType.Login, LoginHandler);
            Dispacher.RegisterHandler(PacketType.LoginToken, LoginWithTokenHandler);
            
            Server.BindSocket("127.0.0.1", PORT);
            Server.ClientConnected = ClientConnected;
            Server.StartListening();
        }

        public void LoginClient(ConnectedClient client, string name, int token)
        {
            client.Name = name;
            client.Token = token;
               
            Server.SendData(PacketFactorie.ConstructToken(token), client.Index);
        }
        
        public void DisconnectClient(ConnectedClient client)
        {
            Server.SendData(PacketFactorie.CustructDisconnect(), client.Index);
            Server.RemoveConnection(client.Index);
            ConnectedClients.Remove(client);
        }
        
        private void DispacherOnUnknowPacket(Socket socket, DataBuffer data)
        {
            data.Begin();
            data.ReadInteger(out var packetId);
            Logger.Log<GameManager>(LoggerLevel.Warning, $"No handler for packet '{(PacketType)packetId}' !");
        }
        
        private void LoginHandler(Socket socket, DataBuffer data)
        {
            var client = GetClient(Server.GetConnectionIndex(socket));
            var token = Rise.Rnd.Next();
            
            data.ReadString(out var name);
            LoginClient(client, name, token);
            Logger.Log<GameManager>($"player '{name}' joint the game.");
        }
        
        private void LoginWithTokenHandler(Socket socket, DataBuffer data)
        {
            var client = GetClient(Server.GetConnectionIndex(socket));
            
            data.ReadString(out var name).ReadInteger(out var token);
            LoginClient(client, name, token);
            Logger.Log<GameManager>($"player '{name}' joint the game with token {token}.");
        }

        public ConnectedClient GetClient(int index)
        {
            foreach (var c in ConnectedClients)
            {
                if (c.Index == index)
                {
                    return c;
                }
            }

            return null;
        }
        
        private void ClientConnected(int socketIndex)
        {
            ConnectedClients.Add(new ConnectedClient(socketIndex, Server));
            
            var connection = Server.GetConnection(socketIndex);
            var endpoint = connection.Socket.RemoteEndPoint as IPEndPoint;
            
            Logger.Log<GameManager>(LoggerLevel.Info, $"New connection from {endpoint.Address}:{endpoint.Port}.");
        }
    }
}