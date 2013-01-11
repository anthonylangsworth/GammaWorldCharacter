using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Gear
{
    [TestFixture]
    public class TestExplorersKit
    {
        [Test]
        public void TestContents()
        {
            Assert.That(ExplorersKit.Contents,
                Is.EquivalentTo(new []
                    {
                        new Item("Backpack", Slot.None),
                        new Item("Bedroll", Slot.None),
                        new Item("Canteen", Slot.None),
                        new Item("Flint and steel", Slot.None),
                        new Item("Trail Rations (10 days)", Slot.None),
                        new Item("Rope (100 ft)", Slot.None)
                    }));
        }
    }
}
