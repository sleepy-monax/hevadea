using Hevadea.Scenes;
using Maker.Rise;
using System;
using Hevadea.Framework;

namespace OpenGLPlatform
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Rise.Initialize(new RiseOpenGLPlatform());
            Rise.Start(new SceneGameSplash());
            Environment.Exit(0);
        }
    }
}
