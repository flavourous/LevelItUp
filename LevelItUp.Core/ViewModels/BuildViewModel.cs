using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelItUp.Core.ViewModels
{
    public class BuildViewModel : MvxViewModel
    {
        readonly FakeDAL dal;
        public readonly Build build;
        readonly BuildDefinitionManager manager;
        public BuildViewModel(FakeDAL dal, Build build)
        {
            this.build = build;
            this.dal = dal;
            id = build.id;
            Name = build.Name;
            Game = build.Game.Name;
            manager = new BuildDefinitionManager(dal, build.Game, build);
            ParameterTypes = new MvxObservableCollection<ParamTypeViewModel>(dal.Get<BuildParameterType>()
                              .Where(x => x.Game.id == build.Game.id)
                              .Select(GetVM).Skip(3));
        }
        ParamTypeViewModel GetVM(BuildParameterType t)
        {
            var opts = dal.Get<BuildParameterTypeLevelPoints>().Where(x => x.Type.id == t.id && x.Game.id == t.Game.id);
            // Is it a yes/no parameter like a skill or abity choice?
            if (opts.All(x => x.Limit == 1) && t.Minimum == 0)
                return new BinaryParamTypeViewModel(dal, t, manager);
            else
                return new MultiParamTypeViewModel(dal, t, manager);
        }
        public String Name { get; set; }
        public String Game { get; set; }
        public IList<ParamTypeViewModel> ParameterTypes { get; set; }
        public IMvxCommand DeleteCommand { get; set; }
        public int id { get; set; }
    }
}
