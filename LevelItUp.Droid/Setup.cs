using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Forms.Droid.Platform;
using MvvmCross.Forms.Platform;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Android.Runtime;
using System.Diagnostics;

namespace LevelItUp.Droid
{
    public class Setup : MvxFormsAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
        }

        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            Debug.Write(e.Exception);
        }

        protected override MvxFormsApplication CreateFormsApplication()
        {
            return new XamarinForms.App();
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            var b = base.GetViewModelAssemblies();
            return b.Concat(new[] { typeof(Core.ViewModels.MainPageViewModel).Assembly });
        }

        protected override IEnumerable<Assembly> GetViewAssemblies()
        {
            var b = base.GetViewAssemblies();
            return b.Concat(new[] { typeof(XamarinForms.MainPage).Assembly });
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
