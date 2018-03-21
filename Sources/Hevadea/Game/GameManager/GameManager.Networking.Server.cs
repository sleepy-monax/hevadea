using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Hevadea.Framework;
using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;

namespace Hevadea.Game
{
    public class ConnectedClient
    {
        public int Index { get; private set; }
        public string Name { get; set; }
        public int Token { get; set; }
        public bool IsConnected => _server.GetConnection(Index) != null;
        
        public EntityPlayer Entity;

        private Server _server;
        
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
            
            Server.BindSocket("127.0.0.1", PORT);
            Server.DataReceived = HandlePacket;
            Server.ClientConnected = ClientConnected;
            Server.StartListening();
        }

        public void DisconnectClient(ConnectedClient client)
        {
            Server.SendData(PacketFactorie.CustructDisconnect(), client.Index);
            Server.RemoveConnection(client.Index);
            ConnectedClients.Remove(client);
        }
        
        private void LoginHandler(Socket socket, DataBuffer data)
        {
            data.ReadString(out var name);
            var client = GetClient(Server.GetConnectionIndex(socket));
            
            // Generate client token
            var token = Rise.Random.Next();
            
            if (client != null)
            {
                client.Name = name;
                client.Token = token;
                
                
                Server.SendData(PacketFactorie.ConstructToken(token), client.Index);
            }
            else
            {
                Console.WriteLine("Uknow client");
            }
        }

        private void DispacherOnUnknowPacket(Socket socket, DataBuffer data)
        {
            data.Begin();
            data.ReadInteger(out var packetId);
            
            Logger.Log<GameManager>(LoggerLevel.Warning, $"Invalide packet id {packetId}!");
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
            
        }
    }
}