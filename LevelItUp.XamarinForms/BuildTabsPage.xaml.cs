using LevelItUp.Core.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;

namespace LevelItUp.XamarinForms
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    public partial class BuildTabsPage : MvxTabbedPage<BuildViewModel>
    {
        public BuildTabsPage()
        {
            InitializeComponent();    
        }
    }
}
