using System;
using System.Collections.Generic;
using LevelItUp.Model;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.Commands;

namespace LevelItUp.Core.ViewModels
{
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        public MainPageViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            var dal = FakeDAL.OurFakeDal();
            var underrail = d_Underrail.Generate(dal);
            Games.Add(new GameViewModel(navigationService, dal, underrail));
            ViewGameCommand = new MvxAsyncCommand<GameViewModel>(async g => await navigationService.Navigate(g));
            Test= "this was bound";
        }
        public String Test { get; set; }
        public IMvxAsyncCommand<GameViewModel> ViewGameCommand { get; set; }
        public IList<GameViewModel> Games { get; set; } = new MvxObservableCollection<GameViewModel>();
    }
}
