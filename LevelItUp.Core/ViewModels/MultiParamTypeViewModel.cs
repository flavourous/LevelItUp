using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class MultiParamTypeViewModel : ParamTypeViewModel
    {
        public MultiParamTypeViewModel(FakeDAL dal, BuildParameterType t, BuildDefinitionManager manager)
            : base(dal, t, manager)
        {
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
            var plk = oprms.ToDictionary(x => x.Name);
            Cells = Enumerable.Range(1, t.Game.MaxLevel)
                               .Select(l =>
                                    prm.Select(x => new LevelParamViewModel(dal, plk[x.Name], l, manager))
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
        

        public IList<IList<LevelParamViewModel>> Cells { get; set; }
        public IList<IList<CHVM>> CHeaders { get; set; }
    }
}
