﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Samples;
using GammaWorldCharacter.Test.Integration;

namespace GammaWorldCharacter.Test.Integration
{
    /// <summary> 
    /// Autogenerated character test
    /// </summary>
    [TestFixture]
    public class TestLevel02Kitty : CharacterTest
    {
        private Character testCharacter;

        [TestCase(ScoreType.Strength, 8)]
        [TestCase(ScoreType.Constitution, 9)]
        [TestCase(ScoreType.Dexterity, 16)]
        [TestCase(ScoreType.Intelligence, 8)]
        [TestCase(ScoreType.Wisdom, 11)]
        [TestCase(ScoreType.Charisma, 18)]
        [TestCase(ScoreType.Initiative, 5)]
        [TestCase(ScoreType.HitPoints, 26)]
        [TestCase(ScoreType.Bloodied, 13)]
        [TestCase(ScoreType.ArmorClass, 18)]
        [TestCase(ScoreType.Fortitude, 11)]
        [TestCase(ScoreType.Reflex, 17)]
        [TestCase(ScoreType.Will, 16)]
        [TestCase(ScoreType.Speed, 7)]
        [TestCase(ScoreType.Fly, 0)]
        [TestCase(ScoreType.Climb, 0)]
        [TestCase(ScoreType.Swim, 0)]
        [TestCase(ScoreType.SavingThrows, 0)]
        [TestCase(ScoreType.Acrobatics, 5)]
        [TestCase(ScoreType.Athletics, 1)]
        [TestCase(ScoreType.Conspiracy, 1)]
        [TestCase(ScoreType.Insight, 6)]
        [TestCase(ScoreType.Interaction, 6)]
        [TestCase(ScoreType.Mechanics, 1)]
        [TestCase(ScoreType.Nature, 2)]
        [TestCase(ScoreType.Perception, 2)]
        [TestCase(ScoreType.Science, 1)]
        [TestCase(ScoreType.Stealth, 13)]
        [TestCase(ScoreType.Level, 2)]
        [TestCase(ScoreType.OpportunityAttackAttackBonus, 0)]
        [TestCase(ScoreType.OpportunityAttackArmorClassBonus, 0)]
        [TestCase(ScoreType.StrengthCheck, 1)]
        [TestCase(ScoreType.ConstitutionCheck, 1)]
        [TestCase(ScoreType.DexterityCheck, 5)]
        [TestCase(ScoreType.IntelligenceCheck, 1)]
        [TestCase(ScoreType.WisdomCheck, 2)]
        [TestCase(ScoreType.CharismaCheck, 6)]
        [TestCase(ScoreType.FireResistance, 0)]
        [TestCase(ScoreType.ElectricityResistance, 0)]
        [TestCase(ScoreType.ColdResistance, 0)]
        [TestCase(ScoreType.PhysicalResistance, 0)]
        [TestCase(ScoreType.FireVulnerability, 0)]
        public override void TestScore(ScoreType scoreType, int expectedValue)
        {
            base.TestScore(scoreType, expectedValue);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), 0, 8, 1, DiceType.d8, 5, "physical damage.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.VitalityTransfer), 0, 6, 0, 0, 0, "The target is weakened until the end of your next turn. In addition, you or one ally within 5 suares of you gains 4 temporary hit points.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.SlashingClaws), 0, 5, 0, 0, 0, "If one attack hits, the damage is 1d6+7 physical. If both attacks hit, the damage is 2d6+7 physical and the target is blinded until the start of your next turn.")]
        public override void TestAttackPower(Type type, int attack, int attackBonus, int damageDiceCount, DiceType damageDiceType, int damageBonus, string additionalText)
        {
            base.TestAttackPower(type, attack, attackBonus, damageDiceCount, damageDiceType, damageBonus, additionalText);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind), "You heal 13 hit points and gain +2 to all defenses until the end of your next turn.")]
        public override void TestUtilityPower(Type type, string expectedEffect)
        {
            base.TestUtilityPower(type, expectedEffect);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), ActionType.Standard, AttackType.Melee, "1", 64, null, 0, PowerFrequency.AtWill, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind), ActionType.Minor, AttackType.Personal, null, 0, "You heal 13 hit points and gain +2 to all defenses until the end of your next turn.", 1, PowerFrequency.Encounter, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.VitalityTransfer), ActionType.Standard, AttackType.Ranged, "3", 0, null, 0, PowerFrequency.AtWill, PowerSource.Psi, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.SlashingClaws), ActionType.Standard, AttackType.Melee, "1", 64, null, 0, PowerFrequency.AtWill, PowerSource.Bio, null)]
        public override void TestPower(Type type, ActionType actionType, AttackType attackType, string range, DamageTypes damageTypes, string effect,
            EffectTypes effectTypes, PowerFrequency frequency, PowerSource powerSource, string trigger)
        {
            base.TestPower(type, actionType, attackType, range, damageTypes, effect, effectTypes, frequency, powerSource, trigger);
        }

        /// <summary>
        /// Create the test character.
        /// </summary>
        protected override Character Character
        {
            get
            {
                if (testCharacter == null)
                {
                    testCharacter = Level02Characters.Kitty;
                }
                return testCharacter;
            }
        }
    }
}
