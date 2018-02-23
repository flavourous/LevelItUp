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
            AppCenter.Start("f1388e2a-caa8-4130-b09a-94ad36ea0e87", typeof(Analytics), typeof(Crashes));
            AppDomain.CurrentDomain.UnhandledException += (o, e) => Console.WriteLine("Exception Raised{0}----------------{0}{1}", Environment.NewLine, e.ExceptionObject);

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var sc = new SplashController();
            Window.RootViewController = sc;
            Window.MakeKeyAndVisible();

            //if (Crashes.HasCrashedInLastSessionAsync().Result)
            //{
            //    Task.Run(async () =>
            //    {
            //        await Task.Delay(10000);
            //        BeginInvokeOnMainThread(StartMvvMxForms);
            //    });

            //}
            //else StartMvvMxForms();
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

            Title = "...Loading...";
            View.BackgroundColor = UIColor.Cyan;

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


