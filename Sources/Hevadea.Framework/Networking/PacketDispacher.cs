using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;

namespace Hevadea.Framework.Networking
{
    public class PacketDispacher<packetT> where packetT : struct, IConvertible
    {
        public delegate void PacketHandler(Socket socket, DataBuffer data);
        private Dictionary<int, PacketHandler> _handlers = new Dictionary<int, PacketHandler>();

        public PacketDispacher(Peer netPeer)
        {
            netPeer.DataReceived = OnDataRecived;   
        }

        private void OnDataRecived(Socket socket, DataBuffer packet)
        {
            var packetType = packet.ReadInteger();

            if (_handlers.ContainsKey(packetType))
            {
                _handlers[packetType]?.Invoke(socket, packet);
            }
        }

        private void RegisterHandler(packetT type, PacketHandler handler)
        {
            var packetId = type.ToInt32(CultureInfo.InvariantCulture);
            
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