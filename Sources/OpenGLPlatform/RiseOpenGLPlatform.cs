using Hevadea.Framework.Platform;

namespace OpenGLPlatform
{
    public class RiseOpenGLPlatform : IPlatform
    {
        public void Initialize() { }

        public string GetPlatformName() => "OpenGl";

        public int GetScreenWidth() => 1280;

        public int GetScreenHeight() => 720;

        public string GetStorageFolder()
        {
            return ".";
        }
    }
}
