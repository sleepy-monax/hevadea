using Hevadea.Framework.Networking;

namespace Hevadea.Networking
{
    public enum PacketType
    {
        Login, LoginToken, Token, Chat, Disconnect,
        PlayerMove, PlayerAttack, PlayerInteract, PlayerDrop, PlayerPickup,
        RequestEntity, RequestTile,
        
         
        EntityMove, EntityUpdate, EntityRemove, EntityAdded
       
    }
    
    public static class PacketFactorie
    {
        public static DataBuffer ConstructLogin(string playerName, string gameInfo)
            => new DataBuffer()
                .WriteInteger((int)PacketType.LoginToken)
                .WriteStringASCII(playerName)
                .WriteStringASCII(gameInfo);
        
        public static DataBuffer ConstructLogin(string playerName, int token, string gameInfo)
            => new DataBuffer()
                .WriteInteger((int)PacketType.LoginToken)
                .WriteStringASCII(playerName)
                .WriteInteger(token)
                .WriteStringASCII(gameInfo);
        
        public static DataBuffer ConstructToken(int token) 
            => new DataBuffer()
                .WriteInteger((int)PacketType.Token)
                .WriteInteger(token);
        
        public static DataBuffer ConstructChat(string message)
            => new DataBuffer()
                .WriteInteger((int)PacketType.Chat)
                .WriteStringASCII(message);

        public static DataBuffer CustructDisconnect()
            => new DataBuffer()
                .WriteInteger((int) PacketType.Disconnect);
    }
}