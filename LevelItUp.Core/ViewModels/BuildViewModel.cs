using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class BuildViewModel : MvxViewModel
    {
        BuildDefinitionManager manager;
        public BuildViewModel(FakeDAL dal, Build build)
        {
            id = build.id;
            Name.Value=build.Name;
            manager = new BuildDefinitionManager(dal, build.Game, build);
            Paramd.Value = dal.Get<BuildParameterType>()
                              .Where(x => x.Game.id == build.id)
                              .Select(x => new ParamTypeViewModel(dal, x, manager))
                              .ToList();
        }

        public INC<String> Name = new NC<String>();
        public INCList<ParamTypeViewModel> Paramd = new NCList<ParamTypeViewModel>();

        public int id { get; set; }
    }
}
