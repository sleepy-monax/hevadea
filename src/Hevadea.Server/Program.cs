using Hevadea.Framework;
using Hevadea.Framework.Threading;
using Hevadea.Loading;
using Hevadea.Registry;
using System;
using System.IO;

namespace Hevadea.Server
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Rise.InitializeNoGraphic(new ServerPlatform());
            Directory.CreateDirectory(Game.GetSaveFolder());

            Ressources.Load();
            REGISTRY.Initialize();

            Console.WriteLine("\n");

            while (true)
            {
                Console.WriteLine($"{Game.Name} Server v{Game.Version}\n");

                var saves = Directory.GetDirectories(Game.GetSaveFolder());

                for (int i = 0; i < saves.Length; i++)
                {
                    Console.WriteLine($"{i}: {saves[i]}");

                }

                Console.WriteLine("\nn: new world.");
                Console.WriteLine("d: delete world.");


                Console.Write("\n> ");
                string input = Console.ReadLine();

                if (input.ToLower() == "n")
                {
                    Console.WriteLine();
                    Console.Write("World name: ");
                    var worldName = Console.ReadLine();

                    Console.Write("World seed: ");
                    var worldSeed = Console.ReadLine();

                    var task = TaskFactorie.NewWorld(Game.GetSaveFolder() + worldName, GENERATOR.DEFAULT, int.Parse(worldSeed));
                    task.Start();

                    Game game = (Game)task.Result;
                    game.Initialize();
                    var repport = new ProgressRepporter();
                    repport.StatusChange += (sender, e) => { Console.WriteLine(e); };
                    game.Save(Game.GetSaveFolder() + worldName, repport);
                }

                Console.Clear();
            }
        }
    }
}
