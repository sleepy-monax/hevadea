using Hevadea.Framework.Networking;

namespace Hevadea.Game
{
    public partial class GameManager
    {
        public Server Server { get; set; }
        public bool IsServer => Client == null && Server != null;
        
        public void StartServer(int port = PORT)
        {
            if (IsRemote) return;
            Server = new Server();
            
            Dispacher = new PacketDispacher<PacketType>(Server);
            
            Server.BindSocket("127.0.0.1", PORT);
            Server.DataReceived = HandlePacket;
            Server.StartListening();
        }
        
        
    }
}