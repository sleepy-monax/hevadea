using Hevadea.Framework.Networking;
using Hevadea.Framework.Threading;
using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Multiplayer;
using Hevadea.Storage;
using Hevadea.Worlds;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Hevadea
{
    public class RemoteGame : Game
    {
        public Client Client { get; private set; }
        private bool _jointed = false;
        public string _address;
        public int _port;

        public RemoteGame(string address, int port)
        {
            _address = address;
            _port = port;
        }

        public void Connect()
        {
            Client = new Client(true);
            var dispacher = new PacketDispacher<PacketType>(Client);

            dispacher.RegisterHandler(PacketType.WORLD, HandleWORLD);
            dispacher.RegisterHandler(PacketType.LEVEL, HandleLEVEL);
            dispacher.RegisterHandler(PacketType.CHUNK, HandleCHUNK);
            dispacher.RegisterHandler(PacketType.JOINT, HandleJOINT);
            dispacher.RegisterHandler(PacketType.TILE, HandleTILE);
            dispacher.RegisterHandler(PacketType.TILE_DATA, HandleTILEDATA);

            Client.Connect(_address, _port, 16);

            Client.Send(Packets.Login("testplayer", "{}"));


            new PacketBuilder(Client.Wait()).Ignore(sizeof(int)).ReadInteger(out var token);
            Logger.Log<Game>($"Recived token {token} from server.");


            while (!_jointed) ;
        }

        public void Disconnect()
        {
            Client.Disconnect();
        }

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

        public void HandleTILE(Socket socket, byte[] data)
        {
            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadInteger(out var levelId)
                .ReadInteger(out var x)
                .ReadInteger(out var y)
                .ReadStringUTF8(out var tileName);

            World.GetLevel(levelId).SetTile(x, y, TILES.GetTile(tileName));
        }

        public void HandleTILEDATA(Socket socket, byte[] data)
        {
            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadInteger(out var levelId)
                .ReadInteger(out var x)
                .ReadInteger(out var y)
                .ReadStringUTF8(out var tiledataJson);

            World.GetLevel(levelId).SetTileDataAt(x, y, tiledataJson.FromJson<Dictionary<string, object>>() ?? new Dictionary<string, object>());
        }
    }
}
