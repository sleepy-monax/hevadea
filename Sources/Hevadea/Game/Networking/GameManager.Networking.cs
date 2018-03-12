using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;

namespace Hevadea.Game
{    
    public partial class GameManager
    {
        public const int PORT = 4225;

        public Client Client { get; set; }
        public Server Server { get; set; }

        public bool IsClient => Client != null && Server == null;
        public bool IsServer => Client == null && Server != null;
        public bool IsRemote => IsClient || IsServer;
        
        public bool Connect(string ip, int port = PORT)
        {
            if (IsServer) return false;
            
            Client = new Client();

            if (!Client.Connect(ip, port, 32))
            {
                Logger.Log<GameManager>(LoggerLevel.Error, $"Connection failed: {ip}:{port.ToString()}");
                return false;
            }

            Logger.Log<GameManager>(LoggerLevel.Fine, $"Connection Suceed: {ip}:{port.ToString()}");

            Client.DataReceived = HandlePacket;
            
            return true;
        }

        public void StartServer(int port = PORT)
        {
            if (IsRemote) return;
            Server = new Server();
            Server.BindSocket("127.0.0.1", PORT);
            Server.DataReceived = HandlePacket;
            Server.StartListening();
        }
    }
}
