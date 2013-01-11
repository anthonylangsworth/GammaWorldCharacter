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
    public class TestHeavyArmor
    {
        [Test]
        public void TestConstructor()
        {
            HeavyArmor heavyArmor = new HeavyArmor();

            Assert.That(heavyArmor.Name, Is.EqualTo("Heavy Armor"));
            Assert.That(heavyArmor.ArmorBonus, Is.EqualTo(7));
            Assert.That(heavyArmor.SpeedPenalty, Is.EqualTo(-1));
            Assert.That(heavyArmor.Slot, Is.EqualTo(Slot.Body));
        }
    }
}
