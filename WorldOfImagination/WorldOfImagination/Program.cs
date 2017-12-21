using Maker.Rise;
using System;
using WorldOfImagination.Game;
using WorldOfImagination.Scenes;

namespace WorldOfImagination
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            
            using (var game = new WorldOfImaginationGame())
            {
                game.OnLoad += GameLoad;
                game.Run();
            }
        }

        private static void GameLoad(WorldOfImaginationGame sender, EventArgs e)
        {
            Ressources.Load(sender);
            sender.Scene.Switch(new SplashScene(sender));
        }
    }
}
