using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class ParamTypeViewModel : MvxViewModel
    {
        FakeDAL dal;
        BuildDefinitionManager manager;
        BuildParameterType t;
        public ParamTypeViewModel(FakeDAL dal, BuildParameterType t, BuildDefinitionManager manager)
        {
            this.dal = dal;
            this.manager = manager;
            this.t = t;
            Name.Value = t.Name;
            Params.Value = dal.Get<BuildParameter>()
                              .Where(x => x.Type.id == t.id && x.Game.id == t.Game.id)
                              .Select(x => new ParamViewModel(dal, x, manager))
                              .ToList();
            foreach(var pt in Params.Value)
                foreach(var l in pt.Levels.Value)
                    l.PropertyChanged += (s, e) =>
                            EvaluateStatsAroundLevel(e.PropertyName, l.Level.Value);
            for(int i=1;i<=t.Game.MaxLevel;i++)
                LevelStatus[i] =  EvaluateLevelStats(i);
        }

        void EvaluateStatsAroundLevel(String pn, int level)
        {
            if (pn != "Amount") return;
            for (int i = level - 1; i <= level + 1; i++)
                if (i >= 1 && i <= t.Game.MaxLevel)
                    LevelStatus[i] = EvaluateLevelStats(i);
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

        public INC<String> Name = new NC<String>();
        public INCList<ParamViewModel> Params = new NCList<ParamViewModel>();
        public INCDictionary<int, String> LevelStatus = new NCDictionary<int, String>();

        public int id { get { return t.id; } }
    }
}
