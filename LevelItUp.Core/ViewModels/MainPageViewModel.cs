using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace LevelItUp.Core.ViewModels
{
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        public MainPageViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            ViewBuildCommand = new MvxAsyncCommand(async () => await this.navigationService.Navigate(SelectedBuild.Value));
        }
        public INC<BuildViewModel> SelectedBuild = new NC<BuildViewModel>();
        public INCList<BuildViewModel> Builds = new NCList<BuildViewModel>();
        public IMvxAsyncCommand ViewBuildCommand { get; private set; }
    }
}
