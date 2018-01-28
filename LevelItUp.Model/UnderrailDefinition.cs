using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Model
{
    public class UnderrailDefinition
    {
        public static GameBuild Generate(FakeDAL dal)
        {
            var g = dal.Get<GameBuild>().FirstOrDefault(x => x.Name == "Underrail");
            if (g != null) return g; // already there

            // Gam
            using (var builder = new DefinitionBuilder(dal, "Underrail", 25))
            {
                BuildParameter str, agi;
                using (var attributes = builder.ParameterType("Attributes", 3))
                {
                    attributes.LevelPoint(1, 40, 10);
                    for (int i = 4; i <= builder.game.MaxLevel; i += 4)
                        attributes.LevelPoint(i, 1, 20);

                    str = attributes.Parameter("Strength").NoRequirments();
                    agi = attributes.Parameter("Agility").NoRequirments();
                }
                BuildParameter throwing, dodge;
                using (var skills = builder.ParameterType("Skills"))
                {
                    skills.LevelPoint(1, 120, 15);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        skills.LevelPoint(i, 15, (i - 1) * 5 + 15);

                    throwing = skills.Parameter("Throwing").NoRequirments();
                    dodge = skills.Parameter("Dodge").NoRequirments();
                }
                using (var feats = builder.ParameterType("Feats"))
                {
                    feats.LevelPoint(1, 2, 1);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        feats.LevelPoint(i, 1, 1);

                    feats.Parameter("Nimble").NoRequirments();
                    feats.Parameter("Three Pointer").NoRequirments();
                    feats.Parameter("Evasive Manuvers").NoRequirments();
                }

                return builder.game;
            }
        }
    }
}