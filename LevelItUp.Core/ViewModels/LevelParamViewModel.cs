using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class LevelParamViewModel : MvxViewModel
    {
        readonly BuildLevelParameter lp;
        readonly BuildDefinitionManager manager;
        public LevelParamViewModel(FakeDAL dal, BuildParameter p, int level, BuildDefinitionManager manager)
        {
            this.manager = manager;
            lp = dal.Get<BuildLevelParameter>()
                   .Single(x => x.Game.id == p.Game.id && x.Build.id == manager.build.id && x.Parameter.Type.id == p.Type.id && x.Parameter.id == p.id && x.Level == level);
            Amount = lp.Amount;
            AmountUpCommand = ChCommand(+1);
            AmountDownCommand = ChCommand(-1);
        }
        IMvxAsyncCommand ChCommand(int am)
        {
            return new MvxAsyncCommand
            (
                async () =>
                {
                    manager.ChangeRequest(lp, am)();
                    Amount+= am;
                    RaisePropertyChanged("Amount");
                },
                () => manager.ChangeRequest(lp, am) != null
            );
        }
        public int Amount { get; set; }
        public IMvxAsyncCommand AmountUpCommand { get; private set; }
        public IMvxAsyncCommand AmountDownCommand { get; private set; }
    }
}
