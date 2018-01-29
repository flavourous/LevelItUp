using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelItUp.Model.Test.NetFW
{
    [TestFixture]
    public class PartialUnderspend : BaseTest
    {
        [Test]
        public void UnfinishedLevelOne()
        {
            AssertParamEqual("Strength", level: 1, value: 3);
            AssertLevelStats(); // all are none
            AssertParamChange("Strength", level: 1, change: +2, allowed: true);
            AssertParamEqual("Strength", level: 1, value: 5);
            AssertLevelStats
            (
                ("Attributes", new[] { (1, LevelStat.TooFewSpent) })
            );
        }
    }
}
