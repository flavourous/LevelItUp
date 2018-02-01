using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelItUp.Model
{
    public static class d_Underrail
    {
        // level is null signal
        public static BuildParameter t_level = null;

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
        public static BuildParameter f_expertise, f_nimble, f_ninja_looter, f_opportunist, f_pack_rathound, f_paranoia, f_quick_pockets, f_recklessness, f_snooping, f_aimed_shot,
                                     f_armor_sloping, f_burglar, f_clothier, f_conditioning, f_doctor, f_fast_metabolism, f_gun_nut, f_gunslinger, f_heavy_punch, f_hit_and_run,
                                     f_juggernaut, f_marksman, f_power_management, f_skinner, f_sprint, f_stoicism, f_suppressive_fire, f_sure_step, f_survival_instincts, f_thick_skull,
                                     f_corporeal_projection, f_disassemble, f_interloper, f_psychosis, f_pummel, f_tranquility, f_yell, f_cerebral_trauma, f_dirty_kick, f_force_user,
                                     f_high_technicalities, f_kneecap_shot, f_lightning_punches, f_point_shot, f_quick_tinkering, f_steadfast_aim, f_vile_weaponry, f_weaponsmith, f_bowyer, f_concussive_shots,
                                     f_crippling_strike, f_escape_artist, f_grenadier, f_meditation, f_mental_subversion, f_thermodynamicity, f_trap_expert, f_ballistics, f_cooked_shot, f_deflection,
                                     f_hypertoxicity, f_last_stand, f_neurology, f_practical_physicist, f_pyromaniac, f_salesman, f_snipe, f_ambush, f_cheap_shots, f_expose_weakness,
                                     f_fancy_footwork, f_full_auto, f_hypothermia, f_mad_chemist, f_pinning, f_premeditation, f_psychostatic_electricity, f_uncanny_dodge, f_wrestling, f_bone_breaker,
                                     f_cryogenic_induction, f_evasive_maneuvers, f_fatal_throw, f_guard, f_psionic_mania, f_rapid_fire, f_ripper, f_spec_ops, f_special_tactics, f_taste_for_blood,
                                     f_three_pointer, f_cut_throat, f_telekinetic_undulation, f_blitz, f_concentrated_fire, f_sharpshooter, f_combo, f_neural_overclocking, f_execute, f_critical_power,
                                     f_deadly_snares, f_elemental_bolts, f_locus_of_control, f_super_slam, f_commando, f_eviscerate, f_split_spare, f_heavyweight, f_psi_empathy, f_fisherman,
                                     f_hunter, f_echoing_soliloquy;

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

                    attributes.LevelPoints(1, 19, 10);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        attributes.LevelPoints(i, i % 4 == 0 ? 1 : 0, 20);

                    a_str = attributes.Parameter("Strength").Commit();
                    a_dex = attributes.Parameter("Dexterity").Commit();
                    a_agi = attributes.Parameter("Agility").Commit();
                    a_con = attributes.Parameter("Constitution").Commit();
                    a_per = attributes.Parameter("Perception").Commit();
                    a_wil = attributes.Parameter("Will").Commit();
                    a_int = attributes.Parameter("Intelligence").Commit();
                }
                using (var skills = builder.ParameterType("Skills"))
                {
                    t_skills = skills.ptype;

                    skills.LevelPoints(1, 120, 15);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        skills.LevelPoints(i, 40, 15 + (i - 1) * 5);

                    skills.SCategory("Offense");
                    s_guns = skills.Parameter("Guns").Commit();
                    s_throwing = skills.Parameter("Throwing").Commit();
                    s_crossbows = skills.Parameter("Crossbows").Commit();
                    s_melee = skills.Parameter("Melee").Commit();

                    skills.SCategory("Defense");
                    s_dodge = skills.Parameter("Dodge").Commit();
                    s_evasion = skills.Parameter("Evasion").Commit();

                    skills.SCategory("Subterfuge");
                    s_stealth = skills.Parameter("Stealth").Commit();
                    s_hacking = skills.Parameter("Hacking").Commit();
                    s_lockpicking = skills.Parameter("Lockpicking").Commit();
                    s_pickpocketing = skills.Parameter("Pickpocketing").Commit();
                    s_traps = skills.Parameter("Traps").Commit();

                    skills.SCategory("Technology");
                    s_mechanics = skills.Parameter("Mechanics").Commit();
                    s_electronics = skills.Parameter("Electronics").Commit();
                    s_chemistry = skills.Parameter("Chemistry").Commit();
                    s_biology = skills.Parameter("Biology").Commit();
                    s_tailoring = skills.Parameter("Tailoring").Commit();

                    skills.SCategory("Psi");
                    s_thoughtcontrol = skills.Parameter("ThoughtControl").Commit();
                    s_psychokinesis = skills.Parameter("Psychokinesis").Commit();
                    s_metathermics = skills.Parameter("Metathermics").Commit();
                    //                    s_temporalmanipulation = skills.Parameter("TemporalManipulation").Commit();

                    skills.SCategory("Social");
                    s_persuasion = skills.Parameter("Persuasion").Commit();
                    s_intimidation = skills.Parameter("Intimidation").Commit();
                    s_mercantile = skills.Parameter("Mercantile").Commit();
                }
                using (var feats = builder.ParameterType("Feats"))
                {
                    t_feats = feats.ptype;

                    feats.LevelPoints(1, 2,1);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        feats.LevelPoints(i, 1,1);

                    f_expertise = feats.Parameter("Expertise").Require(01, t_level).Commit();
                    f_nimble = feats.Parameter("Nimble").Require(01, t_level).Commit();
                    f_ninja_looter = feats.Parameter("Ninja Looter").Require(01, t_level).Commit();
                    f_opportunist = feats.Parameter("Opportunist").Require(01, t_level).Commit();
                    f_pack_rathound = feats.Parameter("Pack Rathound").Require(01, t_level).Commit();
                    f_paranoia = feats.Parameter("Paranoia").Require(01, t_level).Commit();
                    f_quick_pockets = feats.Parameter("Quick Pockets").Require(01, t_level).Commit();
                    f_recklessness = feats.Parameter("Recklessness").Require(01, t_level).Commit();
                    f_snooping = feats.Parameter("Snooping").Require(01, t_level).Commit();
                    f_aimed_shot = feats.Parameter("Aimed Shot").Require(01, t_level).Require(10, s_guns).OrRequire(10, s_crossbows).Require(06, a_per).Commit();
                    f_armor_sloping = feats.Parameter("Armor Sloping").Require(01, t_level).Commit();
                    f_burglar = feats.Parameter("Burglar").Require(01, t_level).Commit();
                    f_clothier = feats.Parameter("Clothier").Require(01, t_level).Commit();
                    f_conditioning = feats.Parameter("Conditioning").Require(01, t_level).Commit();
                    f_doctor = feats.Parameter("Doctor").Require(01, t_level).Commit();
                    f_fast_metabolism = feats.Parameter("Fast Metabolism").Require(01, t_level).Commit();
                    f_gun_nut = feats.Parameter("Gun Nut").Require(01, t_level).Commit();
                    f_gunslinger = feats.Parameter("Gunslinger").Require(01, t_level).Commit();
                    f_heavy_punch = feats.Parameter("Heavy Punch").Require(01, t_level).Commit();
                    f_hit_and_run = feats.Parameter("Hit and Run").Require(01, t_level).Commit();
                    f_juggernaut = feats.Parameter("Juggernaut").Require(01, t_level).Commit();
                    f_marksman = feats.Parameter("Marksman").Require(01, t_level).Commit();
                    f_power_management = feats.Parameter("Power Management").Require(01, t_level).Commit();
                    f_skinner = feats.Parameter("Skinner").Require(01, t_level).Commit();
                    f_sprint = feats.Parameter("Sprint").Require(01, t_level).Commit();
                    f_stoicism = feats.Parameter("Stoicism").Require(01, t_level).Commit();
                    f_suppressive_fire = feats.Parameter("Suppressive Fire").Require(01, t_level).Commit();
                    f_sure_step = feats.Parameter("Sure Step").Require(01, t_level).Commit();
                    f_survival_instincts = feats.Parameter("Survival Instincts").Require(01, t_level).Commit();
                    f_thick_skull = feats.Parameter("Thick Skull").Require(01, t_level).Commit();

                    f_corporeal_projection = feats.Parameter("Corporeal Projection").Require(02, t_level).Commit();
                    f_disassemble = feats.Parameter("Disassemble").Require(02, t_level).Commit();
                    f_interloper = feats.Parameter("Interloper").Require(02, t_level).Commit();
                    var b_psychosis = feats.Parameter("Psychosis");
                    var b_tranquility = feats.Parameter("Tranquility");
                    f_psychosis = b_psychosis.Require(02, t_level).Commit();
                    f_tranquility = b_tranquility.Require(02, t_level).Commit();
                    b_tranquility.Exclude(1, f_psychosis).Commit();
                    b_psychosis.Exclude(1, f_tranquility).Commit();
                    f_pummel = feats.Parameter("Pummel").Require(02, t_level).Commit();
                    f_yell = feats.Parameter("Yell").Require(02, t_level).Commit();

                    f_cerebral_trauma = feats.Parameter("Cerebral Trauma").Require(04, t_level).Commit();
                    f_dirty_kick = feats.Parameter("Dirty Kick").Require(04, t_level).Commit();
                    f_force_user = feats.Parameter("Force User").Require(04, t_level).Commit();
                    f_high_technicalities = feats.Parameter("High-Technicalities").Require(04, t_level).Commit();
                    f_kneecap_shot = feats.Parameter("Kneecap Shot").Require(04, t_level).Commit();
                    f_lightning_punches = feats.Parameter("Lightning Punches").Require(04, t_level).Commit();
                    f_point_shot = feats.Parameter("Point Shot").Require(04, t_level).Commit();
                    f_quick_tinkering = feats.Parameter("Quick Tinkering").Require(04, t_level).Commit();
                    f_steadfast_aim = feats.Parameter("Steadfast Aim").Require(04, t_level).Commit();
                    f_vile_weaponry = feats.Parameter("Vile Weaponry").Require(04, t_level).Commit();
                    f_weaponsmith = feats.Parameter("Weaponsmith").Require(04, t_level).Commit();
                    f_bowyer = feats.Parameter("Bowyer").Require(04, t_level).Commit();
                    f_concussive_shots = feats.Parameter("Concussive Shots").Require(04, t_level).Commit();
                    f_crippling_strike = feats.Parameter("Crippling Strike").Require(04, t_level).Commit();
                    f_escape_artist = feats.Parameter("Escape Artist").Require(04, t_level).Commit();
                    f_grenadier = feats.Parameter("Grenadier").Require(04, t_level).Commit();
                    f_meditation = feats.Parameter("Meditation").Require(04, t_level).Commit();
                    f_mental_subversion = feats.Parameter("Mental Subversion").Require(04, t_level).Commit();
                    f_thermodynamicity = feats.Parameter("Thermodynamicity").Require(04, t_level).Commit();
                    f_trap_expert = feats.Parameter("Trap Expert").Require(04, t_level).Commit();

                    f_ballistics = feats.Parameter("Ballistics").Require(06, t_level).Commit();
                    f_cooked_shot = feats.Parameter("Cooked Shot").Require(06, t_level).Commit();
                    f_deflection = feats.Parameter("Deflection").Require(06, t_level).Commit();
                    f_hypertoxicity = feats.Parameter("Hypertoxicity").Require(06, t_level).Commit();
                    f_last_stand = feats.Parameter("Last Stand").Require(06, t_level).Commit();
                    f_neurology = feats.Parameter("Neurology").Require(06, t_level).Commit();
                    f_practical_physicist = feats.Parameter("Practical Physicist").Require(06, t_level).Commit();
                    f_pyromaniac = feats.Parameter("Pyromaniac").Require(06, t_level).Commit();
                    f_salesman = feats.Parameter("Salesman").Require(06, t_level).Commit();
                    f_snipe = feats.Parameter("Snipe").Require(06, t_level).Commit();
                    f_ambush = feats.Parameter("Ambush").Require(06, t_level).Commit();
                    f_cheap_shots = feats.Parameter("Cheap Shots").Require(06, t_level).Commit();
                    f_expose_weakness = feats.Parameter("Expose Weakness").Require(06, t_level).Commit();
                    f_fancy_footwork = feats.Parameter("Fancy Footwork").Require(06, t_level).Commit();
                    f_full_auto = feats.Parameter("Full Auto").Require(06, t_level).Commit();
                    f_hypothermia = feats.Parameter("Hypothermia").Require(06, t_level).Commit();
                    f_mad_chemist = feats.Parameter("Mad Chemist").Require(06, t_level).Commit();
                    f_pinning = feats.Parameter("Pinning").Require(06, t_level).Commit();
                    f_premeditation = feats.Parameter("Premeditation").Require(06, t_level).Commit();
                    f_psychostatic_electricity = feats.Parameter("Psychostatic Electricity").Require(06, t_level).Commit();
                    f_uncanny_dodge = feats.Parameter("Uncanny Dodge").Require(06, t_level).Commit();
                    f_wrestling = feats.Parameter("Wrestling").Require(06, t_level).Commit();

                    f_bone_breaker = feats.Parameter("Bone Breaker").Require(08, t_level).Commit();
                    f_cryogenic_induction = feats.Parameter("Cryogenic Induction").Require(08, t_level).Commit();
                    f_evasive_maneuvers = feats.Parameter("Evasive Maneuvers").Require(08, t_level).Commit();
                    f_fatal_throw = feats.Parameter("Fatal Throw").Require(08, t_level).Commit();
                    f_guard = feats.Parameter("Guard").Require(08, t_level).Commit();
                    f_psionic_mania = feats.Parameter("Psionic Mania").Require(08, t_level).Commit();
                    f_rapid_fire = feats.Parameter("Rapid Fire").Require(08, t_level).Commit();
                    f_ripper = feats.Parameter("Ripper").Require(08, t_level).Commit();
                    f_spec_ops = feats.Parameter("Spec Ops").Require(08, t_level).Commit();
                    f_special_tactics = feats.Parameter("Special Tactics").Require(08, t_level).Commit();
                    f_taste_for_blood = feats.Parameter("Taste for Blood").Require(08, t_level).Commit();
                    f_three_pointer = feats.Parameter("Three Pointer").Require(08, t_level).Commit();

                    f_cut_throat = feats.Parameter("Cut Throat").Require(10, t_level).Commit();
                    f_telekinetic_undulation = feats.Parameter("Telekinetic Undulation").Require(10, t_level).Commit();
                    f_blitz = feats.Parameter("Blitz").Require(10, t_level).Commit();
                    f_concentrated_fire = feats.Parameter("Concentrated Fire").Require(10, t_level).Commit();
                    f_sharpshooter = feats.Parameter("Sharpshooter").Require(10, t_level).Require(60, s_guns).OrRequire(60, s_crossbows).Require(1, f_aimed_shot).Require(10, a_per).Commit();

                    f_combo = feats.Parameter("Combo").Require(12, t_level).Commit();             
                    f_neural_overclocking = feats.Parameter("Neural Overclocking").Require(12, t_level).Commit();
                    f_execute = feats.Parameter("Execute").Require(12, t_level).Commit();

                    f_critical_power = feats.Parameter("Critical Power").Require(14, t_level).Commit();
                    f_deadly_snares = feats.Parameter("Deadly Snares").Require(14, t_level).Commit();
                    f_elemental_bolts = feats.Parameter("Elemental Bolts").Require(14, t_level).Commit();
                    f_locus_of_control = feats.Parameter("Locus of Control").Require(14, t_level).Commit();
                    f_super_slam = feats.Parameter("Super Slam").Require(14, t_level).Commit();
                    f_commando = feats.Parameter("Commando").Require(14, t_level).Commit();
                    f_eviscerate = feats.Parameter("Eviscerate").Require(14, t_level).Commit();
                    f_split_spare = feats.Parameter("Split Spare").Require(14, t_level).Commit();

                    f_heavyweight = feats.Parameter("Heavyweight").Require(18, t_level).Commit();

                    f_psi_empathy = feats.Parameter("Psi Empathy", 0).Require(00, t_level).Commit();
                    f_fisherman = feats.Parameter("Fisherman", 0).Require(00, t_level).Commit();
                    f_hunter = feats.Parameter("Hunter", 0).Require(00, t_level).Commit();
                    f_echoing_soliloquy = feats.Parameter("Echoing Soliloquy", 0).Require(00, t_level).Commit();
                }
                return builder.game;
            }
        }
    }
}