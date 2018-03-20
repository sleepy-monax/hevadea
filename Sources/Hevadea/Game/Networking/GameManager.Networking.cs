using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;

namespace Hevadea.Game
{    
    public enum PacketType
    {
        Login, World, Sync,
        EntityAdded, EntityRemove, EntityUpdate, EntityMove,
        Tile, TileUpdate, TileRequest
    }
    
    public partial class GameManager
    {
        public const int PORT = 4225;
        public PacketDispacher<PacketType> Dispacher { get; set; }
        public bool IsRemote => IsClient || IsServer;
    }
}
