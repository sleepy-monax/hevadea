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
            Rise.Start(new SplashScene());
            Environment.Exit(0);
        }
    }
}
