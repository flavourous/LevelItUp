using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace LevelItUp.Droid
{
    [Activity(
        Label = "LevelItUp"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnStart()
        {
            XamarinForms.App.StartAppCenter();
            base.OnStart();
        }

        protected override void TriggerFirstNavigate()
        {
            StartActivity(typeof(MainActivity));
            base.TriggerFirstNavigate();
        }
    }
}
