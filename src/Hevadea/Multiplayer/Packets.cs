using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;
using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.Multiplayer
{
    public enum PacketType
    {
        NULL, LOGIN, TOKEN, WORLD, LEVEL, CHUNK, ACKNOWLEDGMENT, JOINT, TILE, TILE_DATA
    }

    public static class Packets
    {

        // LOGIN ===============================================================
        public static byte[] Login(string playerName, string gameInfo)
            => Login(playerName, 0, gameInfo);

        public static byte[] Login(string playerName, int token, string gameInfo)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.LOGIN)
                .WriteStringUTF8(playerName)
                .WriteInteger(token)
                .WriteStringUTF8(gameInfo)
                .Buffer;

        public static byte[] Token(int token)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.TOKEN)
                .WriteInteger(token)
                .Buffer;

        // WORLD ===============================================================

        public static byte[] World(World world)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.WORLD)
                .WriteStringUTF8(world.Save().ToJson()).Buffer;

        public static byte[] Level(Level level)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.LEVEL)
                .WriteStringUTF8(level.Save().ToJson()).Buffer;

        public static byte[] Chunk(Chunk chunk)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.CHUNK)
                .WriteStringUTF8(chunk.Save().ToJson()).Buffer;

        // JOINT THE PLAYER ====================================================

        public static byte[] Join(Player player)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.JOINT)
                .WriteStringUTF8(player.Save().ToJson()).Buffer;


        // SYNC ================================================================

        public static byte[] Tile(Level level, TilePosition pos, Tile tile)
            => new PacketBuilder()
            .WriteInteger((int)PacketType.TILE)
            .WriteInteger(level.Id)
            .WriteInteger(pos.X)
            .WriteInteger(pos.Y)
            .WriteStringUTF8(tile.Name).Buffer;

        public static byte[] TileData(Level level, TilePosition pos, Dictionary<string, object> data)
            => new PacketBuilder()
            .WriteInteger((int)PacketType.TILE_DATA)
            .WriteInteger(level.Id)
            .WriteInteger(pos.X)
            .WriteInteger(pos.Y)
            .WriteStringUTF8(data.ToJson()).Buffer;
    }
}
