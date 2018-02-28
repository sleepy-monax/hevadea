using Hevadea.Scenes;
using Maker.Rise;
using System;

namespace OpenGLPlatform
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Engine.Initialize(new RiseOpenGLPlatform());
            Engine.Start(new SplashScene());
            Environment.Exit(0);
        }
    }
}
