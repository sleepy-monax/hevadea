using Maker.Rise;
using Maker.Rise.GameComponent;
using Maker.Rise.Utils;
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

                game.Graphics.SetWidth(Screen.GetWidth());
                game.Graphics.SetHeight(Screen.GetHeight());

                game.Window.IsBorderless = true;

                game.Run();
            }
        }

        private static void GameLoad(RiseGame sender, EventArgs e)
        {
            Ressources.Load(sender);
            sender.Scene.Switch(new MainMenu(sender));
            
        }
    }
}
