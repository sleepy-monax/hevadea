using Hevadea.Framework;
using Hevadea.Scenes;
using System;

namespace OpenGLPlatform
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Rise.Initialize(new DesktopPlatform());
            Rise.Start(new SceneGameSplash());
            Environment.Exit(0);
        }
    }
}
