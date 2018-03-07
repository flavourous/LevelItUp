using System;
using LibXF.Controls.BindableLayout;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LevelItUp.Core.ViewModels;

namespace LevelItUp.XamarinForms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuildGridPage : ContentPage
	{
		public BuildGridPage ()
		{
            InitializeComponent ();
		}
        public void MultiRenderFailure(Exception e)
        {
            Device.BeginInvokeOnMainThread(() => throw e);
        }
    }

    public class UIDispatcher : ITimedDispatcher
    {
        readonly TimedDispatcher d = new TimedDispatcher(Device.BeginInvokeOnMainThread);
        public Task<Task> Add(Action t) => d.Add(t);
        public Task<Task<T>> Add<T>(Func<T> t) => d.Add(t);
        public void Dispose() => d.Dispose();
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