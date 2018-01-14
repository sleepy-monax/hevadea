using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Scenes;
using Maker.Rise;
using System;

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