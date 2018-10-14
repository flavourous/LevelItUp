using NUnit.Framework;
using System;
using System.Linq;

namespace LevelItUp.Model.Test.NetFW
{
    public class BaseTest
    {
        protected FakeDAL dal { get; private set; }
        protected GameBuild underrail { get; private set; }
        protected BuildDefinitionManager manager { get; private set; }
        public BaseTest()
        {
            dal = FakeDAL.OurFakeDal();
            underrail = d_Underrail.Generate(dal);
            var build = new Build { Name = "New Build", Game = underrail };
            dal.Save(build);
            manager = new BuildDefinitionManager(dal, underrail, build);
        }

        protected int OnLevel { get; set; } = 1;

        protected void AssertParamChEqul(BuildParameter param, int change, int value, bool allfollowing = true)
        {
            AssertParamChange(param, change);
            AssertParamEquals(param, value);
        }
        protected void AssertParamNeeded(BuildParameter param, BuildParameter need, int more, int? level = null)
        {
            var lv = level ?? OnLevel;
            manager.MissingRequirments(dal.Get(lv, param))
                   .Where(x => x.Any(y => y.param.id == need.id && y.amount == more))
                   .Any();
        }
        protected void AssertParamEquals(BuildParameter param, int value, int? level = null) => Assert.AreEqual(dal.Get(level ?? OnLevel, param).Amount, value);
        protected void AssertParamChange(BuildParameter param, int alter, bool allowed = true, int? level = null, bool allfollowing = true)
        {
            int start = level ?? OnLevel;
            for (int i = start; i <= param.Game.MaxLevel; i++)
            {
                var del = manager.ChangeRequest(dal.Get(i, param), alter);
                if (allowed)
                {
                    Assert.IsNotNull(del, "{0} {1} not allowed lvl {3}->{2}", param.Name, alter, i,start);
                    del();
                }
                else
                {
                    Assert.IsNull(del, "{0} {1} actually is allowed lvl {2}", param.Name, alter, start);
                    break; // only first tiem will be disapplows.
                }
            }
        }
        
        protected void AssertLevelStatus(BuildParameterType paramtype, LevelStat state)
        {
            AssertLevelStatus(OnLevel, (paramtype, state));
        }
        protected void AssertLevelStatus(params (BuildParameterType paramtype, LevelStat state)[] p)
        {
            AssertLevelStatus(OnLevel, p);
        }
        protected void AssertLevelStatus(int level, params (BuildParameterType paramtype, LevelStat state)[] p)
        {
            var avals = p.ToDictionary(x => x.paramtype.id, x => x.Item2);
            foreach (var kv in manager.LevelStatus(level))
            {
                var use = avals.ContainsKey(kv.Key.id) ? new[] { avals[kv.Key.id] } : new[] { LevelStat.Ok, LevelStat.None };
                Assert.IsTrue(use.Any(x => x == kv.Value), String.Format("{2} {0} at level {1}", kv.Key.Name, level, Spent(kv.Key, level)));
            }
        }
        int Spent(BuildParameterType p, int level)
        {
            return dal.Get<BuildLevelParameter>().Where(x => x.Level == level && x.Parameter.Type.id == p.id).Sum(x => x.Amount*x.Parameter.Cost);
        }
    }
}
