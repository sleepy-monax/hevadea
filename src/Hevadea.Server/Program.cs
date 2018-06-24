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
        private static void Main()
        {
            Rise.InitializeNoGraphic(new ServerPlatform());
            Directory.CreateDirectory(GamePaths.SavesFolder);

            Ressources.Load();
            REGISTRY.Initialize();

            Console.WriteLine("\n");

            while (true)
            {
                Console.WriteLine($"{Game.Title} Server v{Game.Version}\n");

                var saves = Directory.GetDirectories(GamePaths.SavesFolder);

                for (int i = 0; i < saves.Length; i++)
                {
                    Console.WriteLine($"{i}: {saves[i]}");
                }

                Console.WriteLine();

                Console.WriteLine("0-99: load world.");
                Console.WriteLine("   n: new world.");
                Console.WriteLine("   d: delete world.");

                Console.Write("\n> ");
                string input = Console.ReadLine();

                if (input.ToLower() == "n")
                {
                    Console.WriteLine();
                    Console.Write("World name: ");
                    var worldName = Console.ReadLine();

                    Console.Write("World seed: ");
                    var worldSeed = Console.ReadLine();

                    int seed = 0;

                    if (!int.TryParse(worldSeed, out seed))
                    {
                        seed = worldSeed.GetHashCode();
                    }

                    GameState gameState = (GameState)Jobs.GenerateWorld
                                          .SetArguments(new Jobs.WorldGeneratorInfo(GamePaths.SavesFolder + worldName, seed, GENERATOR.DEFAULT))
                                          .Start(false)
                                          .Result;
                    gameState.Initialize();

                    var repport = Job.NewEmpty("SaveWorld");
                    repport.StatusChanged += (sender, e) => { Console.WriteLine(e); };

                    Jobs.SaveWorld
                        .SetArguments(new Jobs.WorldSaveInfo(GamePaths.SavesFolder + worldName, gameState))
                        .Start(false);
                }
                else if (int.TryParse(input, out var levelindex))
                {
                }

                Console.Clear();
            }
        }
    }
}