using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.iOS;
using MvvmCross.Forms.Platform;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UIKit;

namespace LevelItUp.Views.iOS
{
    public class Setup : MvxFormsIosSetup
    {
        public Setup(IMvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        //protected override void PerformBootstrapActions()
        //{
        //    base.PerformBootstrapActions();

        //    PluginLoader.Instance.EnsureLoaded();
        //}

        
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

    public class DebugTrace : IMvxTrace
    {
        public void Trace(MvxTraceLevel level, string tag, Func<string> message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message());
        }

        public void Trace(MvxTraceLevel level, string tag, string message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message);
        }

        public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
        {
            try
            {
                Debug.WriteLine(tag + ":" + level + ":" + message, args);
            }
            catch (FormatException)
            {
                Trace(MvxTraceLevel.Error, tag, "Exception during trace of {0} {1}", level, message);
            }
        }
    }
}
