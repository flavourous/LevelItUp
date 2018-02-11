using LevelItUp.Model;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelItUp.Core.ViewModels
{
    public class GameViewModel : MvxViewModel
    {
        readonly MvxObservableCollection<BuildViewModel> builds;
        readonly GameBuild game;
        readonly FakeDAL dal;
        private readonly IMvxNavigationService navigationService;
        public GameViewModel(IMvxNavigationService navigationService, FakeDAL dal, GameBuild game)
        {
            this.navigationService = navigationService;
            this.dal = dal;
            this.game = game;
            this.Name = game.Name;

            builds = new MvxObservableCollection<BuildViewModel>(dal.Get<Build>()
                              .Where(x => x.Game.id == game.id)
                              .Select(Create));
            Builds = builds;

            ViewBuildCommand = new MvxAsyncCommand<BuildViewModel>(async b => await navigationService.Navigate(b));
            NewBuildCommand = new MvxAsyncCommand(async () =>
            {
                Busy = true;
                RaisePropertyChanged("Busy");
                var bvm = await Task.Run(() =>
                {
                    var build = new Build { Name = "New Build", Game = game };
                    dal.Save(build);
                    return Create(build);
                });
                builds.Add(bvm);
                Busy = false;
                RaisePropertyChanged("Busy");
                await navigationService.Navigate(bvm);
            });

        }
        BuildViewModel Create(Build b)
        {
            var vm = new BuildViewModel(dal, b);
            vm.DeleteCommand = new MvxCommand(() =>
            {
                Busy = true;
                builds.Remove(vm);
                Busy = false;
                Task.Run(() => dal.Delete(b));
            });
            return vm;
        }
       
        public bool Busy { get; set; }
        public String Name { get; set; }
        public IList<BuildViewModel> Builds { get; set; }
        public IMvxCommand<BuildViewModel> ViewBuildCommand { get; private set; }
        public IMvxCommand NewBuildCommand { get; private set; }
        public IMvxCommand<BuildViewModel> DeleteCommand { get; private set; }
    }

    
}
