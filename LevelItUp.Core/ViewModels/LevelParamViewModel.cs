using LevelItUp.Model;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Linq;

namespace LevelItUp.Core.ViewModels
{
    public class LevelParamViewModel : MvxViewModel
    {
        public readonly BuildLevelParameter lp;
        public LevelParamViewModel(FakeDAL dal, BuildParameter p, int level, Build b, IMvxCommand<LevelParamViewModel> tap)
        {
            lp = dal.Get<BuildLevelParameter>()
                   .Single(x => x.Game.id == p.Game.id && x.Build.id == b.id && x.Parameter.Type.id == p.Type.id && x.Parameter.id == p.id && x.Level == level);
            Amount = lp.Amount;
            Tap = tap;
            Name = p.Name;
            Level = level;
        }
        public String Name { get; set; }
        public int Level { get; set; }
        public IMvxCommand<LevelParamViewModel> Tap { get; set; }

        private int amount;
        public int Amount { get => amount; set => this.RaiseAndSetIfChanged(ref amount, value); }

        private bool isSelected;
        public bool IsSelected { get => isSelected; set => this.RaiseAndSetIfChanged(ref isSelected, value); }
    }
}
