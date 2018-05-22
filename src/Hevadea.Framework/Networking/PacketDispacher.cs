using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;

namespace Hevadea.Framework.Networking
{
    public class PacketDispacher<packetT> where packetT : struct, IConvertible
    {
        public delegate void PacketHandler(Socket socket, PacketBuilder data);

        private Dictionary<int, PacketHandler> _handlers = new Dictionary<int, PacketHandler>();

        public event PacketHandler UnknowPacket;

        public PacketDispacher(Peer netPeer)
        {
            netPeer.DataReceived = OnDataRecived;
        }

        private void OnDataRecived(Socket socket, byte[] data)
        {
            PacketBuilder packet = new PacketBuilder(data);
            int packetType = packet.ReadInteger();

            if (_handlers.ContainsKey(packetType))
            {
                _handlers[packetType]?.Invoke(socket, packet);
            }
            else
            {
                UnknowPacket?.Invoke(socket, packet);
            }
        }

        public void RegisterHandler(packetT type, PacketHandler handler)
        {
            int packetId = type.ToInt32(CultureInfo.InvariantCulture);

            if (_handlers.ContainsKey(packetId))
            {
                _handlers[packetId] = handler;
            }
            else
            {
                _handlers.Add(packetId, handler);
            }
        }
    }
}