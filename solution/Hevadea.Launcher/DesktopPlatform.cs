using Hevadea.Framework;
using Hevadea.Framework.Platform;
using OpenGLPlatform.Inspector;
using System;
using System.IO;
using System.Windows.Forms;

namespace OpenGLPlatform
{
    public class DesktopPlatform : PlatformBase
    {
        private void Window_TextInput(object sender, Microsoft.Xna.Framework.TextInputEventArgs e)
        {
            RaiseTextInput(e.Character, e.Key);
        }

        public override string GetPlatformName()
        {
            return "OpenGL";
        }

        public override int GetScreenWidth()
        {
            return Screen.PrimaryScreen.Bounds.Width;
        }

        public override int GetScreenHeight()
        {
            return Screen.PrimaryScreen.Bounds.Height;
        }

        public override string GetStorageFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games/Hevadea");
        }

        public override void Stop()
        {
            Rise.MonoGame.Exit();
        }

        public override void Inspect(object o)
        {
            new InspectorForm(o).Show();
        }

        public override void Initialize()
        {
            Rise.MonoGame.Window.TextInput += Window_TextInput;
            Rise.MonoGame.Window.Title = "Hevadea";

            try
            {
                Console.Title = "Hevadea";
            }
            catch
            {
            }
        }

        public override float GetScreenScaling()
        {
            return Rise.Graphic.GetHeight() / 768f;
        }

        public override void Update()
        {
        }
    }
}