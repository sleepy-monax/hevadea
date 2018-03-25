using Hevadea.Framework.Networking;

namespace Hevadea.Game
{
    public partial class GameManager
    {
        public const int PORT = 4225;
        public PacketDispacher<PacketType> Dispacher { get; set; }
        public bool IsMasterGame => !IsClient;
        public bool IsRemote => IsClient || IsServer;
    }
}
