using LevelItUp.Model;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            Type = GetType().Name;

            var Popup = new MvxCommand<string>(s => StatusModal = s);
            ClosePopup = new MvxCommand(() => StatusModal = null);

            // Row Headers
            RHeaders = Enumerable.Range(1, t.Game.MaxLevel)
                                 .Select(x =>
                                 {
                                     var xx = EvaluateLevelStats(manager.LevelStatus(t, x),x);
                                     return new[] { new RHVM { Level = x, Status = xx.status, Icon = xx.icon, StatusTap = Popup } }.ToList() as IList<RHVM>;
                                 })
                                 .ToList();
            manager.ParameterChanged += EvaluateStatsAroundLevel;
        }

        public String Type { get; set; }
        public String Name { get; set; }
        public int id { get { return t.id; } }

        SemaphoreSlim ss = new SemaphoreSlim(1, 1);
        HashSet<int> ToEvaluate = new HashSet<int>();
        void EvaluateStatsAroundLevel(int level)
        {
            lock (ToEvaluate)
            {
                for (int i = level - 1; i <= level + 1; i++)
                {
                    if (i >= 1 && i <= t.Game.MaxLevel)
                    {
                        if (!ToEvaluate.Contains(i))
                        {
                            RHeaders[i - 1][0].Icon = "⋯";
                            ToEvaluate.Add(i);
                        }
                    }
                }
            }
            Task.Run(async () =>
            {
                await ss.WaitAsync();
                try
                {
                    while (true)
                    {
                        int lev = -1;
                        lock (ToEvaluate)
                        {
                            if (ToEvaluate.Count == 0) return;
                            lev = ToEvaluate.First();
                            ToEvaluate.Remove(lev);
                        }
                        var lst = EvaluateLevelStats(manager.LevelStatus(t, lev),lev);
                        lock (ToEvaluate)
                        {
                            if (!ToEvaluate.Contains(lev))
                            {
                                RHeaders[lev - 1][0].Status = lst.status;
                                RHeaders[lev - 1][0].Icon = lst.icon;
                            }
                        }
                    }
                }
                finally { ss.Release(); }
            });
        }
        (String icon, String status) EvaluateLevelStats(LevelStat stat, int level)
        {
            switch(stat)
            {
                case LevelStat.Ok:
                    return (" ", null); // ok!
                case LevelStat.None:
                default:
                    return ("◎", null); // not started yet.
                case LevelStat.TooFewSpent:
                    return ("➘", "Not enough spent");
                case LevelStat.TooManySpent:
                    return ("➚", "Too many spent");
                case LevelStat.RequirmentsNotMet:
                    return ("⛔", String.Join
                        (
                        Environment.NewLine,
                        dal.Get<BuildLevelParameter>()
                           .Where(x => x.Game.id == t.Game.id && x.Parameter.Type.id == t.id && x.Level == level)
                           .Select(x=> ( x, manager.MissingRequirments(x) ))
                           .Where(x => x.Item2.Count() > 0)
                           .Select(x=> DescribeReqGroup(x.Item2) + " for " + DescripbeLevelParam( x.Item1))
                        ));
                    
            }
        }

        String DescripbeLevelParam(BuildLevelParameter lp)
        {
            if (lp.Amount == 1)
                return lp.Parameter.Name;
            return lp.Amount + " " + lp.Parameter.Name;
        }
        String DescribeReqGroup((int amt, BuildParameter repgrp)[][] reqs)
        {
            return String.Join
            (
                " and ",
                reqs.Select(x =>
                {
                    var oj = String.Join(" or ", x.Select(y => String.Format("{0} {1}", y.amt > 0 ? y.amt + " more" : -y.amt + " less", y.repgrp.Name)));
                    var mo = x.Length > 1;
                    return mo ? "(" + oj + ")" : oj;
                })
            );
        }


        private String statusModal = null;
        public string StatusModal { get => statusModal; set => this.RaiseAndSetIfChanged(ref statusModal, value); }

        IMvxCommand closePopup;
        public IMvxCommand ClosePopup { get => closePopup; set => this.RaiseAndSetIfChanged(ref closePopup, value); }

        public IList<IList<RHVM>> RHeaders { get; set; }
    }
    public class RHVM : MvxNotifyPropertyChanged
    {
        public int Level { get; set; }
        private String status;
        public string Status { get => status; set => this.RaiseAndSetIfChanged(ref status, value); }
        private String icon;
        public string Icon { get => icon; set => this.RaiseAndSetIfChanged(ref icon, value); }
        public IMvxCommand<String> StatusTap { get; set; }
    }
}
