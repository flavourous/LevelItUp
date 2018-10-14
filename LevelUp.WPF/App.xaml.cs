using MvvmCross.Core;
using MvvmCross.Forms.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace LevelUp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<MvxFormsWpfSetup<LevelItUp.Core.App, LevelItUp.XamarinForms.App>>();
        }
    }
}
