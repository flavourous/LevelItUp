using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelItUp.Model.Test.NetFW
{
    public static class TestDallHelp
    {
        public static BuildLevelParameter Get(this FakeDAL dal, int level, BuildParameter param)
        {
            return dal.Get<BuildLevelParameter>().Single(x => x.Parameter.id == param.id && x.Level == level);
        }
    }
}
