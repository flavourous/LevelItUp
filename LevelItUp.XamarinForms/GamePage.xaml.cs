using LevelItUp.Core.ViewModels;
using MvvmCross.Forms.Views;
using System;
using Xamarin.Forms;

namespace LevelItUp.XamarinForms
{
    public partial class GamePage : MvxContentPage<GameViewModel>
    {
        public GamePage()
        {
            InitializeComponent();
        }

        public void Deselect(Object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuildTabsPage());
        }
    }
}
