using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LevelItUp.Model.d_Underrail;

namespace LevelItUp.Model.Test.NetFW
{

    [TestFixture]
    public class ConnectedTestRun : BaseTest
    {
        [Test, Order(00100)]
        public void C00100__Level_1_Unfinished_Attributes()
        {
            AssertParamEquals(a_str, level: 1, value: 3);
            AssertLevelStats(); // all are none
            AssertParamChange(a_str, level: 1, change: +4, allowed: true);
            AssertParamEquals(a_str, level: 1, value: 7);
            AssertLevelStats
            (
                (t_attributes, new[] { (1, LevelStat.TooFewSpent) })
            );
        }
        [Test, Order(00200)]
        public void C00200__Level_1_Finished_Attributes()
        {
            AssertParamChange(a_agi, level: 1, change: +5, allowed: true);
            AssertParamEquals(a_agi, level: 1, value: 8);
            AssertParamChange(a_wil, level: 1, change: +5, allowed: true);
            AssertParamEquals(a_wil, level: 1, value: 8);
            AssertParamChange(a_dex, level: 1, change: +5, allowed: true);
            AssertParamEquals(a_dex, level: 1, value: 8);
            AssertLevelStats
            (
                (t_attributes, new[] { (1, LevelStat.Ok) })
            );
        }
        [Test, Order(00300)]
        public void C00300__Level_1_SkillMany_FeatMany()
        {
        
        }
    }
}
