using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit
{
    [TestFixture]
    public class TestDice
    {
        [Test]
        public void TestConstructor_ZeroNumber()
        {
            Assert.That(() => new Dice(0, DiceType.d4),
                        Throws.ArgumentException.And.Property("ParamName").EqualTo("number"));
        }

        [Test]
        public void TestConstructor_NegativeNumber()
        {
            Assert.That(() => new Dice(-1, DiceType.d4),
                        Throws.ArgumentException.And.Property("ParamName").EqualTo("number"));
        }

        [TestCase(1, DiceType.d4, "1d4", 1, 4)]
        [TestCase(2, DiceType.d4, "2d4", 2, 8)]
        [TestCase(3, DiceType.d4, "3d4", 3, 12)]
        [TestCase(1, DiceType.d6, "1d6", 1, 6)]
        [TestCase(1, DiceType.d8, "1d8", 1, 8)]
        [TestCase(1, DiceType.d10, "1d10", 1, 10)]
        [TestCase(1, DiceType.d12, "1d12", 1, 12)]
        [TestCase(1, DiceType.d20, "1d20", 1, 20)]
        [TestCase(1, DiceType.d100, "1d100", 1, 100)]
        public void TestRepresentation(int number, DiceType diceType, string expectedToString, int minRoll, int maxRoll)
        {
            Dice dice = new Dice(number, diceType);

            Assert.That(dice.ToString(), Is.EqualTo(expectedToString));
            Assert.That(dice.MinRoll, Is.EqualTo(minRoll), "Incorrect minimum roll");
            Assert.That(dice.MaxRoll, Is.EqualTo(maxRoll), "Incorrect maximum roll");
        }
    }
}
