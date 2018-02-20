using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class MultiParamTypeViewModel : ParamTypeViewModel
    {
        bool CanChange(int am) => TappedCell != null && (ChainChange || manager.ChangeRequest(TappedCell.lp, am) != null);
        void Change(int am)
        {
            int row = TappedCell.Level - 1;
            int col = Cells[row].IndexOf(TappedCell);
            if (!ChainChange) TryIncCell(am, col, row);
            else
                for (int i = row; i < t.Game.MaxLevel; i++)
                    TryIncCell(am, col, i);
            CanIncrease = CanChange(+CellChangeAmount);
            CanDecrease = CanChange(-CellChangeAmount);
        }
        void TryIncCell(int am, int c, int l)
        {
            var cel = Cells[l][c];
            var act = manager.ChangeRequest(cel.lp, am);
            if (act == null) return;
            act();
            cel.Amount += am;
        }
        public override void ViewAppearing()
        {
            base.ViewAppearing();
            TappedCell = null;
        }
        public MultiParamTypeViewModel(FakeDAL dal, BuildParameterType t, BuildDefinitionManager manager)
            : base(dal, t, manager)
        {
            // Edit defaults
            ChainChange = false;
            CellChangeAmount = 1;
            TappedCell = null;
            Increase = new MvxCommand(() => Change(+CellChangeAmount));
            Decrease = new MvxCommand(() => Change(-CellChangeAmount));

            // our params
            var oprms = dal.Get<BuildParameter>().Where(x => x.Type.id == t.id && x.Game.id == t.Game.id);

            // Set up cols, grouped
            IList<CHVM> grp = new List<CHVM>(), prm = new List<CHVM>();
            var gg = oprms.GroupBy(x => x.Category);
            foreach (var pg in gg)
            {
                bool frs = true;
                foreach (var p in pg)
                {
                    grp.Add(frs ? new CHVM { CSpan = pg.Count(), Name = pg.Key } : null);
                    prm.Add(new CHVM { CSpan = 1, Name = p.Name, Sub = true });
                    frs = false;
                }
            }
            CHeaders = grp.Where(x => x != null && x.Name != null).Any() ? new[] { grp, prm }.ToList() : new[] { prm }.ToList();
            // cells, same order as col headers!
            var tapCommand = new MvxCommand<LevelParamViewModel>(lp =>
            {
                if (TappedCell != null) TappedCell.IsSelected = false;
                lp.IsSelected = true;
                TappedCell = lp;
                CanIncrease = CanChange(+CellChangeAmount);
                CanDecrease = CanChange(-CellChangeAmount);
            });
            var plk = oprms.ToDictionary(x => x.Name);
            Cells = Enumerable.Range(1, t.Game.MaxLevel)
                               .Select(l =>
                                    prm.Select(x => new LevelParamViewModel(dal, plk[x.Name], l, manager.build, tapCommand ))
                                       .ToList() as IList<LevelParamViewModel>
                                )
                                .ToList();
        }

        public class CHVM
        {
            public bool Sub { get; set; }
            public String Name { get; set; }
            public int CSpan { get; set; }
        }

        // Cell commands 
        private LevelParamViewModel tappedCell;
        public LevelParamViewModel TappedCell { get => tappedCell; set => this.RaiseAndSetIfChanged(ref tappedCell, value); }
        public int CellChangeAmount { get; set; }
        private bool chainChange;
        public bool ChainChange { get => chainChange; set => this.RaiseAndSetIfChanged(ref chainChange, value); }
        public IMvxCommand Increase { get; set; }
        public IMvxCommand Decrease { get; set; }
        private bool canIncrease, canDecrease;
        public bool CanIncrease { get => canIncrease; set => this.RaiseAndSetIfChanged(ref canIncrease ,value); }
        public bool CanDecrease { get => canDecrease; set => this.RaiseAndSetIfChanged(ref canDecrease, value); }

        // Data
        public IList<IList<LevelParamViewModel>> Cells { get; set; }
        public IList<IList<CHVM>> CHeaders { get; set; }
    }
}
