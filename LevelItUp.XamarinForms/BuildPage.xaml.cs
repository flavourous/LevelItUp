using LevelItUp.Core.ViewModels;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LevelItUp.XamarinForms
{
    public partial class BuildPage : MvxTabbedPage<BuildViewModel>
    {
        public BuildPage()
        {
            InitializeComponent();
        }
    }
    public class ParamTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Multi { get; set; }
        public DataTemplate Binary { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item is MultiParamTypeViewModel ? Multi : Binary;
        }
    }
}
