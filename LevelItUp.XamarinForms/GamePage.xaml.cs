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
    }
}
