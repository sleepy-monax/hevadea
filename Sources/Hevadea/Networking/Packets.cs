using Hevadea.Framework.Networking;
using Hevadea.Game.Entities;
using Hevadea.Game.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Networking
{
    public abstract class GamePacket : Packet
    {
        public abstract void Decode();
    }

    public static class Packets
    {

        public static void RegisterPackets(NetPeer peer)
        {
            peer.RegisterPacket(new Login());
        }

        public class Login : GamePacket
        {
            public override int PacketID => 0;
            public string UserName { get; private set; }

            public Login() { }

            public Login(string userName)
            {
                DataBuffer.WriteString(userName);
            }

            public override void Decode()
            {
                DataBuffer.GoToBegin();
                UserName = DataBuffer.ReadString();
            }
        }

        public class EntityMove : GamePacket
        {
            public override int PacketID => 1;

            public override void Decode()
            {

            }
        }

        public class EntityUpdate
        {

        }

        public class EntityAdded
        {
            public EntityAdded(Entity entity, Level level)
            {

            }
        }

        public class EntityRemove
        {
            public EntityRemove(Entity entity)
            {

            }
        }

        public class TileUpdate { }

    }
}
