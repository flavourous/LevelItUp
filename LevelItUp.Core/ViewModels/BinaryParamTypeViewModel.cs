﻿using LevelItUp.Model;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using MvvmCross.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class BinaryParamTypeViewModel : ParamTypeViewModel
    {
        public BinaryParamTypeViewModel(FakeDAL dal, BuildParameterType t, BuildDefinitionManager manager) : base(dal,t,manager)
        {
            Cells = Enumerable.Range(1, t.Game.MaxLevel)
                              .Select(getLevel)
                              .ToList();
        }

        IList<IEnumerable> getLevel(int l)
        {
            MvxObservableCollection<CVM> lvl = new MvxObservableCollection<CVM>();
            var tl = thisLevel(l).ToArray();

            var adder = new CVM
            {
                Name = "Add",
                Adder = true,
                Command = new MvxAsyncCommand(async () =>
                {
                    Dictionary<Choice, BuildLevelParameter> reference = new Dictionary<Choice, BuildLevelParameter>();
                    var choices = tl.Where(x=>manager.ChangeRequest(x,+1)!=null)
                                    .GroupBy(x => x.Parameter.Category)
                                    .Select(x =>
                                    {
                                        var g = new ChoiceGroup { Group = x.Key };
                                        g.AddRange(x.Select(y =>
                                        {
                                            var c = new Choice { Name = y.Parameter.Name };
                                            reference[c] = y;
                                            return c;
                                        }));
                                        return g;
                                    })
                                    .ToList();
                    if (choices.Count == 1 && choices.First().Group == null)
                        choices.First().Group = t.Name;
                    ChoiceViewModel choiceVM = null;
                    choiceVM = new ChoiceViewModel
                    {
                        Choices = choices,
                        Choose = new MvxAsyncCommand<Choice>(async c =>
                        {
                            var lp = reference[c];
                            var req = manager.ChangeRequest(lp, +1);
                            req();
                            lvl.Insert(lvl.Count - 1, getPicked(lp, lvl));
                            await Mvx.Resolve<IMvxNavigationService>().Close(choiceVM);
                        })
                    };
                    await Mvx.Resolve<IMvxNavigationService>().Navigate(choiceVM);
                })
            };

            var picked = tl.Where(lp => lp.Amount == 1)
                           .Select(x => getPicked(x, lvl));

            lvl.AddRange(picked);
            lvl.Add(adder);

            // the format we chose...
            return new[] { lvl as IEnumerable }.ToList() as IList<IEnumerable>;
        }

        CVM getPicked(BuildLevelParameter lp, MvxObservableCollection<CVM> lvl)
        {
            CVM ret = null;
            ret = new CVM
            {
                Name = lp.Parameter.Name,
                Adder = false,
                Command = new MvxCommand(() =>
                {
                    manager.ChangeRequest(lp, -1)();
                    lvl.Remove(ret);
                })
            };
            return ret;
        }

        IEnumerable<BuildLevelParameter> thisLevel(int level)
        {
            return dal.Get<BuildLevelParameter>()
                      .Where(x => x.Game.id == t.Game.id)
                      .Where(x => x.Build.id == manager.build.id)
                      .Where(x => x.Parameter.Type.id == t.id)
                      .Where(x => x.Level == level);
        }

        class CVM
        {
            public String Name { get; set; }
            public IMvxCommand Command { get; set; }
            public bool Adder { get; set; }
        }

        // no col headers, cells get into a stack
        public IList<IList<IEnumerable>> Cells { get; set; }

    }
}
