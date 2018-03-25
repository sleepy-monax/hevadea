using Android.App;
using Android.Content.PM;
using Android.Content.Res;
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

            Rise.Initialize(new RiseAndroidPlatform(Resources, this));
            SetContentView((View)Rise.MonoGame.Services.GetService(typeof(View)));
            Rise.Start(new SceneGameSplash());
        }
    }
}

