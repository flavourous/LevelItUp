using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Model
{

    class DefinitionBuilder : IDisposable
    {
        readonly FakeDAL dal;
        public readonly GameBuild game;
        public DefinitionBuilder(FakeDAL dal, String name, int maxLevel)
        {
            this.dal = dal;
            game = new GameBuild { Name = name, MaxLevel = maxLevel };
        }
        public void Dispose() => dal.Save(game);

        // Parameter Types
        public PTypeContext ParameterType(String name, int minimum = 0) => new PTypeContext(this, name, minimum);
        public class PTypeContext : IDisposable
        {
            readonly DefinitionBuilder pc;
            public readonly BuildParameterType ptype;
            public PTypeContext(DefinitionBuilder gc, String name, int minimum)
            {
                this.pc = gc;
                ptype = new BuildParameterType { Game = gc.game, Name = name, Minimum=minimum };
            }
            public void Dispose() => pc.dal.Save(ptype);

            // Set Subcategory
            public void SCategory(String name = null) => subcat = name;
            String subcat = null;

            // Parameters
            public ParamContext Parameter(String name, int cost = 1) => new ParamContext(this, name, subcat, cost);
            public class ParamContext : IDisposable
            {
                readonly PTypeContext pc;
                public readonly BuildParameter param;
                public ParamContext(PTypeContext ptc, String name, String subcat, int cost)
                {
                    pc = ptc;
                    param = new BuildParameter { Game = pc.pc.game, Name = name, Category = subcat, Type = pc.ptype, Cost = cost };
                }
                public BuildParameter Commit()
                {
                    Dispose();
                    return param;
                }
                List<BuildParameterRequiement> bpr = new List<BuildParameterRequiement>();
                public ParamContext OrRequire(int amount, BuildParameter ofthis)
                {
                    return Dor(amount, ofthis, false);
                }
                public ParamContext OrExclude(int amount, BuildParameter ofthis)
                {
                    return Dor(amount, ofthis, true);
                }
                public ParamContext Dor(int amount, BuildParameter ofthis, bool not)
                {
                    var g = bpr.Last();
                    Dep(amount, ofthis, g.DAmount, not);
                    bpr.Last().OrGroup = g.OrGroup;
                    return this;
                }
                public ParamContext Require(int amount, BuildParameter ofthis, int forthis = 1)
                {
                    return Dep(amount, ofthis, forthis, false);
                }
                public ParamContext Exclude(int amount, BuildParameter ofthis, int forthis = 1)
                {
                    return Dep(amount, ofthis, forthis, true);
                }
                public ParamContext Dep(int amount, BuildParameter ofthis, int forthis, bool not)
                {
                    bpr.Add(new BuildParameterRequiement
                    {
                        OrGroup = Guid.NewGuid().ToString(),
                        DAmount = forthis,
                        Depend = param,
                        On = ofthis,
                        Game = pc.pc.game,
                        OAmount = amount,
                        Not = not
                    });
                    return this;
                }
                public void Dispose()
                {
                    pc.pc.dal.Save(param);
                    foreach (var r in bpr)
                        pc.pc.dal.Save(r);
                }
            }

            // LevelPoints
            public void LevelPoints(int level, int amount, int limit)
            {
                var pl = new BuildParameterTypeLevelPoints
                {
                    Game = pc.game,
                    Type = ptype,
                    Limit = limit,
                    Amount = amount,
                    Level = level,
                };
                pc.dal.Save(pl);
            }
        }
    }
}
