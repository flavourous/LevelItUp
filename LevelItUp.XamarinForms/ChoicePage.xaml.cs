using LevelItUp.Core.ViewModels;
using MvvmCross.Forms.Views;
using System;
using Xamarin.Forms;

namespace LevelItUp.XamarinForms
{
    public partial class ChoicePage : MvxContentPage<ChoiceViewModel>
    {
        public ChoicePage()
        {
            InitializeComponent();
        }
        public void Deselect(Object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}
