using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelItUp.Model.Test.NetFW
{
    public static class TestDallHelp
    {
        public static BuildLevelParameter Get(this FakeDAL dal, int level, String name)
        {
            return dal.Get<BuildLevelParameter>().Single(x => x.Parameter.Name == name && x.Level == level);
        }
    }
    [TestFixture]
    public class Buildy
    {
        [Test]
        public void EasyBuild()
        {
            var dal = FakeDAL.OurFakeDal();
            var g = UnderrailDefinition.Generate(dal);
            BuildDefinitionManager bdm = new BuildDefinitionManager(dal, g);
            Assert.AreEqual(dal.Get(1, "Strength").Amount, 3);
            var sst =  bdm.ChangeRequest(dal.Get(1, "Strength"), 2);
            Assert.IsNotNull(sst);
            Assert.IsTrue(bdm.LevelStatus().SelectMany(x => x.Value).All(x => x == LevelStat.None));
            sst();
            Assert.AreEqual(dal.Get(1, "Strength").Amount, 5);
            foreach(var kv in bdm.LevelStatus())
            {
                if (kv.Key.Name == "Attributes")
                {
                    Assert.AreEqual(kv.Value[0], LevelStat.TooFewSpent);
                    Assert.IsTrue(kv.Value.Skip(1).All(x => x == LevelStat.None));
                }
                else Assert.IsTrue(kv.Value.All(x => x == LevelStat.None));
            }
        }
    }
}
