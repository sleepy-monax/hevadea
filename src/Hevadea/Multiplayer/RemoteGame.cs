using Hevadea.Framework.Networking;
using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Storage;
using Hevadea.Worlds;
using System.Net.Sockets;

namespace Hevadea
{
    public class RemoteGame : Game
    {
        public Client Client { get; private set; }


        public RemoteGame(string address, int port)
        {

        }


        public void Disconnect()
        {

        }

        private bool _jointed = false;

        public void HandleJOINT(Socket socket, byte[] data)
        {
            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadStringUTF8(out var playerJson);

            Player player = (Player)EntityFactory.PLAYER.Construct().Load(playerJson.FromJson<EntityStorage>());
            MainPlayer = player;
            _jointed = true;
        }

        public void HandleWORLD(Socket socket, byte[] data)
        {
            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadStringUTF8(out var worldJson);

            WorldStorage worldStorage = worldJson.FromJson<WorldStorage>();
            World = World.Load(worldStorage);
        }

        public void HandleLEVEL(Socket socket, byte[] data)
        {
            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadStringUTF8(out var levelJson);

            LevelStorage levelStorage = levelJson.FromJson<LevelStorage>();

            Logger.Log<Game>($"Loading {levelStorage.Name}...");
            Level level = Level.Load(levelStorage);

            World.Levels.Add(level);
        }

        public void HandleCHUNK(Socket socket, byte[] data)
        {
            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadStringUTF8(out var chunkJson);

            ChunkStorage chunkStorage = chunkJson.FromJson<ChunkStorage>();

            Logger.Log<Game>($"Loading chunk: {chunkStorage.Level}:{chunkStorage.X}-{chunkStorage.Y} ...");
            Chunk chunk = Chunk.Load(chunkStorage);

            World.GetLevel(chunkStorage.Level).Chunks[chunkStorage.X, chunkStorage.Y] = chunk;
        }
    }
}
