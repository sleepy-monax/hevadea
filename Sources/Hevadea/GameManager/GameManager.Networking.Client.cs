using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;
using Hevadea.Networking;

namespace Hevadea.GameManager
{
    public partial class GameManager
    {
        public Client Client { get; set; }
        public bool IsClient => Client != null && Server == null;
        
        public bool Connect(string ip, int port = PORT)
        {
            if (IsServer) return false;
            
            Client = new Client();
            Dispacher = new PacketDispacher<PacketType>(Client);

            if (!Client.Connect(ip, port, 32))
            {
                Logger.Log<GameManager>(LoggerLevel.Error, $"Connection failed: {ip}:{port.ToString()}");
                return false;
            }

            Logger.Log<GameManager>(LoggerLevel.Fine, $"Connection Suceed: {ip}:{port.ToString()}");

            
            return true;
        }
    }
}