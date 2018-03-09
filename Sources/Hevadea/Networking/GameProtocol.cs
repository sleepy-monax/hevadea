namespace Hevadea.Networking
{
    public enum PacketType
    {
        EntityAdded, EntityRemove, EntityUpdate,
        
        TileUpdate
    }
    public abstract class GameProtocol
    {
        public string CreatePacket(PacketType type, string data)
        {
            return "";
        }

        public bool SendPacket(string packet)
        {
            return false;
        }
    }
}