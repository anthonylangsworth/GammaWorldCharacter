using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Gear.Armor
{
    [TestFixture]
    public class TestShield
    {
        [Test]
        public void TestConstructor()
        {
            Shield shield = new Shield();

            Assert.That(shield.Name, Is.EqualTo("Shield"));
            Assert.That(shield.ArmorBonus, Is.EqualTo(1));
            Assert.That(shield.SpeedPenalty, Is.EqualTo(0));
            Assert.That(shield.Slot, Is.EqualTo(Slot.Weapon));
        }
    }
}
