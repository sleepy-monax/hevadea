using Hevadea.Framework.Networking;

namespace Hevadea.Networking
{
    public enum PacketType
    {
		LOGIN, TOKEN, CHUNK, ENTITY_UPDATE, TILE_UPDATE, ACKNOWLEDGMENT
    }

    public static class PacketFactorie
    {
		/* --- Login and control flow --------------------------------------- */

        public static byte[] ConstructLogin(string playerName, string gameInfo)
            => ConstructLogin(playerName, 0, gameInfo);

		public static byte[] ConstructLogin(string playerName, int token, string gameInfo)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.LOGIN)
                .WriteStringASCII(playerName)
                .WriteInteger(token)
                .WriteStringASCII(gameInfo)
			    .GetBuffer();

		public static byte[] ConstructToken(int token)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.TOKEN)
                .WriteInteger(token)
			    .GetBuffer();

		public static byte[] ConstructAcknowledgment()
			=> new PacketBuilder()
			    .WriteInteger((int)PacketType.ACKNOWLEDGMENT).GetBuffer();

        /* --- Entity sync -------------------------------------------------- */

        /* --- Tile sync ---------------------------------------------------- */
        
    }
}