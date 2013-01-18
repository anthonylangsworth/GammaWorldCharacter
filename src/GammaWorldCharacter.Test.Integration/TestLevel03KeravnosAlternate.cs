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
using GammaWorldCharacter.Levels;

namespace GammaWorldCharacter.Test.Integration
{
    /// <summary> 
    /// Autogenerated character test
    /// </summary>
    [TestFixture]
    public class TestLevel03KeravnosAlternate : CharacterTest
    {
        private Character testCharacter;

        [TestCase(ScoreType.Strength, 10)]
        [TestCase(ScoreType.Constitution, 13)]
        [TestCase(ScoreType.Dexterity, 10)]
        [TestCase(ScoreType.Intelligence, 18)]
        [TestCase(ScoreType.Wisdom, 16)]
        [TestCase(ScoreType.Charisma, 5)]
        [TestCase(ScoreType.StrengthModifier, 0)]
        [TestCase(ScoreType.ConstitutionModifier, 1)]
        [TestCase(ScoreType.DexterityModifier, 0)]
        [TestCase(ScoreType.IntelligenceModifier, 4)]
        [TestCase(ScoreType.WisdomModifier, 3)]
        [TestCase(ScoreType.CharismaModifier, -3)]
        [TestCase(ScoreType.Initiative, 3)]
        [TestCase(ScoreType.HitPoints, 35)]
        [TestCase(ScoreType.Bloodied, 17)]
        [TestCase(ScoreType.ArmorClass, 20)]
        [TestCase(ScoreType.Fortitude, 14)]
        [TestCase(ScoreType.Reflex, 21)]
        [TestCase(ScoreType.Will, 16)]
        [TestCase(ScoreType.Speed, 6)]
        [TestCase(ScoreType.Fly, 0)]
        [TestCase(ScoreType.Climb, 0)]
        [TestCase(ScoreType.Swim, 0)]
        [TestCase(ScoreType.SavingThrows, 0)]
        [TestCase(ScoreType.Acrobatics, 3)]
        [TestCase(ScoreType.Athletics, 3)]
        [TestCase(ScoreType.Conspiracy, 11)]
        [TestCase(ScoreType.Insight, 6)]
        [TestCase(ScoreType.Interaction, 0)]
        [TestCase(ScoreType.Mechanics, 15)]
        [TestCase(ScoreType.Nature, 6)]
        [TestCase(ScoreType.Perception, 6)]
        [TestCase(ScoreType.Science, 7)]
        [TestCase(ScoreType.Stealth, 3)]
        [TestCase(ScoreType.Level, 3)]
        [TestCase(ScoreType.OpportunityAttackAttackBonus, 0)]
        [TestCase(ScoreType.OpportunityAttackArmorClassBonus, 0)]
        [TestCase(ScoreType.StrengthCheck, 3)]
        [TestCase(ScoreType.ConstitutionCheck, 4)]
        [TestCase(ScoreType.DexterityCheck, 3)]
        [TestCase(ScoreType.IntelligenceCheck, 7)]
        [TestCase(ScoreType.WisdomCheck, 6)]
        [TestCase(ScoreType.CharismaCheck, 0)]
        [TestCase(ScoreType.FireResistance, 0)]
        [TestCase(ScoreType.ElectricityResistance, 10)]
        [TestCase(ScoreType.ColdResistance, 0)]
        [TestCase(ScoreType.PhysicalResistance, 0)]
        [TestCase(ScoreType.FireVulnerability, 0)]
        public override void TestScore(ScoreType scoreType, int expectedValue)
        {
            base.TestScore(scoreType, expectedValue);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), 0, 10, 1, DiceType.d12, 7, "physical damage.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.ElectricBoogaloo), 0, 6, 1, DiceType.d10, 9, "electricity damage and the target takes a -2 penalty to all defenses until the end of your next turn.")]
        public override void TestAttackPower(Type type, int attack, int attackBonus, int damageDiceCount, DiceType damageDiceType, int damageBonus, string additionalText)
        {
            base.TestAttackPower(type, attack, attackBonus, damageDiceCount, damageDiceType, damageBonus, additionalText);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind), "You heal 17 hit points and gain +2 to all defenses until the end of your next turn.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.DoubleTrouble), "You create a duplicate of yourself in an unnoccupied square within 5 squares of you. The duplicate acts in the initiative order directly after you and can take all actions that you can take, except that it can't use doppelganger powers, Alpha Mutation or Omega Tech. Its statistics are the same as yours, except that it only has 1 hit point. Your duplicate disappears when it drops to 0 hit points or at the end of your next turn.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.TwoPlacesAtOnce), "Choose an unoccupied square within 5 squares of you. You simultaneously occupy that square and your current square. Before the start of your next turn, you can teleport to the chosen square as a free action.")]
        public override void TestUtilityPower(Type type, string expectedEffect)
        {
            base.TestUtilityPower(type, expectedEffect);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), ActionType.Standard, AttackType.Melee, "1", 64, null, 0, PowerFrequency.AtWill, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind), ActionType.Minor, AttackType.Personal, null, 0, "You heal 17 hit points and gain +2 to all defenses until the end of your next turn.", 1, PowerFrequency.Encounter, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.DoubleTrouble), ActionType.Standard, AttackType.Personal, null, 64, "You create a duplicate of yourself in an unnoccupied square within 5 squares of you. The duplicate acts in the initiative order directly after you and can take all actions that you can take, except that it can't use doppelganger powers, Alpha Mutation or Omega Tech. Its statistics are the same as yours, except that it only has 1 hit point. Your duplicate disappears when it drops to 0 hit points or at the end of your next turn.", 0, PowerFrequency.AtWill, PowerSource.Dark, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.ElectricBoogaloo), ActionType.Standard, AttackType.Melee, "1", 4, null, 0, PowerFrequency.AtWill, PowerSource.Dark, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.TwoPlacesAtOnce), ActionType.Minor, AttackType.Personal, null, 0, "Choose an unoccupied square within 5 squares of you. You simultaneously occupy that square and your current square. Before the start of your next turn, you can teleport to the chosen square as a free action.", 2, PowerFrequency.Encounter, PowerSource.Dark, null)]
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
                    testCharacter = Level03Characters.KeravnosAlternate;
                }
                return testCharacter;
            }
        }
    }
}