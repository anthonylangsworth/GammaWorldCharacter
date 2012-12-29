using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Test.Unit.Gear.Weapon
{
    [TestFixture]
    public class TestRangedWeapon
    {
        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Light, "Light one-handed ranged weapon")]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Light, "Light one-handed gun")]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light, "Light two-handed ranged weapon")]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Light, "Light two-handed gun")]
        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy, "Heavy one-handed ranged weapon")]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Heavy, "Heavy one-handed gun")]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, "Heavy two-handed ranged weapon")]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, "Heavy two-handed gun")]
        public void TestName(RangedType rangedType, WeaponHandedness handedness, WeaponWeight weight, string expectedName)
        {
            Assert.That(new RangedWeapon(rangedType, handedness, weight).Name, Is.EqualTo(expectedName));
        }

        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Light, 3)]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Light, 4)]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light, 3)]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Light, 4)]
        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 2)]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 2)]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 2)]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 2)]
        public void TestAccuracyBonus(RangedType rangedType, WeaponHandedness handedness, WeaponWeight weight, int expectedAccuracyBonus)
        {
            Assert.That(new RangedWeapon(rangedType, handedness, weight).AccuracyBonus, Is.EqualTo(expectedAccuracyBonus));
        }

        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Light, 1, DiceType.d8)]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Light, 1, DiceType.d8)]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light, 1, DiceType.d12)]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Light, 1, DiceType.d12)]
        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 1, DiceType.d10)]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 2, DiceType.d6)]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 2, DiceType.d8)]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 2, DiceType.d10)]
        public void TestExpectedDamageDice(RangedType rangedType, WeaponHandedness handedness, WeaponWeight weight, int expectedNumber, DiceType expectedDiceType)
        {
            Assert.That(new RangedWeapon(rangedType, handedness, weight).Damage, Is.EqualTo(new Dice(expectedNumber, expectedDiceType)));
        }

        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Light, 5)]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Light, 10)]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light, 10)]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Light, 20)]
        [TestCase(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 5)]
        [TestCase(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Heavy, 10)]
        [TestCase(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 10)]
        [TestCase(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 20)]
        public void TestExpectedRange(RangedType rangedType, WeaponHandedness handedness, WeaponWeight weight, int expectedRange)
        {
            Assert.That(new RangedWeapon(rangedType, handedness, weight).Range, Is.EqualTo(expectedRange));
        }
    }
}
