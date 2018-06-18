using Hevadea.Framework.Threading;
using Hevadea.Framework;
using Hevadea.GameObjects.Entities;
using Hevadea.Multiplayer;
using Hevadea.Registry;
using Hevadea.WorldGenerator;

namespace Hevadea.Loading
{
    public static class Jobs
    {
        /* --- Arguments --------------------------------------------------- */

        public class ConnectToServerInfo : JobArguments
        {
            public int Port { get; }
            public string Address { get; }

            public ConnectToServerInfo(string address, int port)
            {
                Address = address;
                Port = port;
            }
        }

        public class StartServerInfo : WorldLoadInfo
        {
            public int Port { get; }
            public int Slots { get; }
            public string Address { get; }

            public StartServerInfo(string path, string address, int port, int slots) : base(path)
            {
                Address = address;
                Port = port;
                Slots = slots;
            }
        }

        public class WorldGeneratorInfo : JobArguments
        {
            public Generator Generator { get; }
            public int Seed { get; }
            public string Path { get; }

            public WorldGeneratorInfo(string path, int seed, Generator generator)
            {
                Generator = generator;
                Path = path;
                Seed = seed;
            }
        }

        public class WorldLoadInfo : JobArguments
        {
            public string Path { get; }

            public WorldLoadInfo(string path)
            {
                Path = path;
            }
        }

        public class WorldSaveInfo : JobArguments
        {
            public GameState GameState { get; }
            public string Path { get; }

            public WorldSaveInfo(string path, GameState gameState)
            {
                GameState = gameState;
                Path = path;
            }
        }

        /* --- Jobs -------------------------------------------------------- */

        public static Job ConnectToServer => new Job("ConnectToServer", (job, arg) =>
        {
            var info = arg as ConnectToServerInfo;

            var game = new RemoteGame(info.Address, info.Port);
            game.Connect();
            game.Initialize();

            return game;
        });

        public static Job GenerateWorld => new Job("GenerateWorld", (job, args) => 
        {
            var info = args as WorldGeneratorInfo;

            Game.SetLastGame(info.Path);
            job.Report("Generating world...");
            info.Generator.Seed = info.Seed;

            GameState gameState = new GameState
            {
                SavePath = info.Path,
                World = info.Generator.Generate(job)
            };

            var localPlayer = new PlayerSession($"player-{Rise.Rnd.NextInt()}", Rise.Rnd.NextInt(), (Player)ENTITIES.PLAYER.Construct());

            localPlayer.Join(gameState);
            gameState.LocalPlayer = localPlayer;

            return gameState;
        });

        public static Job LoadWorld => new Job("LoadWorld", (job, args) =>
        {
            var info = args as WorldLoadInfo;

            GameState gameState = new GameState().Load(job, info.Path);
            gameState.Initialize();

            return gameState;
        });

        public static Job SaveWorld => new Job("SaveWorld", (job, args) => 
        {
            var info = args as WorldSaveInfo;

            Game.SetLastGame(info.Path);
            info.GameState.Save(job, info.Path);

            return null;
        });

        public static Job StartServer => new Job("StartSever", (job, arg) => 
        {
            var info = arg as StartServerInfo;

            HostGame game = (HostGame)new HostGame(info.Address, info.Port, info.Slots).Load(job, info.Path);
            game.Initialize();
            game.Start();

            return game;
        });
    }
}