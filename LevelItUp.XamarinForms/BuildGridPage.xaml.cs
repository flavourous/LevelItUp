using System;
using LibXF.Controls.BindableLayout;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LevelItUp.Core.ViewModels;
using LibXF.Controls.BindableGrid;
using System.Collections;

namespace LevelItUp.XamarinForms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuildGridPage : ContentPage
	{
		public BuildGridPage ()
		{
            InitializeComponent ();
		}
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
    public class MCellInfo : ICellInfoManager
    {
        public double GetColumnmWidth(int col, IList<IList> src)
        {
            var fel = src?.FirstOrDefault()?.Cast<Object>()?.ElementAtOrDefault(col);
            if(fel is RHVM) return 50;
            else return 30;
        }

        public double GetRowHeight(int row, IList<IList> src)
        {
            var fel = src?.ElementAtOrDefault(row)?.Cast<Object>()?.FirstOrDefault();
            if(fel is RHVM) return 30;
            else if(fel is CHVM fc) return fc.Sub ? 120 : 30;
            else return 30;
        }

        public int GetColumnSpan(object cellData) => cellData is CHVM c ? c.CSpan : 1;

        public int GetRowSpan(object cellData) => 1;
    }
}