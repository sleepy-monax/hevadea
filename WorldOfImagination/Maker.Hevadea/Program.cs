using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Utils;
using System;
using Maker.Hevadea.Game;
using Maker.Hevadea.Scenes;

namespace Maker.Hevadea
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
