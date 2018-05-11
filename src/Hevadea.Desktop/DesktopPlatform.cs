using Hevadea.Framework;
using Hevadea.Framework.Platform;
using System;
using System.Windows.Forms;

namespace OpenGLPlatform
{
    public class DesktopPlatform : PlatformBase
    {

        private void Window_TextInput(object sender, Microsoft.Xna.Framework.TextInputEventArgs e)
        {
            RaiseTextInput(e.Character, e.Key);
        }

        public override string GetPlatformName() => "OpenGl";

        public override int GetScreenWidth() => Screen.PrimaryScreen.Bounds.Width;      
		public override int GetScreenHeight() => Screen.PrimaryScreen.Bounds.Height;

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
