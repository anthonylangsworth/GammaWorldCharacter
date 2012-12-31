using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Fluent;
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
            Target target = new Target(expression, TargetType.YouOrAlly);

            Assert.That(target.TargetType, Is.EqualTo(TargetType.YouOrAlly));
            Assert.That(target.Expression, Is.SameAs(expression));
        }

        [Test]
        public void TestConstructor_NullExpression()
        {
            Assert.That(() => new Target(null, TargetType.YouOrAlly),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("expression"));
        }
    }
}
