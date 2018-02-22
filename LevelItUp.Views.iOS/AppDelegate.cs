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
            Console.WriteLine("OK I AM ACTUALLY RUNNING, APPCENTER IS " + Crashes.IsEnabledAsync().Result);
            Crashes.NotifyUserConfirmation(UserConfirmation.AlwaysSend);
            Crashes.ShouldAwaitUserConfirmation = () =>
            {
                Console.WriteLine("Await confirmination asked");
                return false;
            };
            Crashes.ShouldProcessErrorReport = e =>
            {
                Console.WriteLine("ashing to process " + e.Exception.ToString());
                return true;
            };
            Crashes.FailedToSendErrorReport += (e, d) => Console.WriteLine("FAiled to seond: " + d.Exception.ToString());
            Crashes.SendingErrorReport += (x, e) => Console.WriteLine("sending...." + e.Report.ToString());
            Crashes.SentErrorReport += (xme, e) => Console.WriteLine("SENTY! " + e.Report.ToString());
            Crashes.GenerateTestCrash();
            AppCenter.Start("f1388e2a-caa8-4130-b09a-94ad36ea0e87", typeof(Analytics), typeof(Crashes));

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


