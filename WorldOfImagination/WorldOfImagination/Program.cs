using Maker.Rise;
using Maker.Rise.Components;
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

            Engine.Initialize();
            Engine.Start(new SplashScene());
            Environment.Exit(0);
        }
    }
}
