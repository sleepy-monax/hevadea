using Microsoft.Xna.Framework;
using System;
using WorldOfImagination.GameComponent;

namespace WorldOfImagination.Utils
{
    public static class GameExtension
    {

        public static void SetTitle(this Game game, string title)
        {
            game.Window.Title = title;
            Console.Title = title;
        }

        public static void SetFullScreen(this WorldOfImaginationGame game)
        {
            game.Graphics.SetFullScreen();
        }

    }
}
