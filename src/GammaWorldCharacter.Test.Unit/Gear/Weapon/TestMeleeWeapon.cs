using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Test.Unit.Gear.Weapon
{
    [TestFixture]
    public class TestMeleeWeapon
    {
        [TestCase(WeaponHandedness.OneHanded, WeaponWeight.Light, "Light one-handed melee weapon")]
        [TestCase(WeaponHandedness.TwoHanded, WeaponWeight.Light, "Light two-handed melee weapon")]
        [TestCase(WeaponHandedness.OneHanded, WeaponWeight.Heavy, "Heavy one-handed melee weapon")]
        [TestCase(WeaponHandedness.TwoHanded, WeaponWeight.Heavy, "Heavy two-handed melee weapon")]
        public void TestName(WeaponHandedness handedness, WeaponWeight weight, string expectedName)
        {
            Assert.That(new MeleeWeapon(handedness, weight).Name, Is.EqualTo(expectedName));
        }

        [TestCase(WeaponWeight.Heavy, 2)]
        [TestCase(WeaponWeight.Light, 3)]
        public void TestAccuracyBonus(WeaponWeight weight, int expectedBonus)
        {
            Assert.That(new MeleeWeapon(WeaponHandedness.OneHanded, weight).AccuracyBonus, Is.EqualTo(expectedBonus));
        }

        [TestCase(WeaponHandedness.OneHanded, WeaponWeight.Light, 1, DiceType.d8)]
        [TestCase(WeaponHandedness.TwoHanded, WeaponWeight.Light, 1, DiceType.d12)]
        [TestCase(WeaponHandedness.OneHanded, WeaponWeight.Heavy, 1, DiceType.d10)]
        [TestCase(WeaponHandedness.TwoHanded, WeaponWeight.Heavy, 2, DiceType.d8)]
        public void TestExpectedDamageDice(WeaponHandedness handedness, WeaponWeight weight, int expectedNumber, DiceType expectedDiceType)
        {
            Assert.That(new MeleeWeapon(handedness, weight).Damage, Is.EqualTo(new Dice(expectedNumber, expectedDiceType)));
        }
    }
}
