using Hevadea.Framework;
using Hevadea.Framework.Networking;
using Hevadea.Framework.Threading;
using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Scenes.Menus;
using Hevadea.Storage;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace Hevadea
{
    public class Game
    {
        public static readonly int Unit = 16;
        public static readonly string Name = "Hevadea";
        public static readonly string Version = "0.1.0";
        public static readonly int VersionNumber = 1;

        public static string GetSaveFolder()
        {
            return Rise.Platform.GetStorageFolder() + "/Saves/";
        }


        public bool IsClient => Client != null;
        public bool IsServer => Server != null;
        public bool IsLocal => Client == null && Server == null;
        public bool IsMaster => !IsLocal;

        public Client Client;
        public Server Server;

        Menu _currentMenu;
        LevelSpriteBatchPool _spriteBatchPool = new LevelSpriteBatchPool();

        public string SavePath { get; set; } = "./test/";

        public Camera Camera { get; set; }
        public Player MainPlayer { get; set; }
        public World World { get; set; }

        public List<Player> Players { get; } = new List<Player>();
        public PlayerInputHandler PlayerInput { get; set; }
        
        public Menu CurrentMenu { get => _currentMenu; set { CurrentMenuChange?.Invoke(_currentMenu, value); _currentMenu = value; } }

        public delegate void CurrentMenuChangeHandler(Menu oldMenu, Menu newMenu);
        public event CurrentMenuChangeHandler CurrentMenuChange;

        // --- Initialize, Update and Draw ---------------------------------- // 

        public void Initialize()
        {
            Logger.Log<Game>("Initializing...");
            World.Initialize(this);
            CurrentMenu = new MenuInGame(this);

            if (MainPlayer.Removed)
            {
                if (MainPlayer.X == 0f && MainPlayer.Y == 0f)
                    World.SpawnPlayer(MainPlayer);
                else
                    World.GetLevel(MainPlayer.LastLevel).AddEntity(MainPlayer);
            }

            PlayerInput = new PlayerInputHandler(MainPlayer);
            Camera = new Camera(MainPlayer);
            Camera.JumpToFocusEntity();
        }

        public void Draw(GameTime gameTime)
        {
            Camera.FocusEntity.Level.Draw(_spriteBatchPool, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            PlayerInput.Update(gameTime);

            World.DayNightCycle.UpdateTime(gameTime.ElapsedGameTime.TotalSeconds);
            MainPlayer.Level.Update(gameTime);

        }

        // --- Path generator ----------------------------------------------- // 

        public string GetSavePath()
            => $"{SavePath}/";

        public string GetLevelSavePath(Level level)
            => $"{SavePath}/{level.Name}/";

        public string GetLevelMinimapSavePath(Level level)
            => $"{SavePath}/{level.Name}/minimap.png";

        public string GetLevelMinimapDataPath(Level level)
            => $"{SavePath}/{level.Name}/minimap.json";

        // --- Multiplayer code --------------------------------------------- //

        public void Connect(string address, int port, ProgressRepporter progressRepporter)
        {
            Client = new Client(true);
            var dispacher = new PacketDispacher<PacketType>(Client);

            dispacher.RegisterHandler(PacketType.WORLD, HandleWORLD);
            dispacher.RegisterHandler(PacketType.LEVEL, HandleLEVEL);
            dispacher.RegisterHandler(PacketType.CHUNK, HandleCHUNK);
            dispacher.RegisterHandler(PacketType.JOINT, HandleJOINT);

            progressRepporter.RepportStatus($"Connecting to {address}:{port}...");

            Client.Connect(address, port, 16);

            progressRepporter.RepportStatus($"Login in...");
            Client.Send(ConstructLOGIN("testplayer", "{}"));


            new  PacketBuilder(Client.Wait()).Ignore(sizeof(int)).ReadInteger(out var token);
            Logger.Log<Game>($"Recived token {token} from server.");

            progressRepporter.RepportStatus($"Downloading world...");

            while (!_jointed) ;
        }

        public void StartServer(string address, int port, int slots)
        {
            if (IsLocal)
            {
                Server = new Server(address, port, true);
                var dispacher = new PacketDispacher<PacketType>(Server);
                dispacher.RegisterHandler(PacketType.LOGIN, HandleLOGIN);

                Server.Start(25, slots);
            }
        }

        // --- Construct and handle packets --------------------------------- // 

        public enum PacketType
        {
            NULL, LOGIN, TOKEN, WORLD, LEVEL, CHUNK, ACKNOWLEDGMENT, JOINT
        }

        // LOGIN ===============================================================
        public static byte[] ConstructLOGIN(string playerName, string gameInfo)
            => ConstructLOGIN(playerName, 0, gameInfo);

        public static byte[] ConstructLOGIN(string playerName, int token, string gameInfo)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.LOGIN)
                .WriteStringUTF8(playerName)
                .WriteInteger(token)
                .WriteStringUTF8(gameInfo)
                .Buffer;

        public static byte[] ConstructTOKEN(int token)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.TOKEN)
                .WriteInteger(token)
                .Buffer;

        public void HandleLOGIN(Socket socket, byte[] data )
        {
            var client = Server.GetClientFrom(socket);

            new PacketBuilder(data)
                .Ignore(sizeof(int))
                .ReadStringUTF8(out var userName)
                .ReadInteger(out var token)
                .ReadStringUTF8(out var gameInfo);

            Logger.Log<Game>($"User '{userName}' login with token '{token}'...");

            if (token == 0)
            {
                token = Rise.Rnd.NextInt();
                Logger.Log<Game>($"{userName}' token is now {token}!");
            }

            client.Send(ConstructTOKEN(token));
            SendWorld(client);

            var newPlayer = (Player)EntityFactory.PLAYER.Construct();
            World.SpawnPlayer(newPlayer);
            client.Send(ConstructJOIN(newPlayer));
        }

        // WORLD ===============================================================

        public byte[] ConstructWORLD()
            => new PacketBuilder()
                .WriteInteger((int)PacketType.WORLD)
                .WriteStringUTF8(World.Save().ToJson()).Buffer;

        public byte[] ConstructLEVEL(Level level)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.LEVEL)
                .WriteStringUTF8(level.Save().ToJson()).Buffer;

        public byte[] ConstructCHUNK(Chunk chunk)
            => new PacketBuilder()
                .WriteInteger((int)PacketType.CHUNK)
                .WriteStringUTF8(chunk.Save().ToJson()).Buffer;

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

        public void SendWorld(ConnectedClient client)
        {
            client.Send(ConstructWORLD());

            foreach (var l in World.Levels)
            {
                client.Send(ConstructLEVEL(l));

                foreach (var c in l.Chunks)
                {
                    var progress = (c.X * l.Chunks.GetLength(1) + c.Y) / (float)l.Chunks.Length;

                    Logger.Log<Game>($"Sending '{l.Name}' {(int)(progress * 100)}%");
                    client.Send(ConstructCHUNK(c));
                }
            }
        }

        // JOINT THE PLAYER ====================================================

        public byte[] ConstructJOIN(Player player)
        => new PacketBuilder()
            .WriteInteger((int)PacketType.JOINT)
            .WriteStringUTF8(player.Save().ToJson()).Buffer;

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

        // --- Save and load ------------------------------------------------ // 

        public static Game Load(string saveFolder, ProgressRepporter progressRepporter)
        {
            Game game = new Game();
            game.SavePath = saveFolder;

            progressRepporter.RepportStatus("Loading world...");

            string path = game.GetSavePath();

            WorldStorage worldStorage = File.ReadAllText(path + "world.json").FromJson<WorldStorage>();
            World world = World.Load(worldStorage);
            Entity player = EntityFactory.PLAYER.Construct().Load(File.ReadAllText(path + "player.json").FromJson<EntityStorage>());


            foreach (var levelName in worldStorage.Levels)
            {
                world.Levels.Add(LoadLevel(game, levelName, progressRepporter));
            }

            game.World = world;
            game.MainPlayer = (Player)player;

            return game;
        }

        public static Level LoadLevel(Game game, string levelName, ProgressRepporter progressRepporter )
        {
            string levelPath = $"{game.GetSavePath()}{levelName}/";
            Level level = Level.Load(File.ReadAllText(levelPath + "level.json").FromJson<LevelStorage>());

            progressRepporter.RepportStatus($"Loading level {level.Name}...");
            for (int x = 0; x < level.Chunks.GetLength(0); x++)
            {
                for (int y = 0; y < level.Chunks.GetLength(1); y++)
                {
                    level.Chunks[x, y] = Chunk.Load(File.ReadAllText(levelPath + $"r{x}-{y}.json").FromJson<ChunkStorage>());
                    progressRepporter.Report((x * level.Chunks.GetLength(1) + y) / (float)level.Chunks.Length);
                }
            }

            level.Minimap.Waypoints = File.ReadAllText(levelPath + "minimap.json").FromJson<List<MinimapWaypoint>>();

            var task = new AsyncTask(() =>
            {
                var fs = new FileStream(levelPath + "minimap.png", FileMode.Open);
                level.Minimap.Texture = Texture2D.FromStream(Rise.MonoGame.GraphicsDevice, fs);
                fs.Close();
            });

            Rise.AsyncTasks.Enqueue(task);

            while (!task.Done)
            {
                // XXX: Hack to fix the soft lock when loading the world.
                System.Threading.Thread.Sleep(10);
            }

            return level;
        }

        public void Save(string savePath, ProgressRepporter progressRepporter)
        {
            SavePath = savePath;

            progressRepporter.RepportStatus("Saving world...");

            var levelsName = new List<string>();

            Directory.CreateDirectory(SavePath);

            foreach (var level in World.Levels)
            {
                SaveLevel(level, progressRepporter);
            }


            File.WriteAllText(GetSavePath() + "world.json", World.Save().ToJson());
            File.WriteAllText(GetSavePath() + "player.json", MainPlayer.Save().ToJson());
        }

        private void SaveLevel(Level level, ProgressRepporter progressRepporter)
        {
            progressRepporter.RepportStatus($"Saving {level.Name}...");
            string path = GetLevelSavePath(level);
            Directory.CreateDirectory(path);

            File.WriteAllText(path + "level.json", level.Save().ToJson());

            foreach (var chunk in level.Chunks)
            {
                progressRepporter.Report((chunk.X * level.Chunks.GetLength(1) + chunk.Y) / (float)level.Chunks.Length);
                File.WriteAllText(path + $"r{chunk.X}-{chunk.Y}.json", chunk.Save().ToJson());
            }

            File.WriteAllText(path + "minimap.json", level.Minimap.Waypoints.ToJson());

            var task = new AsyncTask(() =>
            {
                var fs = new FileStream(path + "minimap.png", FileMode.OpenOrCreate);
                level.Minimap.Texture.SaveAsPng(fs, level.Width, level.Height);
                fs.Close();
            });

            progressRepporter.RepportStatus($"Saving {level.Name} minimap...");
            progressRepporter.Report(1f);
            Rise.AsyncTasks.Enqueue(task);

            while (!task.Done)
            {
                // XXX: Hack to fix the soft lock when saving the world.
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}