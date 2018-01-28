using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Model
{
    public enum LevelStat { None, Ok, TooManySpent, TooFewSpent, RequirmentsNotMet };
    public class BuildDefinitionManager
    {
        readonly FakeDAL dal;
        readonly Build build;
        readonly List<BuildLevelParameter> buildParams;
        public BuildDefinitionManager(FakeDAL dal, GameBuild game, Build build = null)
        {
            this.dal = dal;
            if(build==null)
            {
                build = new Build { Name = "New Build", Game = game };
                dal.Save(build);
            }
            this.build = build;

            var blp = dal.Get<BuildLevelParameter>().Where(x => x.Build.id == build.id);
            buildParams = dal.Get<BuildParameter>()
                             .ToList()
                             .Where(x => x.Game.id == build.Game.id)
                             .SelectMany(x => Enumerable.Range(1, 25).Select(i =>
                                              {
                                                 // get or create the levelparameter
                                                 var b = blp.FirstOrDefault(l => l.Parameter.id == x.id && l.Level == i);
                                                 if (b == null)
                                                 {
                                                     b = new BuildLevelParameter
                                                     {
                                                         Game = build.Game,
                                                         Build = build,
                                                         Parameter = x,
                                                         Amount = x.Type.Minimum,
                                                         Level = i
                                                     };
                                                     dal.Save(b);
                                                 }
                                                 return b;
                                              }))
                             .ToList();
        }

        public Action ChangeRequest(BuildLevelParameter param, int amount)
        {
            var levelRules = dal.Get<BuildParameterTypeLevelPoints>().Single(x => x.Type.id == param.Parameter.Type.id && x.Level == param.Level);
            var newVal = param.Amount + amount;
            if (newVal < param.Parameter.Type.Minimum || newVal > levelRules.Limit)
                return null; // That would never be possible.

            var prevLevelVal = GetPreviousLevel(param).Amount;
            if (newVal < prevLevelVal && amount < 0)
                return null; // neither would that really.

            return () =>
            {
                param.Amount += amount;
                dal.Save(param);
            };
        }

        BuildLevelParameter GetPreviousLevel(BuildLevelParameter param)
        {
            if (param.Level == 1) // fake it
                return new BuildLevelParameter { Amount = param.Parameter.Type.Minimum, Build = param.Build, Game = param.Game, Level = 0, Parameter = param.Parameter };
            return buildParams.Single(x => x.Level == param.Level - 1 && x.Parameter.id == param.Parameter.id);
        }

        
        public Dictionary<BuildParameterType, LevelStat[]> LevelStatus()
        {
            Dictionary<BuildParameterType, LevelStat[]> dret = new Dictionary<BuildParameterType, LevelStat[]>();
            foreach (var tp in dal.Get<BuildParameterType>().Where(x => x.Game.id == build.Game.id))
            {
                List<LevelStat> r = new List<LevelStat>();
                for (int i = 1; i <= build.Game.MaxLevel; i++)
                {
                    var levelParams = buildParams.Where(x => x.Level == i && x.Parameter.Type.id == tp.id).ToList();
                    var prevLevel = levelParams.Select(x => GetPreviousLevel(x));
                    // minimum and limit have been processed
                    // first, has nothing been done? dont panic then.
                    var diff = levelParams.Join(prevLevel, x => x.Parameter.id, x => x.Parameter.id, (l, p) => l.Amount - p.Amount).Sum();
                    var must = dal.Get<BuildParameterTypeLevelPoints>().SingleOrDefault(x => x.Type.id == tp.id && x.Level == i)?.Amount ?? 0;
                    if (levelParams.All(x=>x.Amount == x.Parameter.Type.Minimum))
                        r.Add(LevelStat.None);
                    else if (diff > must) r.Add(LevelStat.TooManySpent);
                    else if (diff < must) r.Add(LevelStat.TooFewSpent);
                    else
                    {
                        // check if requirments are met
                        var reqs = dal.Get<BuildParameterRequiement>()
                                      .Where(x => levelParams.Any(p => p.Parameter.id == x.Depend.id && p.Amount >= x.DAmount));
                        if (reqs.Any(x => (levelParams.Single(l => l.Parameter.id == x.On.id).Amount < x.OAmount)))
                            r.Add(LevelStat.RequirmentsNotMet);
                        else r.Add(LevelStat.Ok); // no problem.
                    }
                }
                dret[tp] = r.ToArray();
            }
            return dret;
        }

        public (int amount, BuildParameter param)[] MissingRequirments(BuildLevelParameter plevel)
        {
            var levelParams = buildParams.Where(x => x.Level == plevel.Level);
            return dal.Get<BuildParameterRequiement>()
                      .Where(x => x.Depend.id == plevel.Parameter.id)
                      .Where(x => plevel.Amount >= x.DAmount)
                      .Select(x => (x.OAmount - levelParams.Single(l => l.Parameter.id == x.On.id).Amount, x.On))
                      .Where(x => x.Item1 > 0)
                      .ToArray();
        }
    }
}