using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Hevadea.Scenes;
using Maker.Rise;

namespace AndroidPlatform
{
    [Activity(Label = "Hevadea"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var metrics = Resources.DisplayMetrics;
            var platform = new RiseAndroidPlatform(metrics.WidthPixels, metrics.HeightPixels);

            Engine.Initialize(platform);
            SetContentView((View)Engine.MonoGameHandle.Services.GetService(typeof(View)));
            Engine.Start(new SplashScene());
        }
    }
}

