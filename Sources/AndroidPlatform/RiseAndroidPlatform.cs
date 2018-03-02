using Hevadea.Framework.Platform;

namespace AndroidPlatform
{
    public class RiseAndroidPlatform : IPlatform
    {
        private int _screenWidth;
        private int _screenHeight;
        public RiseAndroidPlatform(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }


        public string GetPlatformName() => "Android";

        public int GetScreenWidth() => _screenWidth;
        public int GetScreenHeight() => _screenHeight;

        public void Initialize()
        {

        }

        public string GetStorageFolder()
        {
            return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        }
    }
}