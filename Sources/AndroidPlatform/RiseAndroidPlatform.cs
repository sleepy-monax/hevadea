using Hevadea.Framework.Platform;

namespace AndroidPlatform
{
    public class RiseAndroidPlatform : PlatformBase
    {
        private int _screenWidth;
        private int _screenHeight;
        public RiseAndroidPlatform(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }


        public override string GetPlatformName() => "Android";

        public override int GetScreenWidth() => _screenWidth;
        public override int GetScreenHeight() => _screenHeight;

        public override void Initialize()
        {

        }

        public override string GetStorageFolder()
        {
            return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        }

        public override void Update()
        {
            
        }
    }
}