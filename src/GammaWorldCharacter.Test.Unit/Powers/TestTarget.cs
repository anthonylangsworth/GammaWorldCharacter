using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Powers
{
    [TestFixture]
    public class TestTarget
    {
        [Test]
        public void TestConstructor()
        {
            EffectExpression expression = new EffectExpression();
            Target target = new Target(expression, TargetType.Enemy, Where.Unspecified);

            Assert.That(target.TargetType, Is.EqualTo(TargetType.Enemy));
            Assert.That(target.Expression, Is.SameAs(expression));
            Assert.That(target.Where, Is.EqualTo(Where.Unspecified));
        }

        [Test]
        public void TestConstructor_NullExpression()
        {
            Assert.That(() => new Target(null, TargetType.Enemy, Where.Unspecified),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("expression"));
        }
    }
}
