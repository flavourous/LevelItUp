using LevelItUp.Core.ViewModels;
using MvvmCross.Forms.Views;
using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using MvvmCross.Platform.UI;
using LibXF.Controls.BindableLayout;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;

namespace LevelItUp.XamarinForms
{
    public partial class BuildTabsPage : MvxTabbedPage<BuildViewModel>
    {
        public BuildTabsPage()
        {
            InitializeComponent();    
        }
        
    }

    
}
