using Hevadea.Framework;
using Hevadea.WorldGenerator;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects;
using System.IO;

namespace Hevadea.Loading
{
    public static class TaskFactorie
    {
		public static LoadingTask NewWorld(string path, Generator generator, int seed)
        {
			return new LoadingTask((tasks, reporter) =>
            {
                SetLastGame(path);
				reporter.RepportStatus("Generating world...");
                generator.Seed = seed;

				GameManager game = new GameManager
				{
					SavePath = path,
					MainPlayer = (EntityPlayer)EntityFactory.PLAYER.Construct(),
					World = generator.Generate(reporter)
				};

				tasks.Result = game;
            });
        }

		public static LoadingTask SaveWorld(GameManager game, string savePath = null)
		{
			return new LoadingTask((task, reporter) =>
			{
				SetLastGame(savePath ?? game.SavePath);
				game.Save(savePath ?? game.SavePath, reporter);
			});
		}

        public static LoadingTask LoadWorld(string path)
        {
			return new LoadingTask((task, reporter)=>
			{
				GameManager game = GameManager.Load(path, reporter);
				game.Initialize();
				task.Result = game;
			});
        }

		public static void SetLastGame(string path)
		{
			File.WriteAllText(Rise.Platform.GetStorageFolder() + "/.lastgame", path);
		}
    }
}