using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit
{
    [TestFixture]
    public class TestDiceExtension
    {
        [TestCase(1, "1d4")]
        [TestCase(2, "2d4")]
        public void TestD4(int number, string expectedToString)
        {
            Assert.That(number.D4().ToString(), Is.EqualTo(expectedToString));
        }

        [TestCase(1, "1d6")]
        [TestCase(2, "2d6")]
        public void TestD6(int number, string expectedToString)
        {
            Assert.That(number.D6().ToString(), Is.EqualTo(expectedToString));
        }

        [TestCase(1, "1d8")]
        [TestCase(2, "2d8")]
        public void TestD8(int number, string expectedToString)
        {
            Assert.That(number.D8().ToString(), Is.EqualTo(expectedToString));
        }

        [TestCase(1, "1d10")]
        [TestCase(2, "2d10")]
        public void TestD10(int number, string expectedToString)
        {
            Assert.That(number.D10().ToString(), Is.EqualTo(expectedToString));
        }

        [TestCase(1, "1d12")]
        [TestCase(2, "2d12")]
        public void TestD12(int number, string expectedToString)
        {
            Assert.That(number.D12().ToString(), Is.EqualTo(expectedToString));
        }

        [TestCase(1, "1d20")]
        [TestCase(2, "2d20")]
        public void TestD20(int number, string expectedToString)
        {
            Assert.That(number.D20().ToString(), Is.EqualTo(expectedToString));
        }

        [TestCase(1, "1d100")]
        [TestCase(2, "2d100")]
        public void TestD100(int number, string expectedToString)
        {
            Assert.That(number.D100().ToString(), Is.EqualTo(expectedToString));
        }
    }
}
