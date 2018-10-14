using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Forms.Core;

namespace LevelItUp.XamarinForms
{
    public partial class App : MvxFormsApplication
    {
        public static void StartAppCenter()
        {
            AppCenter.Start
            (
                "ios=f1388e2a-caa8-4130-b09a-94ad36ea0e87;"
                + "uwp={Your UWP App secret here};"
                + "android={Your Android App secret here}",
                typeof(Analytics),
                typeof(Crashes)
            );
        }

        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
