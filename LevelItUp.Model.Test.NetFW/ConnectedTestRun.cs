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
            OnLevel = 1;
            AssertParamEquals(a_str, value: 3);
            AssertLevelStatus();
            AssertParamChange(a_str, alter: +4, allowed: true);
            AssertParamEquals(a_str, value: 7);
            AssertLevelStatus(t_attributes, LevelStat.TooFewSpent);
        }
        [Test, Order(00200)]
        public void C00200__Level_1_Finished_Attributes()
        {
            AssertParamChange(a_agi, alter: +5, allowed: true);
            AssertParamEquals(a_agi, value: 8);
            AssertParamChange(a_wil, alter: +5, allowed: true);
            AssertParamEquals(a_wil, value: 8);
            AssertParamChange(a_dex, alter: +5, allowed: true);
            AssertParamEquals(a_dex, value: 8);
            AssertLevelStatus(t_attributes, LevelStat.Ok);
        }
        [Test, Order(00300)]
        public void C00300__Level_1_SkillMany_FeatMany()
        {
        
        }
    }
}
