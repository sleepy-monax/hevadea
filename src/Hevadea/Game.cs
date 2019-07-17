using System;
using System.IO;
using Hevadea.Framework;
using Hevadea.Framework.Threading;
using Hevadea.Loading;
using Hevadea.Scenes;
using Hevadea.Scenes.MainMenu;
using Hevadea.WorldGenerator;

namespace Hevadea
{
    public static class Game
    {
        public static readonly int Unit = 16;
        public static readonly string Title = "Hevadea";
        public static readonly string SubTitle = "\"Tales of the unknown\"";
        public static readonly string Version = "0.2.0";
        public static readonly int VersionNumber = 2;

        public static void SetLastGame(string path)
        {
            try
            {
                File.WriteAllText(GamePaths.LastGameFile, path);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public static string GetLastGame()
        {
            return File.Exists(GamePaths.LastGameFile) 
                       ? File.ReadAllText(GamePaths.LastGameFile) 
                       : null;
        }

        public static void GoToTileScreen()
        {
            Rise.Scene.Switch(new TitleSplash());
        }

        public static void GoToMainMenu()
        {
            Rise.Scene.Switch(new DesktopMainMenu());
        }

        public static void Play(string gamePath)
        {
            var job = Jobs.LoadWorld.SetArguments(new Jobs.WorldLoadInfo(gamePath));

            job.Finish += (sender, e)
                => Rise.Scene.Switch(new SceneGameplay((GameState)((Job)sender).Result));

            Rise.Scene.Switch(new LoadingScene(job));
        }

        public static void New(string name, string seedString, Generator generator)
        {
            if (!int.TryParse(seedString, out int seed))
            {
                seed = seedString.GetHashCode();
            }

            New(name, seed, generator);
        }

        public static void New(string name, int seed, Generator generator)
        {
            var job = Jobs.GenerateWorld;
            job.SetArguments(new Jobs.WorldGeneratorInfo(Path.Combine(GamePaths.SavesFolder, $"{name}"), seed, generator));

            job.Finish += (sender, e) =>
            {
                GameState gameState = (GameState)((Job)sender).Result;
                ((Job)sender).Report("Initializing...");
                gameState.Initialize();
                Rise.Scene.Switch(new SceneGameplay(gameState));
            };
            Rise.Scene.Switch(new LoadingScene(job));
        }
    }
}