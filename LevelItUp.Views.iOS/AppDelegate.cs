using CoreGraphics;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.iOS;
using MvvmCross.Platform;
using System;
using System.Threading;
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
            SemaphoreSlim s = new SemaphoreSlim(0, 1);
            Crashes.FailedToSendErrorReport += (o, e) => s.Release();
            Crashes.SentErrorReport += (o, e) => s.Release();
            AppCenter.Start("f1388e2a-caa8-4130-b09a-94ad36ea0e87", typeof(Analytics), typeof(Crashes), typeof(Push));
            AppDomain.CurrentDomain.UnhandledException += (o, e) => Console.WriteLine("Exception Raised{0}----------------{0}{1}", Environment.NewLine, e.ExceptionObject);

            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            if (Crashes.HasCrashedInLastSessionAsync().Result)
            {
                var sc = new SplashController();
                Window.RootViewController = sc;
                Window.MakeKeyAndVisible();
                Task.Run(async () =>
                {
                    await s.WaitAsync(10000);
                    BeginInvokeOnMainThread(StartMvvMxForms);
                });
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

            Title = "Starting Up";
            View.BackgroundColor = UIColor.Purple;

            // keep the code the username UITextField
            load = new UILabel
            {
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White,
                Text = "Uploading crash data\n(._.)"
            };
            load.Font = UIFont.BoldSystemFontOfSize(24f);

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


