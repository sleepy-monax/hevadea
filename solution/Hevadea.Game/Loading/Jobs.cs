using Hevadea.Entities;
using Hevadea.Framework;
using Hevadea.Framework.Threading;
using Hevadea.Registry;
using Hevadea.WorldGenerator;

namespace Hevadea.Loading
{
    public static class Jobs
    {
        /* --- Arguments --------------------------------------------------- */

        public class ConnectToServerInfo : IJobArguments
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

        public class WorldGeneratorInfo : IJobArguments
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

        public class WorldLoadInfo : IJobArguments
        {
            public string Path { get; }

            public WorldLoadInfo(string path)
            {
                Path = path;
            }
        }

        public class WorldSaveInfo : IJobArguments
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

        public static Job GenerateWorld => new Job("GenerateWorld", (job, args) =>
        {
            var info = args as WorldGeneratorInfo;

            job.Report("Generating world...");
            info.Generator.Seed = info.Seed;

            var gameState = new GameState
            {
                SavePath = info.Path,
                World = info.Generator.Generate(job)
            };

            var localPlayer = new PlayerSession($"player-{Rise.Rnd.Next()}", Rise.Rnd.Next(),
                (Player) ENTITIES.PLAYER.Construct());

            localPlayer.Join(gameState);
            gameState.LocalPlayer = localPlayer;

            job.ThrowIfCanceled();

            return gameState;
        });

        public static Job LoadWorld => new Job("LoadWorld", (job, args) =>
        {
            var info = args as WorldLoadInfo;

            job.Log(LoggerLevel.Info, $"Loading world from '{info.Path}'...");
            var gameState = new GameState().Load(job, info.Path);
            gameState.Initialize();

            job.ThrowIfCanceled();

            return gameState;
        });

        public static Job SaveWorld => new Job("SaveWorld", (job, args) =>
        {
            var info = args as WorldSaveInfo;

            Game.SetLastGame(info.Path);
            info.GameState.Save(job, info.Path);

            job.ThrowIfCanceled();

            return null;
        });
    }
}