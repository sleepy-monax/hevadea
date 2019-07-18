using Hevadea;
using Hevadea.Framework;
using Hevadea.Scenes;
using System;
using System.IO;

namespace OpenGLPlatform
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            Rise.Initialize(new DesktopPlatform());
            Directory.CreateDirectory(Rise.Platform.GetStorageFolder());
            Rise.Start(new SceneLoadingScreen(), () => { Rise.Config.Load(Game.ConfigFile); });
            Environment.Exit(0);
        }
    }
}