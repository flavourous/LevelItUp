using LevelItUp.Model;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelItUp.Core.ViewModels
{
    class ActionChoice : Choice
    {
        public IMvxAsyncCommand Action { get; set; }
    }
    public class BinaryParamTypeViewModel : ParamTypeViewModel
    {
        public BinaryParamTypeViewModel(FakeDAL dal, BuildParameterType t, BuildDefinitionManager manager) : base(dal,t,manager)
        {
            Cells = Enumerable.Range(1, t.Game.MaxLevel)
                              .Select(getLevel)
                              .ToList();
        }

        BinaryLevelSkills getLevel(int l)
        {
            var lvl = new BinaryLevelSkills();
            var tl = getLParams()
                      .Where(x=>x.Level == l)
                      .ToArray();

            lvl.AddCommand = new MvxAsyncCommand(async () =>
                 {
                     Dictionary<Choice, BuildLevelParameter> reference = new Dictionary<Choice, BuildLevelParameter>();
                     var choices = tl.Where(x => manager.ChangeRequest(x, +1) != null)
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
                             await AlterBinary(lp, +1);
                             lvl.Added.Add(getPicked(lp, lvl.Added));
                             await Mvx.Resolve<IMvxNavigationService>().Close(choiceVM);
                         })
                     };
                     await Mvx.Resolve<IMvxNavigationService>().Navigate(choiceVM);
                 });

            var itms = tl.Where(lp => lp.Amount == 1)
                         .Select(x => getPicked(x, lvl.Added));
            lvl.Added = new MvxObservableCollection<BinaryAddedSkill>(itms);
            lvl.Level = l;

            // the format we chose...
            return lvl;
        }

        async Task AlterBinary(BuildLevelParameter lpStart, int change)
        {
            await Task.Run(() =>
            {
                var lps = getLParams().Where(x => x.Parameter.id == lpStart.Parameter.id)
                                      .Where(x => x.Level >= lpStart.Level)
                                      .OrderBy(x => x.Level)
                                      .ToArray();
                foreach (var lp in lps) manager.ChangeRequest(lp, change)();
            });
        }

        BinaryAddedSkill getPicked(BuildLevelParameter lp, IList<BinaryAddedSkill> lvl)
        {
            BinaryAddedSkill ret = null;
            ret = new BinaryAddedSkill
            {

                Name = lp.Parameter.Name,
                RemoveCommand = new MvxAsyncCommand(async () =>
                {
                    await AlterBinary(lp, -1);
                    lvl.Remove(ret);
                })
            };
            return ret;
        }

        IEnumerable<BuildLevelParameter> getLParams()
        {
            return dal.Get<BuildLevelParameter>()
                      .Where(x => x.Game.id == t.Game.id)
                      .Where(x => x.Build.id == manager.build.id)
                      .Where(x => x.Parameter.Type.id == t.id);
        }


        // no col headers, cells get into a stack
        public IList<BinaryLevelSkills> Cells { get; set; }
        public IMvxAsyncCommand<BinaryLevelSkills> Tapped { get; set; }
    }

    public class BinaryAddedSkill
    {
        public String Name { get; set; }
        public IMvxAsyncCommand RemoveCommand { get; set; }
    }

    public class BinaryLevelSkills
    {
        public int Level { get; set; }
        public MvxObservableCollection<BinaryAddedSkill> Added { get; set; } 
        public IMvxAsyncCommand AddCommand { get; set; }
    }

}
