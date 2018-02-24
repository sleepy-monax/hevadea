using Hevadea.Scenes;
using Maker.Rise;
using System;

namespace Hevadea
{
    public static class Program
    {
        public static GameConfig Configuration = new GameConfig();
        
        [STAThread]
        private static void Main()
        {
            Engine.Initialize();
            Engine.Start(new SplashScene());
            Environment.Exit(0);
        }
    }
}