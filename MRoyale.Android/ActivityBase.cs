#region Using Statements
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
#endregion

namespace MRoyale.Android
{
    [Activity(Label = "rhythm.Beats"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.UserLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class ActivityBase : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private GameBase _gameBase = new GameBase();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView((View)_gameBase.Services.GetService(typeof(View)));

            _gameBase.Run();
        }
    }
}

