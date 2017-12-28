using Maker.Rise.Components;
using System;

namespace Maker.Rise.Utils
{
    public static class GameExtension
    {

        public static void SetTitle(this Microsoft.Xna.Framework.Game game, string title)
        {
            game.Window.Title = title;
            Console.Title = title;
        }
    }
}
