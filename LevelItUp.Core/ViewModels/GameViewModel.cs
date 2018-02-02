using LevelItUp.Model;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class GameViewModel : MvxViewModel
    {
        readonly GameBuild game;
        readonly FakeDAL dal;
        private readonly IMvxNavigationService navigationService;
        public GameViewModel(IMvxNavigationService navigationService, FakeDAL dal, GameBuild game)
        {
            this.navigationService = navigationService;
            this.dal = dal;
            this.game = game;
            ViewBuildCommand = new MvxAsyncCommand(async () => await this.navigationService.Navigate(SelectedBuild.Value), () => SelectedBuild.Value != null);
            NewBuildCommand = new MvxAsyncCommand(async () =>
            {
                var build = new Build { Name = "New Build", Game = game };
                dal.Save(build);
                var bvm = new BuildViewModel(dal, build);
                Builds.Value.Add(bvm);
                await this.navigationService.Navigate(bvm);
            });
        }
        public INC<String> Name = new NC<String>();
        public INC<BuildViewModel> SelectedBuild = new NC<BuildViewModel>();
        public INCList<BuildViewModel> Builds = new NCList<BuildViewModel>();
        public IMvxAsyncCommand ViewBuildCommand { get; private set; }
        public IMvxAsyncCommand NewBuildCommand { get; private set; }
    }
}
