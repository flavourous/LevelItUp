using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using LevelItUp.Model;

namespace LevelItUp.Core.ViewModels
{
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        public MainPageViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            ViewGameCommand = new MvxAsyncCommand(async () => await this.navigationService.Navigate(SelectedGame.Value), () => SelectedGame.Value != null);
            var dal = FakeDAL.OurFakeDal();
            var underrail = d_Underrail.Generate(dal);
            Games.Value.Add(new GameViewModel(navigationService, dal, underrail));
        }
        public INC<GameViewModel> SelectedGame = new NC<GameViewModel>();
        public INCList<GameViewModel> Games = new NCList<GameViewModel>(new List<GameViewModel>());
        public IMvxAsyncCommand ViewGameCommand { get; private set; }
    }
}
