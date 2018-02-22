using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.iOS;
using MvvmCross.Platform;
using System;
using UIKit;

namespace LevelItUp.Views.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppCenter.Start("f1388e2a-caa8-4130-b09a-94ad36ea0e87", typeof(Analytics), typeof(Crashes));
            Crashes.GenerateTestCrash();
            Console.WriteLine("OK I AM ACTUALLY RUNNING, APPCENTER IS " + Crashes.IsEnabledAsync().Result);

            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            LoadApplication(setup.FormsApplication);

            Window.MakeKeyAndVisible();

            return true;
        }
    }
}


