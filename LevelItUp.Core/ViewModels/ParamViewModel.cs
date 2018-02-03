using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class ParamViewModel : MvxViewModel
    {
        public ParamViewModel(FakeDAL dal, BuildParameter p, BuildDefinitionManager manager)
        {
            Name.Value = p.Name;
            Category.Value = p.Category;
            for (int i = 1; i <= p.Game.MaxLevel; i++)
                Levels.Value.Add(new LevelParamViewModel(dal, p, i, manager));
        }

        public INC<String> Name = new NC<String>();
        public INC<String> Category = new NC<String>();
        public INCList<LevelParamViewModel> Levels = new NCList<LevelParamViewModel>(new List<LevelParamViewModel>());
    }
}
