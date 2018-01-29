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
}
