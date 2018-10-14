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

        // Psi-abilities (have skill deps and feat deps on them)
        public static BuildParameterType t_psi_abilities;
        public static BuildParameter p_neural_overload, p_frighten, p_mental_breakdown, p_billocation, p_psi_cognitive_interruption,
                                     p_enrage, p_psuedo_spatial_projection, p_neurovisual_disruption,
                                     p_telekinetic_punch, p_force_field, p_telekinetic_proxy, p_force_emission,
                                     p_disruptive_field, p_implosion, p_electrokinesis, p_electrokinetic_imprint,
                                     p_pyrokinesis, p_pyrokinetic_stream, p_exothermic_aura, p_thermodynamic_destabilisation,
                                     p_cryokinesis, p_cryostasis, p_cryokinetic_orb, p_cryo_shield;

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
                    s_thoughtcontrol = skills.Parameter("Thought Control").Commit();
                    s_psychokinesis = skills.Parameter("Psychokinesis").Commit();
                    s_metathermics = skills.Parameter("Metathermics").Commit();
                    //                    s_temporalmanipulation = skills.Parameter("TemporalManipulation").Commit();

                    skills.SCategory("Social");
                    s_persuasion = skills.Parameter("Persuasion").Commit();
                    s_intimidation = skills.Parameter("Intimidation").Commit();
                    s_mercantile = skills.Parameter("Mercantile").Commit();
                }

                using (var psi = builder.ParameterType("Psi Abilities"))
                {
                    t_psi_abilities = psi.ptype;
                    for (int i = 1; i <= builder.game.MaxLevel; i++)
                        psi.LevelPoints(i, 0, 1);

                    psi.SCategory("Metathermics");
                    p_cryokinesis = psi.Parameter("Cryokinesis", 0).Require(0, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_pyrokinesis = psi.Parameter("Pyrokinesis", 0).Require(25, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_cryostasis = psi.Parameter("Cryostasis", 0).Require(35, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_pyrokinetic_stream = psi.Parameter("Pyrokinetic Stream", 0).Require(35, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_cryokinetic_orb = psi.Parameter("Cryokinetic Orb", 0).Require(40, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_thermodynamic_destabilisation = psi.Parameter("Thermodynamic Destabilisation", 0).Require(50, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_cryo_shield = psi.Parameter("Cryo Sheild", 0).Require(55, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_exothermic_aura = psi.Parameter("Exothermic Aura", 0).Require(70, s_metathermics).Require(1, f_psi_empathy).ImplyLevelReq().Commit();

                    psi.SCategory("Psychokinesis");
                    p_telekinetic_punch = psi.Parameter("Telekinetic Punch", 0).Require(0, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_force_field = psi.Parameter("Force Field", 0).Require(25, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_electrokinesis = psi.Parameter("Electrokinesis", 0).Require(30, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_force_emission = psi.Parameter("Force Emission", 0).Require(35, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_disruptive_field = psi.Parameter("Disruptive Field", 0).Require(40, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_electrokinetic_imprint = psi.Parameter("Electrokinetic Imprint", 0).Require(45, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_telekinetic_proxy = psi.Parameter("Telekinetic Proxy", 0).Require(55, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_implosion = psi.Parameter("Implosion", 0).Require(70, s_psychokinesis).Require(1, f_psi_empathy).ImplyLevelReq().Commit();

                    psi.SCategory("Thought Control");
                    p_neural_overload = psi.Parameter("Neural Overload", 0).Require(0, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_frighten = psi.Parameter("Frighten", 0).Require(30, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_billocation = psi.Parameter("Billocation", 0).Require(35, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_mental_breakdown = psi.Parameter("Mental Breakdown", 0).Require(45, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_psi_cognitive_interruption = psi.Parameter("Psi-cognitive Interruption", 0).Require(55, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_enrage = psi.Parameter("Enrage", 0).Require(60, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_psuedo_spatial_projection = psi.Parameter("Psuedo-spatial Projection", 0).Require(65, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                    p_neurovisual_disruption = psi.Parameter("Neurovisual Disruption", 0).Require(70, s_thoughtcontrol).Require(1, f_psi_empathy).ImplyLevelReq().Commit();
                }

                using (var feats = builder.ParameterType("Feats"))
                {
                    t_feats = feats.ptype;

                    feats.LevelPoints(1, 2,1);
                    for (int i = 2; i <= builder.game.MaxLevel; i++)
                        feats.LevelPoints(i, i % 2 == 0 ? 1 : 0, 1);

                    f_expertise = feats.Parameter("Expertise").ImplyLevelReq().Commit();
                    f_nimble = feats.Parameter("Nimble").ImplyLevelReq().Commit();
                    f_ninja_looter = feats.Parameter("Ninja Looter").ImplyLevelReq().Commit();
                    f_opportunist = feats.Parameter("Opportunist").ImplyLevelReq().Commit();
                    f_pack_rathound = feats.Parameter("Pack Rathound").ImplyLevelReq().Commit();
                    f_paranoia = feats.Parameter("Paranoia").ImplyLevelReq().Commit();
                    f_quick_pockets = feats.Parameter("Quick Pockets").ImplyLevelReq().Commit();
                    f_recklessness = feats.Parameter("Recklessness").ImplyLevelReq().Commit();
                    f_snooping = feats.Parameter("Snooping").ImplyLevelReq().Commit();
                    f_aimed_shot = feats.Parameter("Aimed Shot").Require(10, s_guns).OrRequire(10, s_crossbows).Require(06, a_per).ImplyLevelReq().Commit();
                    f_armor_sloping = feats.Parameter("Armor Sloping").Require(06,a_int).Require(15, s_mechanics).ImplyLevelReq().Commit();
                    f_burglar = feats.Parameter("Burglar").Require(7,a_dex).Require(15, s_lockpicking).OrRequire(15,s_hacking).ImplyLevelReq().Commit();
                    f_clothier = feats.Parameter("Clothier").Require(7,a_int).Require(15,s_tailoring).ImplyLevelReq().Commit();
                    f_conditioning = feats.Parameter("Conditioning").Require(5,a_con).ImplyLevelReq().Commit();
                    f_doctor = feats.Parameter("Doctor").Require(15,s_biology).ImplyLevelReq().Commit();
                    f_fast_metabolism = feats.Parameter("Fast Metabolism").Require(6,a_con).ImplyLevelReq().Commit();
                    f_gun_nut = feats.Parameter("Gun Nut").Require(7,a_int).Require(15,s_mechanics).ImplyLevelReq().Commit();
                    f_gunslinger = feats.Parameter("Gunslinger").Require(7, a_dex).Require(15, s_guns).ImplyLevelReq().Commit();
                    f_heavy_punch = feats.Parameter("Heavy Punch").Require(5, a_str).Require(15, s_melee).ImplyLevelReq().Commit();
                    f_hit_and_run = feats.Parameter("Hit and Run").Require(7,a_agi).ImplyLevelReq().Commit();
                    f_juggernaut = feats.Parameter("Juggernaut").Require(7,a_str).Require(7,a_str).ImplyLevelReq().Commit();
                    f_marksman = feats.Parameter("Marksman").Require(5,a_dex).Require(15,s_crossbows).ImplyLevelReq().Commit();
                    f_power_management = feats.Parameter("Power Management").Require(7,a_int).Require(15,s_electronics).ImplyLevelReq().Commit();
                    f_skinner = feats.Parameter("Skinner").Require(7,a_int).Require(15,s_tailoring).ImplyLevelReq().Commit();
                    f_sprint = feats.Parameter("Sprint").Require(6,a_agi).ImplyLevelReq().Commit();
                    f_stoicism = feats.Parameter("Stoicism").Require(7,a_wil).Require(7,a_con).ImplyLevelReq().Commit();
                    f_suppressive_fire = feats.Parameter("Suppressive Fire").Require(6,a_per).Require(10,s_guns).ImplyLevelReq().Commit();
                    f_sure_step = feats.Parameter("Sure Step").Require(5, a_agi).ImplyLevelReq().Commit();
                    f_survival_instincts = feats.Parameter("Survival Instincts").Require(9, a_con).ImplyLevelReq().Commit();
                    f_thick_skull = feats.Parameter("Thick Skull").Require(10,a_con).ImplyLevelReq().Commit();

                    f_corporeal_projection = feats.Parameter("Corporeal Projection").Require(1,p_telekinetic_punch).OrRequire(1,p_force_emission).Require(7,a_str).ImplyLevelReq().Commit();
                    f_disassemble = feats.Parameter("Disassemble").Require(7, a_int).Require(20, s_electronics).OrRequire(20, s_mechanics).ImplyLevelReq().Commit();
                    f_interloper = feats.Parameter("Interloper").Require(7,a_agi).Require(20,s_stealth).ImplyLevelReq().Commit();
                    var b_psychosis = feats.Parameter("Psychosis");
                    var b_tranquility = feats.Parameter("Tranquility");
                    f_psychosis = b_psychosis.ImplyLevelReq().Commit();
                    f_tranquility = b_tranquility.ImplyLevelReq().Commit();
                    b_tranquility.Require(1, f_psi_empathy).Exclude(1, f_psychosis).ImplyLevelReq().Commit();
                    b_psychosis.Require(1, f_psi_empathy).Exclude(1, f_tranquility).ImplyLevelReq().Commit();
                    f_pummel = feats.Parameter("Pummel").Require(20,s_melee).ImplyLevelReq().Commit();
                    f_yell = feats.Parameter("Yell").Require(20,s_intimidation).ImplyLevelReq().Commit();

                    f_cerebral_trauma = feats.Parameter("Cerebral Trauma").Require(25,s_thoughtcontrol).Require(1, p_neural_overload).ImplyLevelReq().Commit();
                    f_dirty_kick = feats.Parameter("Dirty Kick").Require(25,s_melee).ImplyLevelReq().Commit();
                    f_force_user = feats.Parameter("Force User").Require(7,a_wil).Require(1,p_telekinetic_punch).OrRequire(1, p_force_field).Require(25,s_psychokinesis).ImplyLevelReq().Commit();
                    f_high_technicalities = feats.Parameter("High-Technicalities").Require(5,a_int).Require(25,s_guns).ImplyLevelReq().Commit();
                    f_kneecap_shot = feats.Parameter("Kneecap Shot").Require(4,a_per).Require(25,s_guns).OrRequire(25,s_crossbows).ImplyLevelReq().Commit();
                    f_lightning_punches = feats.Parameter("Lightning Punches").Require(8,a_dex).Require(25,s_melee).ImplyLevelReq().Commit();
                    f_point_shot = feats.Parameter("Point Shot").Require(6,a_per).Require(6,a_dex).Require(25, s_guns).ImplyLevelReq().Commit();
                    f_quick_tinkering = feats.Parameter("Quick Tinkering").Require(25,s_traps).Require(7,a_dex).ImplyLevelReq().Commit();
                    f_steadfast_aim = feats.Parameter("Steadfast Aim").Require(5,a_str).Require(6,a_dex).Require(25,s_guns).ImplyLevelReq().Commit();
                    f_vile_weaponry = feats.Parameter("Vile Weaponry").Require(10,s_biology).Require(25,s_crossbows).OrRequire(25,s_melee).OrRequire(25,s_traps).ImplyLevelReq().Commit();
                    f_weaponsmith = feats.Parameter("Weaponsmith").Require(6,a_int).Require(25,s_mechanics).ImplyLevelReq().Commit();
                    f_bowyer = feats.Parameter("Bowyer").Require(7,a_int).Require(30,s_mechanics).ImplyLevelReq().Commit();
                    f_concussive_shots = feats.Parameter("Concussive Shots").Require(30,s_crossbows).ImplyLevelReq().Commit();
                    f_crippling_strike = feats.Parameter("Crippling Strike").Require(30,s_melee).ImplyLevelReq().Commit();
                    f_escape_artist = feats.Parameter("Escape Artist").Require(7,a_dex).Require(30,s_dodge).ImplyLevelReq().Commit();
                    f_grenadier = feats.Parameter("Grenadier").Require(6,a_dex).Require(40,s_throwing).ImplyLevelReq().Commit();
                    f_meditation = feats.Parameter("Meditation").Require(7,a_wil).Require(5,a_int).Require(1,f_tranquility).Require(50,s_thoughtcontrol).OrRequire(50,s_metathermics).OrRequire(50,s_psychokinesis).ImplyLevelReq().Commit();
                    f_mental_subversion = feats.Parameter("Mental Subversion").Require(6,a_wil).Require(30,s_thoughtcontrol).Require(1, p_neural_overload).OrRequire(1, p_frighten).ImplyLevelReq().Commit();
                    f_thermodynamicity = feats.Parameter("Thermodynamicity").Require(1,p_cryokinesis).Require(1,p_pyrokinesis).Require(30, s_metathermics).ImplyLevelReq().Commit();
                    f_trap_expert = feats.Parameter("Trap Expert").Require(6,a_dex).Require(30,s_traps).ImplyLevelReq().Commit();

                    f_ballistics = feats.Parameter("Ballistics").Require(6,a_int).Require(35,s_tailoring).ImplyLevelReq().Commit();
                    f_cooked_shot = feats.Parameter("Cooked Shot").Require(5,a_dex).Require(35,s_guns).Require(25,s_chemistry).ImplyLevelReq().Commit();
                    f_deflection = feats.Parameter("Deflection").Require(6,a_dex).Require(35,s_melee).ImplyLevelReq().Commit();
                    f_hypertoxicity = feats.Parameter("Hypertoxicity").Require(35,s_biology).ImplyLevelReq().Commit();
                    f_last_stand = feats.Parameter("Last Stand").Require(5, t_level).Require(9,a_con).ImplyLevelReq().Commit();
                    f_neurology = feats.Parameter("Neurology").Require(7,a_int).Require(35,s_biology).ImplyLevelReq().Commit();
                    f_practical_physicist = feats.Parameter("Practical Physicist").Require(7,a_int).Require(35,s_electronics).ImplyLevelReq().Commit();
                    f_pyromaniac = feats.Parameter("Pyromaniac").Require(1,p_pyrokinesis).Require(35,s_metathermics).ImplyLevelReq().Commit();
                    f_salesman = feats.Parameter("Salesman").Require(35,s_mercantile).ImplyLevelReq().Commit();
                    f_snipe = feats.Parameter("Snipe").Require(10,a_per).Require(30,s_stealth).Require(35,s_guns).OrRequire(35,s_crossbows).Require(1,f_aimed_shot).ImplyLevelReq().Commit();
                    f_ambush = feats.Parameter("Ambush").Require(6,a_per).Require(40,s_stealth).ImplyLevelReq().Commit();
                    f_cheap_shots = feats.Parameter("Cheap Shots").Require(6,a_dex).Require(5,a_int).Require(40,s_melee).ImplyLevelReq().Commit();
                    f_expose_weakness = feats.Parameter("Expose Weakness").Require(5,a_int).Require(40,s_melee).ImplyLevelReq().Commit();
                    f_fancy_footwork = feats.Parameter("Fancy Footwork").Require(7, a_agi).Require(40, s_melee).Require(40,s_dodge).ImplyLevelReq().Commit();
                    f_full_auto = feats.Parameter("Full Auto").Require(7,a_str).Require(40,s_guns).ImplyLevelReq().Commit();
                    f_hypothermia = feats.Parameter("Hypothermia").Require(1,p_cryokinesis).OrRequire(1,p_cryostasis).Require(40,s_metathermics).ImplyLevelReq().Commit();
                    f_mad_chemist = feats.Parameter("Mad Chemist").Require(7,a_int).Require(40,s_chemistry).ImplyLevelReq().Commit();
                    f_pinning = feats.Parameter("Pinning").Require(7,a_dex).Require(40,s_throwing).ImplyLevelReq().Commit();
                    f_premeditation = feats.Parameter("Premeditation").Require(6,a_int).Require(1, f_psi_empathy).Require(40,s_thoughtcontrol).OrRequire(40,s_metathermics).OrRequire(40,s_psychokinesis).ImplyLevelReq().Commit();
                    f_psychostatic_electricity = feats.Parameter("Psychostatic Electricity").Require(1,p_electrokinesis).OrRequire(1, p_electrokinetic_imprint).Require(40,s_psychokinesis).ImplyLevelReq().Commit();
                    f_uncanny_dodge = feats.Parameter("Uncanny Dodge").Require(8,a_agi).Require(40,s_dodge).ImplyLevelReq().Commit();
                    f_wrestling = feats.Parameter("Wrestling").Require(40,s_melee).Require(7,a_str).ImplyLevelReq().Commit();

                    f_bone_breaker = feats.Parameter("Bone Breaker").Require(7,a_str).Require(45,s_melee).ImplyLevelReq().Commit();
                    f_cryogenic_induction = feats.Parameter("Cryogenic Induction").Require(1,p_cryostasis).Require(50,s_metathermics).ImplyLevelReq().Commit();
                    f_evasive_maneuvers = feats.Parameter("Evasive Maneuvers").Require(6,a_agi).Require(50,s_evasion).ImplyLevelReq().Commit();
                    f_fatal_throw = feats.Parameter("Fatal Throw").Require(8,a_dex).Require(50,s_throwing).ImplyLevelReq().Commit();
                    f_guard = feats.Parameter("Guard").Require(8,t_level).Require(7,a_str).Require(50,s_melee).ImplyLevelReq().Commit();
                    f_psionic_mania = feats.Parameter("Psionic Mania").Require(8,a_wil).Require(50,s_thoughtcontrol).OrRequire(50,s_metathermics).OrRequire(50,s_psychokinesis).ImplyLevelReq().Commit();
                    f_rapid_fire = feats.Parameter("Rapid Fire").Require(50,s_guns).ImplyLevelReq().Commit();
                    f_ripper = feats.Parameter("Ripper").Require(10,a_dex).Require(5,a_wil).Require(50,s_melee).OrRequire(50,s_throwing).ImplyLevelReq().Commit();
                    f_spec_ops = feats.Parameter("Spec Ops").Require(6,a_agi).Require(50,s_guns).ImplyLevelReq().Commit();
                    f_special_tactics = feats.Parameter("Special Tactics").Require(6,a_int).Require(50,s_crossbows).ImplyLevelReq().Commit();
                    f_taste_for_blood = feats.Parameter("Taste for Blood").Require(50,s_melee).ImplyLevelReq().Commit();
                    f_three_pointer = feats.Parameter("Three Pointer").Require(7,a_dex).Require(50,s_throwing).ImplyLevelReq().Commit();

                    f_cut_throat = feats.Parameter("Cut Throat").Require(10,a_dex).Require(55,s_melee).Require(45,s_stealth).ImplyLevelReq().Commit();
                    f_telekinetic_undulation = feats.Parameter("Telekinetic Undulation").Require(1,p_telekinetic_proxy).Require(5,a_wil).ImplyLevelReq().Commit();
                    f_blitz = feats.Parameter("Blitz").Require(10,t_level).Require(10,a_agi).ImplyLevelReq().Commit();
                    f_concentrated_fire = feats.Parameter("Concentrated Fire").Require(8,a_per).Require(60,s_guns).ImplyLevelReq().Commit();
                    f_sharpshooter = feats.Parameter("Sharpshooter").Require(60, s_guns).OrRequire(60, s_crossbows).Require(1, f_aimed_shot).Require(10, a_per).ImplyLevelReq().Commit();

                    f_combo = feats.Parameter("Combo").Require(8,a_dex).Require(65,s_melee).ImplyLevelReq().Commit();             
                    f_neural_overclocking = feats.Parameter("Neural Overclocking").Require(10,a_wil).Require(1, f_psi_empathy).Require(65,s_metathermics).OrRequire(65,s_thoughtcontrol).OrRequire(65, s_psychokinesis).ImplyLevelReq().Commit();
                    f_execute = feats.Parameter("Execute").Require(70,s_guns).Require(1,f_opportunist).ImplyLevelReq().Commit();

                    f_critical_power = feats.Parameter("Critical Power").Require(75,s_melee).OrRequire(75,s_guns).OrRequire(75,s_crossbows).ImplyLevelReq().Commit();
                    f_deadly_snares = feats.Parameter("Deadly Snares").Require(10,a_per).Require(75,s_crossbows).Require(50,s_traps).ImplyLevelReq().Commit();
                    f_elemental_bolts = feats.Parameter("Elemental Bolts").Require(75,s_crossbows).ImplyLevelReq().Commit();
                
                    f_locus_of_control = feats.Parameter("Locus of Control").Require(10,a_wil).Require(75,s_thoughtcontrol).Require(1,f_psi_empathy).ImplyLevelReq().Commit();
                    f_super_slam = feats.Parameter("Super Slam").Require(10,a_str).Require(75,s_melee).ImplyLevelReq().Commit();
                    f_commando = feats.Parameter("Commando").Require(80,s_guns).ImplyLevelReq().Commit();
                    f_eviscerate = feats.Parameter("Eviscerate").Require(80,s_melee).Require(8,a_dex).ImplyLevelReq().Commit();
                    f_split_spare = feats.Parameter("Split Spare").Require(10,a_dex).Require(80,s_throwing).ImplyLevelReq().Commit();

                    f_heavyweight = feats.Parameter("Heavyweight").Require(10, a_str).Require(100,s_melee).ImplyLevelReq().Commit();

                    f_psi_empathy = feats.Parameter("Psi Empathy", 0).ImplyLevelReq().Commit();
                    f_fisherman = feats.Parameter("Fisherman", 0).ImplyLevelReq().Commit();
                    f_hunter = feats.Parameter("Hunter", 0).ImplyLevelReq().Commit();
                    f_echoing_soliloquy = feats.Parameter("Echoing Soliloquy", 0).ImplyLevelReq().Commit();
                }
                
                return builder.game;
            }
        }
    }
}