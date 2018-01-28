using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Droid.Views;

namespace LevelItUp.Droid
{
    [Activity(
        Label = "LevelItUp.Droid"
        , Icon = "@drawable/icon"
        , Theme = "@style/MyTheme"
        , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        )]
    public class MainActivity : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
        }
    }
}
