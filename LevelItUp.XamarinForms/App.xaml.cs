using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using MvvmCross.Forms.Platform;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace LevelItUp.XamarinForms
{
    public partial class App : MvxFormsApplication
    {
        public App()
        {
            InitializeComponent();
            AppCenter.Start
            (
                "ios=f1388e2a-caa8-4130-b09a-94ad36ea0e87;"
                + "uwp={Your UWP App secret here};"
                + "android={Your Android App secret here}",
                typeof(Analytics),
                typeof(Crashes)
            );
        }

        protected override void OnStart()
        {

            // Handle when your app starts
#if !DEBUG
#endif
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
