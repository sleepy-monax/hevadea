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
        static void Main()
        {
            Rise.Initialize(new DesktopPlatform());
			Directory.CreateDirectory(Rise.Platform.GetStorageFolder());
            Rise.Start(new SceneGameSplash(), () => { Rise.Config.Load(GamePaths.ConfigFile); });
            Environment.Exit(0);
        }
    }
}