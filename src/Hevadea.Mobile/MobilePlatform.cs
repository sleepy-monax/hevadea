using Android.Content.Res;
using Hevadea.Framework.Platform;

namespace AndroidPlatform
{
    public class MobilePlatform : PlatformBase
    {
        Resources _ress;
        HevadeaActivity _activity;

        public MobilePlatform(Resources resource, HevadeaActivity activity)
        {
            _ress = resource;
            _activity = activity;
            Family = PlatformFamily.Mobile;
        }


        public override string GetPlatformName() => "Android";

        public override int GetScreenWidth() => _ress.DisplayMetrics.WidthPixels;
        public override int GetScreenHeight() => _ress.DisplayMetrics.HeightPixels;

        public override void Initialize()
        {
        }

        public override void Update()
        {    
        }

        public override string GetStorageFolder()
        {
            return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        }

        public override void Stop()
        {
            _activity.Finish();
        }
    }
}