using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Model
{
    public static class d_Underrail
    {
        // Attributes
        public static BuildParameterType t_attributes;
        public static BuildParameter a_str, a_dex, a_agi, a_con, a_per, a_wil, a_int;

        // Skills
        public static BuildParameterType t_skills;
        public static BuildParameter s_guns, s_throwing, s_crossbows, s_melee;// offense
        public static BuildParameter s_dodge, s_evasion;// defense
        public static BuildParameter s_stealth, s_hacking, s_lockpicking, s_pickpocketing, s_traps;// subterfuge
        public static BuildParameter s_mechanics, s_electronics, s_chemistry, s_biology, s_tailoring;// technology
        public static BuildParameter s_thoughtcontrol, s_psychokinesis, s_metathermics, s_temporalmanipulation;// psi
        public static BuildParameter s_persuasion, s_intimidation, s_mercantile;// social

        // Feats
        public static BuildParameterType t_feats;
        public static BuildParameter f_nimble, f_three_pointer, f_evasive_manuvers;
        public static GameBuild Generate(FakeDAL dal)
        {
            // or something
            //var g = dal.Get<GameBuild>().FirstOrDefault(x => x.Name == "Underrail");
            //if (g != null) return g; // already there

            // Gam
            using (var builder = new DefinitionBuilder(dal, "Underrail", 25))
            {
                using (var attributes = builder.ParameterType("Attributes", 3))
                {
                    t_attributes = attributes.ptype;

                    attributes.LevelPoint(1, 19, 10);
                    for (int i = 4; i <= builder.game.MaxLevel; i += 4)
                        attributes.LevelPoint(i, 1, 20);

                    a_str = attributes.Parameter("Strength").NoRequirments();
                    a_dex = attributes.Parameter("Dexterity").NoRequirments();
                    a_agi = attributes.Parameter("Agility").NoRequirments();
                    a_con = attributes.Parameter("Constitution").NoRequirments();
                    a_per = attributes.Parameter("Perception").NoRequirments();
                    a_wil = attributes.Parameter("Will").NoRequirments();
                    a_int = attributes.Parameter("Intelligence").NoRequirments();
                }
                using (var skills = builder.ParameterType("Skills"))
                {
                    t_skills = skills.ptype;

                    skills.LevelPoint(1, 120, 15);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        skills.LevelPoint(i, 15, (i - 1) * 5 + 15);

                    skills.SubCategory("Offense");
                                    s_guns = skills.Parameter("Guns").NoRequirments();
                                s_throwing = skills.Parameter("Throwing").NoRequirments();
                               s_crossbows = skills.Parameter("Crossbows").NoRequirments();
                                   s_melee = skills.Parameter("Melee").NoRequirments();

                    skills.SubCategory("Defense");
                                   s_dodge = skills.Parameter("Dodge").NoRequirments();
                                 s_evasion = skills.Parameter("Evasion").NoRequirments();

                    skills.SubCategory("Subterfuge");
                                 s_stealth = skills.Parameter("Stealth").NoRequirments();
                                 s_hacking = skills.Parameter("Hacking").NoRequirments();
                             s_lockpicking = skills.Parameter("Lockpicking").NoRequirments();
                           s_pickpocketing = skills.Parameter("Pickpocketing").NoRequirments();
                                   s_traps = skills.Parameter("Traps").NoRequirments();

                    skills.SubCategory("Technology");
                               s_mechanics = skills.Parameter("Mechanics").NoRequirments();
                             s_electronics = skills.Parameter("Electronics").NoRequirments();
                               s_chemistry = skills.Parameter("Chemistry").NoRequirments();
                                 s_biology = skills.Parameter("Biology").NoRequirments();
                               s_tailoring = skills.Parameter("Tailoring").NoRequirments();

                    skills.SubCategory("Psi");
                          s_thoughtcontrol = skills.Parameter("ThoughtControl").NoRequirments();
                           s_psychokinesis = skills.Parameter("Psychokinesis").NoRequirments();
                            s_metathermics = skills.Parameter("Metathermics").NoRequirments();
                    s_temporalmanipulation = skills.Parameter("TemporalManipulation").NoRequirments();

                    skills.SubCategory("Social");
                              s_persuasion = skills.Parameter("Persuasion").NoRequirments();
                            s_intimidation = skills.Parameter("Intimidation").NoRequirments();
                              s_mercantile = skills.Parameter("Mercantile").NoRequirments();
                }
                using (var feats = builder.ParameterType("Feats"))
                {
                    t_feats = feats.ptype;

                    feats.LevelPoint(1, 2, 1);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        feats.LevelPoint(i, 1, 1);

                              f_nimble = feats.Parameter("Nimble").NoRequirments();
                       f_three_pointer = feats.Parameter("Three Pointer").NoRequirments();
                    f_evasive_manuvers = feats.Parameter("Evasive Manuvers").NoRequirments();
                }

                return builder.game;
            }
        }
    }
}