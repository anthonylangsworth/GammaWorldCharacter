using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Test.Unit.Origins;

namespace GammaWorldCharacter.Test.Unit.Powers
{
    /// <summary>
    /// See table on p74.
    /// </summary>
    [TestFixture]
    public class TestBasicAttack
    {
        [Test]
        [TestCase(10, 10, 10, 10, WeaponHandedness.OneHanded, WeaponWeight.Light, 4, 1, DiceType.d8, 1)]
        [TestCase(10, 10, 12, 10, WeaponHandedness.OneHanded, WeaponWeight.Light, 5, 1, DiceType.d8, 2)]
        [TestCase(10, 10, 10, 12, WeaponHandedness.OneHanded, WeaponWeight.Light, 5, 1, DiceType.d8, 2)]
        [TestCase(12, 10, 10, 10, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 4, 1, DiceType.d10, 2)]
        [TestCase(10, 12, 10, 10, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 4, 1, DiceType.d10, 2)]
        [TestCase(14, 10, 10, 10, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 5, 1, DiceType.d10, 3)]
        [TestCase(10, 10, 10, 10, WeaponHandedness.TwoHanded, WeaponWeight.Light, 4, 1, DiceType.d12, 1)]
        [TestCase(10, 10, 10, 10, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 3, 2, DiceType.d8, 1)]
        public void Test_BasicAttack_AbilityScoreMeleeWeaponCombinations(int strength, int constitution, int dexterity, int intelligence, 
            WeaponHandedness handedness, WeaponWeight weight, int expectedAttackBonus, int expectedDamageDiceCount, DiceType expectedDamageDiceType, int expectedDamageBonus)
        {
            Character character;
            BasicAttack basicAttack;

            character = new Character(new int[]{strength, constitution, dexterity, intelligence},
                new NullOrigin(ScoreType.Wisdom), new NullOrigin(ScoreType.Charisma), ScoreType.Acrobatics);
            character.SetHeldItem(Hand.Main, new MeleeWeapon(handedness, weight));
            character.Update();

            basicAttack = character.GetPowers().First(x => x is BasicAttack) as BasicAttack;
            Assert.That(basicAttack, !Is.Null, "Basic Attack is null");
            Assert.That(basicAttack.Attacks.Count, Is.EqualTo(1), "Incorrect number of Basic Attack attacks");
            Assert.That(basicAttack.Attacks[0].AttackBonus.Total, Is.EqualTo(expectedAttackBonus), 
                string.Format("Incorrect Basic Attack attack bonus: {0}", basicAttack.Attacks[0].AttackBonus));
            Assert.That(basicAttack.Attacks[0].Damage.Dice, Is.EqualTo(new Dice(expectedDamageDiceCount, expectedDamageDiceType)), 
                "Incorrect Basic Attack damage");
            Assert.That(basicAttack.Attacks[0].DamageBonus.Total, Is.EqualTo(expectedDamageBonus),
                string.Format("Incorrect Basic Attack damage bonus: {0}", basicAttack.Attacks[0].DamageBonus));
        }

        [Test]
        [TestCase(10, 10, 10, 10, RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Light, 4, 1, DiceType.d8, 1, 5)]
        [TestCase(10, 10, 12, 10, RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Light, 5, 1, DiceType.d8, 2, 5)]
        [TestCase(10, 10, 10, 12, RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Light, 5, 1, DiceType.d8, 2, 5)]
        [TestCase(12, 10, 10, 10, RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 4, 1, DiceType.d10, 2, 5)]
        [TestCase(10, 12, 10, 10, RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 4, 1, DiceType.d10, 2, 5)]
        [TestCase(14, 10, 10, 10, RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 5, 1, DiceType.d10, 3, 5)]
        [TestCase(10, 10, 10, 10, RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light, 4, 1, DiceType.d12, 1, 10)]
        [TestCase(10, 10, 10, 10, RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 3, 2, DiceType.d8, 1, 10)]
        [TestCase(10, 10, 10, 10, RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Light, 5, 1, DiceType.d8, 1, 10)]
        [TestCase(10, 10, 10, 10, RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Light, 5, 1, DiceType.d12, 1, 20)]
        [TestCase(10, 10, 10, 10, RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 3, 2, DiceType.d6, 1, 10)]
        [TestCase(10, 10, 10, 10, RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 3, 2, DiceType.d10, 1, 20)]
        public void Test_BasicAttack_AbilityScoreRangedWeaponCombinations(int strength, int constitution, int dexterity, int intelligence,
            RangedType type, WeaponHandedness handedness, WeaponWeight weight, int expectedAttackBonus, int expectedDamageDiceCount, 
            DiceType expectedDamageDiceType, int expectedDamageBonus, int expectedRange)
        {
            Character character;
            BasicAttack basicAttack;

            character = new Character(new int[] { strength, constitution, dexterity, intelligence },
                new NullOrigin(ScoreType.Wisdom), new NullOrigin(ScoreType.Charisma), ScoreType.Acrobatics);
            character.SetHeldItem(Hand.Main, new RangedWeapon(type, handedness, weight));
            character.Update();

            basicAttack = character.GetPowers().First(x => x is BasicAttack) as BasicAttack;
            Assert.That(basicAttack, !Is.Null, "Basic Attack is null");
            Assert.That(basicAttack.Attacks.Count, Is.EqualTo(1), "Incorrect number of Basic Attack attacks");
            Assert.That(basicAttack.Attacks[0].AttackBonus.Total, Is.EqualTo(expectedAttackBonus),
                string.Format("Incorrect Basic Attack attack bonus: {0}", basicAttack.Attacks[0].AttackBonus));
            Assert.That(basicAttack.Attacks[0].Damage.Dice, Is.EqualTo(new Dice(expectedDamageDiceCount, expectedDamageDiceType)),
                "Incorrect Basic Attack damage");
            Assert.That(basicAttack.Attacks[0].DamageBonus.Total, Is.EqualTo(expectedDamageBonus),
                string.Format("Incorrect Basic Attack damage bonus: {0}", basicAttack.Attacks[0].DamageBonus));
            Assert.That(basicAttack.AttackTypeAndRange.AttackType, Is.EqualTo(AttackType.Ranged), 
                "Incorrect Basic Attack attack type");
            Assert.That(basicAttack.AttackTypeAndRange.Range, Is.EqualTo(expectedRange.ToString()),
                "Incorrect Basic Attack range");
        }
    }
}
