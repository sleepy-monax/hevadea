using Hevadea.Framework;
using Hevadea.Framework.Platform;
using System;

namespace OpenGLPlatform
{
    public class DesktopPlatform : PlatformBase
    {

        private void Window_TextInput(object sender, Microsoft.Xna.Framework.TextInputEventArgs e)
        {
            RaiseTextInput(e.Character, e.Key);
        }

        public override string GetPlatformName() => "OpenGl";

        public override int GetScreenWidth() => 1280;

        public override int GetScreenHeight() => 720;

        public override string GetStorageFolder()
        {
            return ".";
        }

        public override void Stop()
        {
            Rise.MonoGame.Exit();
        }

        public override void Initialize()
        {
            Rise.MonoGame.Window.TextInput += Window_TextInput;
            Rise.MonoGame.Window.Title = "Hevadea";
            Console.Title = "Hevadea";
        }

        public override void Update()
        {
            
        }
    }
}
