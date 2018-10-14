using LevelItUp.Core.ViewModels;
using MvvmCross.Forms.Views;
using System;
using Xamarin.Forms;

namespace LevelItUp.XamarinForms
{
    public partial class MainPage : MvxContentPage<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public void Deselect(Object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}
