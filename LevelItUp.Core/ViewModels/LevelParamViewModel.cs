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
            Level.Value = level;
            lp = dal.Get<BuildLevelParameter>()
                   .Single(x => x.Game.id == p.Game.id && x.Parameter.Type.id == p.Type.id && x.Parameter.id == p.id);
            Amount.Value = lp.Amount;
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
                    Amount.Value += am;
                },
                () => manager.ChangeRequest(lp, am) != null
            );
        }
        public INC<int> Level = new NC<int>();
        public INC<int> Amount = new NC<int>();
        public IMvxAsyncCommand AmountUpCommand { get; private set; }
        public IMvxAsyncCommand AmountDownCommand { get; private set; }
    }
}
