namespace Hevadea.Framework.Networking
{
    public abstract class Packet
    {
        private readonly DataBuffer _dataBuffer = new DataBuffer();

        public abstract int PacketID { get; }
        public int SocketIndex { get; internal set; }
        public DataBuffer DataBuffer { get { return _dataBuffer; } }
    }
}