using LevelItUp.Model;  
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public abstract class ParamTypeViewModel : MvxViewModel
    {
        protected readonly FakeDAL dal;
        protected readonly BuildDefinitionManager manager;
        protected readonly BuildParameterType t;
        public ParamTypeViewModel(FakeDAL dal, BuildParameterType t, BuildDefinitionManager manager)
        {
            this.dal = dal;
            this.manager = manager;
            this.t = t;
            Name = t.Name;

            // Row Headers
            RHeaders = Enumerable.Range(1, t.Game.MaxLevel)
                                 .Select(x => new[] { new RHVM { Level = x, Status = EvaluateLevelStats(x) } }.ToList() as IList<RHVM>)
                                 .ToList();
            manager.ParameterChanged += EvaluateStatsAroundLevel;
        }

        public String Name { get; set; }
        public int id { get { return t.id; } }

        void EvaluateStatsAroundLevel(int level)
        {
            for (int i = level - 1; i <= level + 1; i++)
                if (i >= 1 && i <= t.Game.MaxLevel)
                    RHeaders[i - 1][0].Status = EvaluateLevelStats(i);
        }
        String EvaluateLevelStats(int level)
        {
            switch(manager.LevelStatus(t, level))
            {
                case LevelStat.Ok:
                case LevelStat.None:
                default:
                    return null; // ok!
                case LevelStat.TooFewSpent:
                    return "Not enough spent";
                case LevelStat.TooManySpent:
                    return "Too many spent";
                case LevelStat.RequirmentsNotMet:
                    return String.Join
                        (
                        Environment.NewLine,
                        dal.Get<BuildLevelParameter>()
                           .Where(x => x.Game.id == t.Game.id && x.Parameter.Type.id == t.id)
                           .Select(manager.MissingRequirments)
                           .Where(x => x.Count() > 0)
                           .Select(DescribeReqGroup)
                        );
                    
            }
        }
        String DescribeReqGroup((int amt, BuildParameter repgrp)[][] reqs)
        {
            return String.Join
            (
                " and ",
                reqs.Select(x =>
                {
                    var oj = String.Join(" or ", x.Select(y => String.Format("{0} {1}", y.amt, y.repgrp.Name)));
                    var mo = x.Length > 1;
                    return mo ? "(" + oj + ")" : oj;
                })
            );
        }

        public class RHVM
        {
            public int Level { get; set; }
            public String Status { get; set; }
        }

        public IList<IList<RHVM>> RHeaders { get; set; }
    }
}
