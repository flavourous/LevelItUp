using CoreGraphics;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.iOS;
using MvvmCross.Platform;
using System;
using System.Threading.Tasks;
using UIKit;

namespace LevelItUp.Views.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Console.WriteLine("OK I AM ACTUALLY RUNNING, APPCENTER IS " + Crashes.IsEnabledAsync().Result);
            //Crashes.NotifyUserConfirmation(UserConfirmation.AlwaysSend);
            //Crashes.ShouldAwaitUserConfirmation = () =>
            //{
            //    Console.WriteLine("Await confirmination asked");
            //    return false;
            //};
            //Crashes.ShouldProcessErrorReport = e =>
            //{
            //    Console.WriteLine("ashing to process " + e.Exception.ToString());
            //    return true;
            //};
            //Crashes.FailedToSendErrorReport += (e, d) => Console.WriteLine("FAiled to seond: " + d.Exception.ToString());
            //Crashes.SendingErrorReport += (x, e) => Console.WriteLine("sending...." + e.Report.ToString());
            //Crashes.SentErrorReport += (xme, e) => Console.WriteLine("SENTY! " + e.Report.ToString());
            //Crashes.GenerateTestCrash();
            //AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start("f1388e2a-caa8-4130-b09a-94ad36ea0e87", typeof(Analytics), typeof(Crashes));
            AppDomain.CurrentDomain.UnhandledException += (o, e) => Console.WriteLine("Exception Raised{0}----------------{0}{1}", Environment.NewLine, e.ExceptionObject);

            if (Crashes.HasCrashedInLastSessionAsync().Result)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(10000);
                    BeginInvokeOnMainThread(StartMvvMxForms);
                });

                Window = new UIWindow(UIScreen.MainScreen.Bounds);
                Window.RootViewController = new SplashController();
                Window.MakeKeyAndVisible();
            }
            else StartMvvMxForms();
            return true;
        }
        void StartMvvMxForms()
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            LoadApplication(setup.FormsApplication);

            Window.MakeKeyAndVisible();
        }
    }
    public class SplashController : UIViewController
    {
        UILabel load;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // keep the code the username UITextField
            load = new UILabel
            {
                BackgroundColor = UIColor.White,
                TextColor = UIColor.Black,
                Text = "Uploading crash data :("
            };

            View.AddSubview(load);
        }
        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            nfloat h = 31.0f;
            nfloat w = View.Bounds.Width;
            load.Frame = new CGRect(10, 114, w - 20, h);
        }
    }
}


