using LevelItUp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LevelItUp.XamarinForms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuildBinaryPage : ContentPage
	{
		public BuildBinaryPage ()
		{
			InitializeComponent ();
		}
        public async void ITap(object sender, ItemTappedEventArgs tea)
        {
            var vm = tea.Item as BinaryLevelSkills;
            var bact = vm.Added.ToDictionary(pv => "Remvoe " + pv.Name, pv => pv.RemoveCommand);
            bact["Add"] = vm.AddCommand;

            var res = await DisplayActionSheet("What to do?", "Cancel", null, bact.Keys.ToArray());
            if (bact.ContainsKey(res)) await bact[res].ExecuteAsync();
        }
	}
}