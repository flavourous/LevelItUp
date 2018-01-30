﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            manager = new BuildDefinitionManager(dal, underrail);
        }
        protected void AssertParamEquals(BuildParameter param, int level, int value) => Assert.AreEqual(dal.Get(level, param.Name).Amount, value);
        protected void AssertParamChange(BuildParameter param, int level, int change, bool allowed)
        {
            var del = manager.ChangeRequest(dal.Get(level, param.Name), change);
            if(allowed)
            {
                Assert.IsNotNull(del);
                del();
            }
            else
            {
                Assert.IsNotNull(del);
            }
        }
        protected void AssertLevelStats(params (BuildParameterType paramtype, (int level, LevelStat state)[])[] vals)
        {
            var avals = vals.ToDictionary(x => x.paramtype.Name, x => x.Item2.ToDictionary(y=> y.level, y => y.state));
            foreach (var kv in manager.LevelStatus())
            {
                var kkn = kv.Key.Name;
                var ld = avals.ContainsKey(kkn) ? avals[kkn] : new Dictionary<int, LevelStat>();
                for (int i = 0; i < kv.Value.Length; i++)
                {
                    var use = ld.ContainsKey(i+1) ? ld[i+1] : LevelStat.None;
                    Assert.AreEqual(kv.Value[i], use, String.Format("{0} at level {1}", kkn, i+1));
                }
            }
        }
    }
}
