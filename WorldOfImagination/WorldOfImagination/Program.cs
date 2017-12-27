using Maker.Rise;
using Maker.Rise.GameComponent;
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
            
            using (var game = new RiseGame())
            {
                game.OnLoad += GameLoad;
                game.Graphics.SetWidth(1280);
                game.Graphics.SetHeight(720);
                game.Run();
            }
        }

        private static void GameLoad(RiseGame sender, EventArgs e)
        {
            Ressources.Load(sender);
            //sender.Scene.Switch(new MainMenu(sender));
            sender.Scene.Switch(new GameScene(sender, World.Generate(0, sender)));
        }
    }
}
