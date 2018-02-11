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
        public readonly Build build;
        readonly List<BuildLevelParameter> buildParams;
        public BuildDefinitionManager(FakeDAL dal, GameBuild game, Build build )
        {
            this.dal = dal;
            this.build = build;

                var blp = dal.Get<BuildLevelParameter>().Where(x => x.Build.id == build.id);
            buildParams = dal.Get<BuildParameter>()
                             .ToList()
                             .Where(x => x.Game.id == build.Game.id)
                             .SelectMany(x => Enumerable.Range(1, game.MaxLevel).Select(i =>
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
                                                  }
                                                  return b;
                                              }))
                             .ToList();

            foreach (var b in buildParams)
                dal.Save(b);
        }

        public event Action<int> ParameterChanged = delegate { };

        public Action ChangeRequest(BuildLevelParameter param, int amount)
        {
            // breaking type level max limit?
            var newVal = param.Amount + amount;
            var lRule = dal.Get<BuildParameterTypeLevelPoints>()
                           .Where(x => x.Type.id == param.Parameter.Type.id)
                           .Where(x => x.Level == param.Level)
                           .Single();

            if (newVal > lRule.Limit)
                return null;

            var prevLevelVal = GetPreviousLevel(param).Amount;
            if (newVal < prevLevelVal && amount < 0)
                return null; // neither would that really.

            // check level restrictions
            var ldep = dal.Get<BuildParameterRequiement>()
                          .Where(x => x.Depend.id == param.Parameter.id)
                          .Where(x => x.On == null)
                          .ToArray();

            // Cant be picked/pushed that far at this level, ever.
            if (ldep.Where(x => newVal >= x.DAmount)
                    .Where(x => param.Level < x.OAmount)
                    .Any()) return null;
               
            // ok you could...
            return () =>
            {
                param.Amount += amount;
                dal.Save(param);
                ParameterChanged(param.Level);
            };
        }

        BuildLevelParameter GetPreviousLevel(BuildLevelParameter param)
        {
            if (param.Level == 1) // fake it
                return new BuildLevelParameter { Amount = param.Parameter.Type.Minimum, Build = param.Build, Game = param.Game, Level = 0, Parameter = param.Parameter };
            return buildParams.Single(x => x.Level == param.Level - 1 && x.Parameter.id == param.Parameter.id);
        }

        
        public Dictionary<BuildParameterType, LevelStat> LevelStatus(int lvl)
        {
            var levelParams = buildParams.Where(x => x.Level == lvl).ToList();
            Dictionary<BuildParameterType, LevelStat> dret = new Dictionary<BuildParameterType, LevelStat>();
            if (lvl > build.Game.MaxLevel || lvl < 1) return dret;
            foreach (var tp in dal.Get<BuildParameterType>().Where(x => x.Game.id == build.Game.id))
                dret[tp] = LevelStatusIO(tp, lvl, levelParams);
            return dret;
        }
        public LevelStat LevelStatus(BuildParameterType tp, int lvl)
        {
            var levelParams = buildParams.Where(x => x.Level == lvl).ToList();
            return LevelStatusIO(tp, lvl, levelParams);
        }
        LevelStat LevelStatusIO(BuildParameterType tp , int lvl, List<BuildLevelParameter> levelParams)
        {
            var typeLevelParams = levelParams.Where(x => x.Parameter.Type.id == tp.id).ToList();
            var prevLevel = typeLevelParams.Select(x => GetPreviousLevel(x));
            // minimum and limit have been processed
            // first, has nothing been done? dont panic then.
            var diff = typeLevelParams.Join(prevLevel, x => x.Parameter.id, x => x.Parameter.id, (l, p) => l.Parameter.Cost * (l.Amount - p.Amount)).Sum();
            var must = dal.Get<BuildParameterTypeLevelPoints>().SingleOrDefault(x => x.Type.id == tp.id && x.Level == lvl)?.Amount ?? 0;
            if (diff == 0) return must == 0 ? LevelStat.Ok : LevelStat.None;
            else if (diff > must) return LevelStat.TooManySpent;
            else if (diff < must) return LevelStat.TooFewSpent;
            else
            {
                // check if requirments are met
                var reqs = dal.Get<BuildParameterRequiement>()
                              .Where(x => x.On != null) //level restrictions already dealt with
                              .Where(x => typeLevelParams.Any(p => p.Parameter.id == x.Depend.id && p.Amount >= x.DAmount));
                Func<BuildParameterRequiement, int> reqAmt = x => levelParams.Single(l => l.Parameter.id == x.On.id).Amount;
                if (reqs.GroupBy(x => x.OrGroup)
                        .Any(y => y.All(x => reqAmt(x) < x.OAmount ^ x.Not)))
                    return LevelStat.RequirmentsNotMet;
                else return LevelStat.Ok; // no problem.
            }
        }

        public (int amount, BuildParameter param)[][] MissingRequirments(BuildLevelParameter plevel)
        {
            var levelParams = buildParams.Where(x => x.Level == plevel.Level);
            Func<BuildParameter, int> lAmount = p => p == null ? plevel.Level :
                levelParams.Single(l => l.Parameter.id == p.id).Amount;
            var deps = dal.Get<BuildParameterRequiement>()
                          .Where(x => x.Depend.id == plevel.Parameter.id);//match
            var abv = deps.Where(x => plevel.Amount >= x.DAmount); // the req is for above an amount
            var org = abv.GroupBy(x => x.OrGroup, x => (x.OAmount - lAmount(x.On), x.On, x.Not));// inside group is or
            var ret = org.Where(x => x.All(y => y.Item1 > 0)) // all OR are missing
                         .Select(x => x.Select(y => (y.Item1, y.Item2)).ToArray()); // arrayu or grps
            return ret.ToArray();
        }
    }
}