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
    public class TestLightArmor
    {
        [Test]
        public void TestConstructor()
        {
            LightArmor lightArmor = new LightArmor();

            Assert.That(lightArmor.Name, Is.EqualTo("Light Armor"));
            Assert.That(lightArmor.ArmorBonus, Is.EqualTo(3));
            Assert.That(lightArmor.SpeedPenalty, Is.EqualTo(0));
            Assert.That(lightArmor.Slot, Is.EqualTo(Slot.Body));
        }
    }
}
