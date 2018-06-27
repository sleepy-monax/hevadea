using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Hevadea;
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
        , ScreenOrientation = ScreenOrientation.UserPortrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class HevadeaActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Rise.Initialize(new MobilePlatform(Resources, this));
            Rise.MonoGame.Graphics.IsFullScreen = true;

            SetContentView((View)Rise.MonoGame.Services.GetService(typeof(View)));

            Rise.Start(new SceneGameSplash(), () => { Rise.Config.Load(GamePaths.ConfigFile); });
        }
    }
}