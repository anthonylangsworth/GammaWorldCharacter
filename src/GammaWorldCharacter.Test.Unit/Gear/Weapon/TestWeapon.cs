using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Test.Unit.Gear.Weapon
{
    [TestFixture]
    public class TestWeapon
    {
        [TestCase(WeaponWeight.Light, new []{ScoreType.Dexterity, ScoreType.Intelligence})]
        [TestCase(WeaponWeight.Heavy, new []{ScoreType.Strength, ScoreType.Constitution})]
        public void TestWeaponAbilityScore(WeaponWeight weight, IEnumerable<ScoreType> expectedAbilityScores)
        {
            Assert.That(new MeleeWeapon(WeaponHandedness.TwoHanded, weight).BasicAttackAbilityScores, 
                Is.EquivalentTo(expectedAbilityScores));
        }
    }
}
