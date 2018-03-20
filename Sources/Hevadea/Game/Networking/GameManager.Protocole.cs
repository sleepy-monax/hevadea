using System.Net.Sockets;
using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;
using Hevadea.Game.Worlds;

namespace Hevadea.Game
{    
    public partial class GameManager
    {
        public void NotifyTileChange(Level level, int tx, int ty, int tileId)
        {
            SendPacket(new DataBuffer()
                .WriteInteger((int)PacketType.Tile)
                .WriteInteger(level.Id)
                .WriteInteger(tx)
                .WriteInteger(ty)
                .WriteInteger(tileId));   
        }

        public void NotifyEntityMove(Entity entity, float sx, float sy)
        {
            SendPacket(new DataBuffer());
        }
        
        public void SendPacket(DataBuffer data)
        {
            if (IsServer)
            {
                Server.BroadcastPacket(data);   
            } 
            else if (IsClient)
            {
                Client.SendData(data);
            }
        }
        
        public void HandlePacket(Socket socket, DataBuffer packet)
        {
            PacketType packetType = (PacketType)packet.ReadInteger();

            Logger.Log<GameManager>($"Packet recived {packet.GetBuffer().Length}bytes");
            
            switch (packetType)
            {
                case PacketType.Tile:
                    packet
                        .ReadInteger(out var levelId)
                        .ReadInteger(out var tx)
                        .ReadInteger(out var ty)
                        .ReadInteger(out var tileId);
                    
                    World.GetLevel(levelId).SetTile(tx, ty, tileId, false);

                    if (IsServer)
                    {
                        Server.BroadcastPacket(packet);
                    }
                    
                    break;
            }
        }
    }
}