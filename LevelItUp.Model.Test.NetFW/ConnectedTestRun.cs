using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LevelItUp.Model.d_Underrail;

namespace LevelItUp.Model.Test.NetFW
{
    [TestFixture]
    public class MyFirstUnderrailBuild : BaseTest
    {
        [Test, Order(00100)]
        public void c00100__Level_1_Unfinished_Attributes()
        {
            OnLevel = 1;
            AssertParamEquals(a_str, value: 3);
            AssertLevelStatus();
            AssertParamChEqul(a_str, +3, 6);
            AssertLevelStatus(t_attributes, LevelStat.TooFewSpent);
        }
        [Test, Order(00200)]
        public void c00200__Level_1_Finished_Attributes()
        {
            AssertParamChEqul(a_agi, +4, 7);
            AssertParamChEqul(a_wil, +5, 8);
            AssertParamChEqul(a_per, +7, 10);
            AssertLevelStatus(t_attributes, LevelStat.Ok);
        }
        [Test, Order(00300)]
        public void c00300__Level_1_SkillFew_FeatFew()
        {
            AssertParamChEqul(s_psychokinesis, +15, 15);
            AssertParamChEqul(s_thoughtcontrol, +15, 15);
            AssertParamChEqul(s_metathermics, +15, 15);
            AssertParamChEqul(f_paranoia, +1, 1);

            AssertLevelStatus
            (
                (t_attributes, LevelStat.Ok),
                (t_skills, LevelStat.TooFewSpent),
                (t_feats, LevelStat.TooFewSpent)
            );
        }
        [Test, Order(00400)]
        public void c00400__Level_1_SkillMany_FeatFew()
        {
            // It's ok even on lv 1 bc who knows what path you may wanna take! 
            AssertParamChEqul(s_throwing, +10, 10);
            AssertParamChEqul(s_guns, 15, 15);
            AssertParamChEqul(s_dodge, +15, 15);
            AssertParamChEqul(s_evasion, +15, 15);
            AssertParamChEqul(s_hacking, +10, 10);
            AssertParamChEqul(s_lockpicking, +10, 10);
            AssertParamChEqul(s_persuasion, +1, 1);

            AssertLevelStatus
            (
                (t_attributes, LevelStat.Ok),
                (t_skills, LevelStat.TooManySpent),
                (t_feats, LevelStat.TooFewSpent)
            );
        }
        [Test, Order(00500)]
        public void c00500__Level_1_SkillsBounded_0_15()
        {
            AssertParamChange(s_mercantile, -1, false);
            AssertParamChange(s_mercantile, +16, false);
            AssertParamEquals(s_mercantile, 0);
        }
        [Test, Order(00600)]
        public void c00600__Level_1_AttrBounded_3_10()
        {
            AssertParamChange(a_con, -1, false);
            AssertParamChange(a_con, +8, false);
            AssertParamEquals(a_con, 3);
        }
        [Test, Order(00700)]
        public void c00700__Level_1_FeatBounded_0_1()
        {
            AssertParamChange(f_pack_rathound, -1, false);
            AssertParamChange(f_pack_rathound, +2, false);
            AssertParamEquals(f_pack_rathound, 0);
        }
        [Test, Order(00800)]
        public void c00800__Level_1_Finish_Choosing_AimedShot()
        {
            AssertParamChEqul(s_persuasion, -1, 0);
            AssertParamChEqul(f_aimed_shot, +1, 1);
            AssertParamChange(f_heavyweight, +1, false); // not met at level possible
            AssertParamChEqul(f_psi_empathy, +1, 1); // free
            AssertParamChange(f_psi_empathy, +1, false); // ..but you cant have 2

            AssertLevelStatus
            (
                (t_attributes, LevelStat.Ok),
                (t_skills, LevelStat.Ok),
                (t_feats, LevelStat.Ok)
            );
        }
        [Test, Order(00850)]
        public void c00850__Level_2_3_Psychosis_Tranquility_Lockuout()
        {
            OnLevel = 2;
            AssertLevelStatus((t_feats, LevelStat.None)); // need to spend 1 feat
            AssertParamChEqul(f_psychosis, +1, 1);
            AssertLevelStatus((t_feats, LevelStat.Ok)); // feats are ok..
            OnLevel = 3;
            AssertParamChEqul(f_tranquility, +1, 1);  // it'll let you do it but expect you to remove one or other
            AssertLevelStatus((t_feats, LevelStat.RequirmentsNotMet)); // lockout with tranquilitty!
            AssertParamNeeded(f_psychosis, f_tranquility, -1);
            AssertParamNeeded(f_tranquility, f_psychosis, -1);
            AssertParamChEqul(f_tranquility, -1, 0);
            OnLevel = 2;
            AssertParamChEqul(f_psychosis, -1, 0);
            AssertLevelStatus();
        }
        [Test, Order(00900)]
        public void c00900__Level_2_to_10_and_SharpShooter_Dependency()
        {
            var sPump = new[] { s_guns, s_metathermics, s_dodge, s_evasion,
                s_hacking, s_lockpicking, s_thoughtcontrol, s_psychokinesis};
            var fChoose = new[] { f_psychosis, f_ninja_looter, f_snooping, f_nimble,
                f_kneecap_shot, f_recklessness, f_rapid_fire, f_quick_pockets, f_sharpshooter };

            foreach (var p in new[] { p_neural_overload, p_telekinetic_punch, p_cryokinesis })
                AssertParamChange(p, +1);

            for (int i = 2; i <= 10; i++)
            {
                OnLevel = i;
                if (i % 4 == 0) AssertParamChange(a_per, +1);
                foreach (var s in sPump) AssertParamChange(s, +5);
                AssertParamChange(fChoose[i - 2], +1);

                AssertLevelStatus
                (
                    (t_attributes, LevelStat.Ok),
                    (t_skills, LevelStat.Ok),
                    (t_feats, LevelStat.Ok)
                );
            }
            foreach (var p in new[] 
                {
                    p_frighten, p_billocation, p_mental_breakdown, p_psi_cognitive_interruption,
                    p_enrage,
                    p_force_field, p_electrokinesis, p_force_emission, p_disruptive_field,
                    p_electrokinetic_imprint,
                    p_pyrokinesis, p_cryostasis, p_pyrokinetic_stream, p_cryokinetic_orb,
                    p_thermodynamic_destabilisation
                })
                
                AssertParamChange(p, +1);

        }
        [Test, Order(01000)]
        public void c01000__Level_10_to_15_BoredNow_CheckCant_26()
        {
            var sPump = new[] { s_guns, s_metathermics, s_dodge, s_evasion,
                s_hacking, s_lockpicking, s_thoughtcontrol, s_psychokinesis};
            var fChoose = new[]
            {
                f_mental_subversion, f_hypothermia, f_psychostatic_electricity,
                f_cryogenic_induction, f_suppressive_fire, f_psionic_mania,
                f_telekinetic_undulation, f_neural_overclocking, f_critical_power,
                f_pack_rathound, f_opportunist,f_hit_and_run ,
                f_locus_of_control, f_cerebral_trauma, f_thermodynamicity
            };
            for (int i = 11; i<=25; i++)
            {
                OnLevel = i;

                if (i==12)
                    foreach (var p in new[] { p_psuedo_spatial_projection, p_telekinetic_proxy, p_cryo_shield,
                                              p_neurovisual_disruption, p_implosion, p_exothermic_aura })
                        AssertParamChange(p, +1);

                if (i % 4 == 0) AssertParamChange(a_per, +1);
                foreach (var s in sPump) AssertParamChange(s, +5);
                AssertParamChange(fChoose[i - 11], +1);
            }
            AssertParamChange(a_wil, +1, false, 26); // NO lol
        }
    }
}