using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;
using Hevadea.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game
{
    public partial class GameManager
    {
        public const int PORT = 4225;

        public NetClient Client { get; set; }
        public NetServer Server { get; set; }

        public bool IsRemote => Client != null;
        public bool IsMasterGame => Client == null && Server != null;

        public bool Connect(string ip, int port = PORT)
        {
            if (!IsMasterGame)
            {
                Client = new NetClient();
                Packets.RegisterPackets(Client);

                if (!Client.Connect(ip, port, 32))
                {
                    Logger.Log<GameManager>(LoggerLevel.Error, $"Connection failed: {ip}:{port.ToString()}");
                    return false;
                }

                Client.PacketRecived = ClientPacketHandler;
                Client.SendPacket(new Packets.Login("dummy"));

                Logger.Log<GameManager>(LoggerLevel.Fine, $"Connection Suceed: {ip}:{port.ToString()}");
                return true;
            }

            return false;
        }

        public void ClientPacketHandler(Socket socket, Packet packet)
        {

        }

        public void StartServer(int port = PORT)
        {
            if (!IsRemote)
            {
                Server = new NetServer();
                Packets.RegisterPackets(Server);
                Server.BindSocket("127.0.0.1", PORT);
                Server.Listen();
            }
        }

        public void ServerPacketHandler(Socket socket, Packet packet)
        {

        }
    }
}
