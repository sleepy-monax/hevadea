using Hevadea.Framework.Networking;
using Hevadea.Networking;

namespace Hevadea.GameManager
{
    public partial class GameManager
    {
        public const int PORT = 4225;
        public PacketDispacher<PacketType> Dispacher { get; set; }
        public bool IsMasterGame => !IsClient;
        public bool IsRemote => IsClient || IsServer;
    }
}
