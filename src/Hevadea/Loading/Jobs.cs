using Hevadea.Framework.Threading;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Multiplayer;
using Hevadea.WorldGenerator;

namespace Hevadea.Loading
{
    public static class Jobs
    {
		public class WorldGeneratorInfo : JobArguments 
		{
			public string Path { get; }
			public int Seed { get; }
			public Generator Generator { get; }
   
			public WorldGeneratorInfo(string path, int seed, Generator generator)
            {
                Path = path;
                Seed = seed;
                Generator = generator;
            }         
		}

		public class WorldSaveInfo : JobArguments
		{
			public WorldSaveInfo(string path, Game game)
			{
				Path = path;
				Game = game;
			}

			public string Path { get; }
			public Game Game { get; }
		}

		public class WorldLoadInfo : JobArguments
        {
			public WorldLoadInfo(string path)
            {
                Path = path;
            }

            public string Path { get; }
        }

		public class StartServerInfo : WorldLoadInfo
		{
			public StartServerInfo(string path, string address, int port, int slots) : base(path)
			{
				Address = address;
				Port = port;
				Slots = slots;
			}

			public string Address { get; }
			public int Port { get; }
			public int Slots { get; }
		}

		public class ConnectToServerInfo : JobArguments 
		{
			public ConnectToServerInfo(string address, int port)
			{
				Address = address;
				Port = port;
			}

			public string Address { get; }
			public int Port { get; }

		}

        public static Job GenerateWorld => new Job("GenerateWorld", (job, args) => 
        {
			var info = args as WorldGeneratorInfo;

            Game.SetLastGame(info.Path);
            job.Report("Generating world...");
			info.Generator.Seed = info.Seed;

            Game game = new Game
            {
				SavePath = info.Path,
                MainPlayer = (Player)EntityFactory.PLAYER.Construct(),
				World = info.Generator.Generate(job)
            };

            return game;
        });

        public static Job SaveWorld => new Job("SaveWorld", (job, args) => 
        {
			var info = args as WorldSaveInfo; 
           
			Game.SetLastGame(info.Path);
			info.Game.Save(job, info.Path);

            return null;
        });

        public static Job LoadWorld => new Job("LoadWorld", (job, args) =>
        {
			var info = args as WorldLoadInfo;
            
			Game game = new Game().Load(job, info.Path);
            game.Initialize();

            return game;
        });

        public static Job StartServer => new Job("StartSever", (job, arg) =>
        {
			var info = arg as StartServerInfo;

			HostGame game = (HostGame)new HostGame(info.Address, info.Port, info.Slots).Load(job, info.Path);
            game.Initialize();
            game.Start();

            return game;
        });

        public static Job ConnectToServer => new Job("ConnectToServer", (job, arg) => 
        {
			var info = arg as ConnectToServerInfo;

			var game = new RemoteGame(info.Address, info.Port);
            game.Connect();
            game.Initialize();
            return game;
        });



    }
}