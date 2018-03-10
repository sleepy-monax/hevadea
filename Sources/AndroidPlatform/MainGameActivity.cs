using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Hevadea.Framework;
using Hevadea.Scenes;
using Microsoft.Xna.Framework;

namespace AndroidPlatform
{
    [Activity(Label = "Hevadea"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.UserLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainGameActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var metrics = Resources.DisplayMetrics;
            var platform = new RiseAndroidPlatform(metrics.WidthPixels, metrics.HeightPixels);

            Rise.Initialize(platform);
            SetContentView((View)Rise.MonoGame.Services.GetService(typeof(View)));
            Rise.Start(new SceneGameSplash());
        }
    }
}

