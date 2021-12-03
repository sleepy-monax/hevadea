using Hevadea.Framework;
using Hevadea.Framework.Platform;
using System;
using System.IO;

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
            return 1920;
        }

        public override int GetScreenHeight()
        {
            return 1080;
        }

        public override string GetStorageFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games/Hevadea");
        }

        public override void Stop()
        {
        }

        public override void Initialize()
        {
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