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
    public class TestLevel03KittyAlternate : CharacterTest
    {
        private Character testCharacter;

        [TestCase(ScoreType.Strength, 8)]
        [TestCase(ScoreType.Constitution, 9)]
        [TestCase(ScoreType.Dexterity, 16)]
        [TestCase(ScoreType.Intelligence, 8)]
        [TestCase(ScoreType.Wisdom, 11)]
        [TestCase(ScoreType.Charisma, 18)]
        [TestCase(ScoreType.StrengthModifier, -1)]
        [TestCase(ScoreType.ConstitutionModifier, -1)]
        [TestCase(ScoreType.DexterityModifier, 3)]
        [TestCase(ScoreType.IntelligenceModifier, -1)]
        [TestCase(ScoreType.WisdomModifier, 0)]
        [TestCase(ScoreType.CharismaModifier, 4)]
        [TestCase(ScoreType.Initiative, 6)]
        [TestCase(ScoreType.HitPoints, 31)]
        [TestCase(ScoreType.Bloodied, 15)]
        [TestCase(ScoreType.ArmorClass, 19)]
        [TestCase(ScoreType.Fortitude, 12)]
        [TestCase(ScoreType.Reflex, 18)]
        [TestCase(ScoreType.Will, 17)]
        [TestCase(ScoreType.Speed, 7)]
        [TestCase(ScoreType.Fly, 0)]
        [TestCase(ScoreType.Climb, 0)]
        [TestCase(ScoreType.Swim, 0)]
        [TestCase(ScoreType.SavingThrows, 0)]
        [TestCase(ScoreType.Acrobatics, 6)]
        [TestCase(ScoreType.Athletics, 2)]
        [TestCase(ScoreType.Conspiracy, 2)]
        [TestCase(ScoreType.Insight, 7)]
        [TestCase(ScoreType.Interaction, 7)]
        [TestCase(ScoreType.Mechanics, 2)]
        [TestCase(ScoreType.Nature, 3)]
        [TestCase(ScoreType.Perception, 3)]
        [TestCase(ScoreType.Science, 2)]
        [TestCase(ScoreType.Stealth, 14)]
        [TestCase(ScoreType.Level, 3)]
        [TestCase(ScoreType.OpportunityAttackAttackBonus, 0)]
        [TestCase(ScoreType.OpportunityAttackArmorClassBonus, 0)]
        [TestCase(ScoreType.StrengthCheck, 2)]
        [TestCase(ScoreType.ConstitutionCheck, 2)]
        [TestCase(ScoreType.DexterityCheck, 6)]
        [TestCase(ScoreType.IntelligenceCheck, 2)]
        [TestCase(ScoreType.WisdomCheck, 3)]
        [TestCase(ScoreType.CharismaCheck, 7)]
        [TestCase(ScoreType.FireResistance, 0)]
        [TestCase(ScoreType.ElectricityResistance, 0)]
        [TestCase(ScoreType.ColdResistance, 0)]
        [TestCase(ScoreType.PhysicalResistance, 0)]
        [TestCase(ScoreType.FireVulnerability, 0)]
        public override void TestScore(ScoreType scoreType, int expectedValue)
        {
            base.TestScore(scoreType, expectedValue);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), 0, 9, 1, DiceType.d8, 6, "physical damage.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.VitalityTransfer), 0, 7, 0, 0, 0, "The target is weakened until the end of your next turn. In addition, you or one ally within 5 suares of you gains 4 temporary hit points.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.SlashingClaws), 0, 6, 0, 0, 0, "If one attack hits, the damage is 1d6+9 physical. If both attacks hit, the damage is 2d6+9 physical and the target is blinded until the start of your next turn.")]
        public override void TestAttackPower(Type type, int attack, int attackBonus, int damageDiceCount, DiceType damageDiceType, int damageBonus, string additionalText)
        {
            base.TestAttackPower(type, attack, attackBonus, damageDiceCount, damageDiceType, damageBonus, additionalText);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind), "You heal 15 hit points and gain +2 to all defenses until the end of your next turn.")]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.Pounce), "You jump 7 squares, either vertically or horizontally.")]
        public override void TestUtilityPower(Type type, string expectedEffect)
        {
            base.TestUtilityPower(type, expectedEffect);
        }

        [TestCase(typeof(GammaWorldCharacter.Powers.BasicAttack), ActionType.Standard, AttackType.Melee, "1", 64, null, 0, PowerFrequency.AtWill, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.SecondWind), ActionType.Minor, AttackType.Personal, null, 0, "You heal 15 hit points and gain +2 to all defenses until the end of your next turn.", 1, PowerFrequency.Encounter, PowerSource.None, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.VitalityTransfer), ActionType.Standard, AttackType.Ranged, "3", 0, null, 0, PowerFrequency.AtWill, PowerSource.Psi, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.SlashingClaws), ActionType.Standard, AttackType.Melee, "1", 64, null, 0, PowerFrequency.AtWill, PowerSource.Bio, null)]
        [TestCase(typeof(GammaWorldCharacter.Powers.Origins.Pounce), ActionType.Move, AttackType.Personal, null, 0, "You jump 7 squares, either vertically or horizontally.", 0, PowerFrequency.Encounter, PowerSource.Bio, null)]
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
                    testCharacter = Level03Characters.KittyAlternate;
                }
                return testCharacter;
            }
        }
    }
}
