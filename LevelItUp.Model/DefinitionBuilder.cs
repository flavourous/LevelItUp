using System;
using System.Collections.Generic;
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

            // Parameters
            public ParamContext Parameter(String name, int cost = 1) => new ParamContext(this, name, cost);
            public class ParamContext : IDisposable
            {
                readonly PTypeContext pc;
                public readonly BuildParameter param;
                public ParamContext(PTypeContext ptc, String name, int cost)
                {
                    pc = ptc;
                    param = new BuildParameter { Game = pc.pc.game, Name = name, Type = pc.ptype, Cost = cost };
                }
                public BuildParameter NoRequirments()
                {
                    Dispose();
                    return param;
                }
                public void Dispose() => pc.pc.dal.Save(param);
            }

            // LevelPoints
            public void LevelPoint(int level, int amount, int limit)
            {
                var pl = new BuildParameterTypeLevelPoints
                {
                    Game = pc.game,
                    Type = ptype,
                    Amount = amount,
                    Level = level,
                    Limit = limit,
                };
                pc.dal.Save(pl);
            }
        }
    }
}
