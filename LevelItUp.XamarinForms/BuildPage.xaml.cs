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

namespace LevelItUp.XamarinForms
{
    public partial class BuildPage : MvxTabbedPage<BuildViewModel>
    {
        public BuildPage()
        {
            InitializeComponent();    
        }
        public void MultiRenderFailure(Exception e)
        {
            Debugger.Break();
        }
    }
    public class UIDispatcher : ITimedDispatcher
    {
        readonly TimedDispatcher d = new TimedDispatcher(Device.BeginInvokeOnMainThread);
        public Task<Task> Add(Action t) => d.Add(t);
        public Task<Task<T>> Add<T>(Func<T> t) => d.Add(t);
        public void Dispose() => d.Dispose();
    }


    public class GapTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Gap { get; set; }
        public DataTemplate Normal { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item is LibXF.Controls.BindableLayout.Gap ? Gap : Normal;
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
    public class BoolConverter : IMvxValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class SelectedColorConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is bool b && b ? Color.FromHex("CCCCCC") : Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class cib : CellInfoBinder
    {

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ColumnSpan = 1;
            if (BindingContext is ParamTypeViewModel.RHVM)
            {
                Height = 30; 
                    Width = 50;
            }
            else if (BindingContext is MultiParamTypeViewModel.CHVM cv)
            {
                Width = cv.CSpan == 1 ? -1 : 0;
                Height = cv.CSpan == 1 ? -1 : -1;
                ColumnSpan = cv.CSpan;
            }
            else if (BindingContext is Gap)
            {
                Width = 50;
                Height = 0;
            }
            else Width = Height = 30;
        }
    }
}
