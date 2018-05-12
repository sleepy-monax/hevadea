using Hevadea.Framework.Networking;

namespace Hevadea.Networking
{
    public enum PacketType
    {
        LOGIN, TOKEN, CHUNK, ENTITY_UPDATE, TILE_UPDATE 
    }

    public static class PacketFactorie
    {
        public static PacketBuilder ConstructLogin(string playerName, string gameInfo)
            => ConstructLogin(playerName, 0, gameInfo);


        public static PacketBuilder ConstructLogin(string playerName, int token, string gameInfo)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.LOGIN)
                .WriteStringASCII(playerName)
                .WriteInteger(token)
                .WriteStringASCII(gameInfo);

        public static PacketBuilder ConstructToken(int token)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.TOKEN)
                .WriteInteger(token);
    }
}