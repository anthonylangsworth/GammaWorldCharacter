using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Fluent;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Powers
{
    [TestFixture]
    public class TestDiceDamageEffect
    {
        [Test]
        public void TestConstructor()
        {
            EffectExpression expression = new EffectExpression();
            Dice dice = 1.D20();
            Target target = new Target(expression, TargetType.Ally, Where.WithinSquares(5, Of.Target));
            DiceDamageEffect effect = new DiceDamageEffect(target, dice);

            Assert.That(effect.Expression, Is.SameAs(expression));
            Assert.That(effect.Target, Is.SameAs(target));
            Assert.That(effect.Dice, Is.SameAs(dice));
        }

        [Test]
        public void TestConstructor_NullTarget()
        {
            Assert.That(() => new DiceDamageEffect(null, 1.D12()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("target"));
        }

        [Test]
        public void TestConstructor_NullDice()
        {
            Assert.That(() => new DiceDamageEffect(new Target(new EffectExpression(), TargetType.Ally, Where.WithinSquares(5, Of.Target)), null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("dice"));
        }
    }
}
