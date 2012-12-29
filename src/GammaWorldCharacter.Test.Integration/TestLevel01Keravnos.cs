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
    public class TestLevel01Keravnos: CharacterTest
    {
        private Character testCharacter;

        [TestCase(ScoreType.Strength, 10)]
        [TestCase(ScoreType.Constitution, 13)]
        [TestCase(ScoreType.Dexterity, 10)]
        [TestCase(ScoreType.Intelligence, 18)]
        [TestCase(ScoreType.Wisdom, 16)]
        [TestCase(ScoreType.Charisma, 5)]
        [TestCase(ScoreType.Initiative, 1)]
        [TestCase(ScoreType.HitPoints, 25)]
        [TestCase(ScoreType.Bloodied, 12)]
        [TestCase(ScoreType.ArmorClass, 18)]
        [TestCase(ScoreType.Fortitude, 12)]
        [TestCase(ScoreType.Reflex, 19)]
        [TestCase(ScoreType.Will, 14)]
        [TestCase(ScoreType.Speed, 6)]
        [TestCase(ScoreType.Fly, 0)]
        [TestCase(ScoreType.Climb, 0)]
        [TestCase(ScoreType.Swim, 0)]
        [TestCase(ScoreType.SavingThrows, 0)]
        [TestCase(ScoreType.Acrobatics, 1)]
        [TestCase(ScoreType.Athletics, 1)]
        [TestCase(ScoreType.Conspiracy, 9)]
        [TestCase(ScoreType.Insight, 4)]
        [TestCase(ScoreType.Interaction, -2)]
        [TestCase(ScoreType.Mechanics, 13)]
        [TestCase(ScoreType.Nature, 4)]
        [TestCase(ScoreType.Perception, 4)]
        [TestCase(ScoreType.Science, 5)]
        [TestCase(ScoreType.Stealth, 1)]
        [TestCase(ScoreType.Level, 1)]
        [TestCase(ScoreType.OpportunityAttackAttackBonus, 0)]
        [TestCase(ScoreType.OpportunityAttackArmorClassBonus, 0)]
        [TestCase(ScoreType.StrengthCheck, 1)]
        [TestCase(ScoreType.ConstitutionCheck, 2)]
        [TestCase(ScoreType.DexterityCheck, 1)]
        [TestCase(ScoreType.IntelligenceCheck, 5)]
        [TestCase(ScoreType.WisdomCheck, 4)]
        [TestCase(ScoreType.CharismaCheck, -2)]
        [TestCase(ScoreType.FireResistance, 0)]
        [TestCase(ScoreType.ElectricityResistance, 10)]
        [TestCase(ScoreType.ColdResistance, 0)]
        [TestCase(ScoreType.PhysicalResistance, 0)]
        [TestCase(ScoreType.FireVulnerability, 0)]
        public override void TestScore(ScoreType scoreType, int expectedValue)
        {
            base.TestScore(scoreType, expectedValue);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), 0, 8, 1, DiceType.d12, 5, "physical damage.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.ElectricBoogaloo), 0, 4, 1, DiceType.d10, 5, "electricity damage and the target takes a -2 penalty to all defenses until the end of your next turn.")]
        public override void TestAttackPower(Type type, int attack, int attackBonus, int damageDiceCount, DiceType damageDiceType, int damageBonus, string additionalText)
        {
            base.TestAttackPower(type, attack, attackBonus, damageDiceCount, damageDiceType, damageBonus, additionalText);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind))]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.DoubleTrouble))]
        public override void TestUtilityPower(Type type)
        {
            base.TestUtilityPower(type);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), ActionType.Standard, AttackType.Melee, "1", 64, null, 0, PowerFrequency.AtWill, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind), ActionType.Minor, AttackType.Personal, null, 0, "You heal 12 hit points and gain +2 to all defenses until the end of your next turn.", 1, PowerFrequency.Encounter, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.DoubleTrouble), ActionType.Standard, AttackType.Personal, null, 64, "You create a duplicate of yourself in an unnoccupied square within 5 squares of you. The duplicate acts in the initiative order directly after you and can take all actions that you can take, except that it can't use doppelganger powers, Alpha Mutation or Omega Tech. Its statistics are the same as yours, except that it only has 1 hit point. Your duplicate disappears when it drops to 0 hit points or at the end of your next turn.", 0, PowerFrequency.AtWill, PowerSource.Dark, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.ElectricBoogaloo), ActionType.Standard, AttackType.Melee, "1", 4, null, 0, PowerFrequency.AtWill, PowerSource.Dark, null)]
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
                    testCharacter = Level01Characters.Keravnos;
                }
                return testCharacter;
            }
        }
    }
}
