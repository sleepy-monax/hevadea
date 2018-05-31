using Hevadea.Framework.Threading;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.Multiplayer;
using Hevadea.WorldGenerator;

namespace Hevadea.Loading
{
    public static class Jobs
    {
        public static Job GenerateWorld => new Job("GenerateWorld", (job, args) => 
        {
            var path = args[0] as string;
            var seed = args[1] as int?;
            var generator = args[2] as Generator;

            Game.SetLastGame(path);
            job.Report("Generating world...");
            generator.Seed = seed ?? 0;

            Game game = new Game
            {
                SavePath = path,
                MainPlayer = (Player)EntityFactory.PLAYER.Construct(),
                World = generator.Generate(job)
            };

            return game;
        });

        public static Job SaveWorld => new Job("SaveWorld", (job, args) => 
        {
            var game = args[1] as Game;
            var savePath = args[0] as string;
            Game.SetLastGame(savePath ?? game.SavePath);
            game.Save(job, savePath ?? game.SavePath);

            return null;
        });

        public static Job LoadWorld => new Job("LoadWorld", (job, args) =>
        {
            var path = args[0] as string;
            Game game = new Game().Load(job, path);
            game.Initialize();

            return game;
        });

        public static Job StartServer => new Job("StartSever", (job, arg) =>
        {
            var worldPath = arg[0] as string;
            var address = arg[1] as string;
            var port = arg[2] as int?;
            var slots = arg[3] as int?;

            HostGame game = (HostGame)new HostGame(address ?? "127.0.0.1", port ?? 7777, slots ?? 8).Load(job, worldPath);
            game.Initialize();
            game.Start();

            return game;
        });

        public static Job ConnectToServer => new Job("ConnectToServer", (job, arg) => 
        {
            var address = arg[0] as string;
            var port = arg[1] as int?;

            var game = new RemoteGame(address, port ?? 7777);
            game.Connect();
            game.Initialize();
            return game;
        });



    }
}