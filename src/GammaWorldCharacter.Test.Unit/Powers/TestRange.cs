using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Powers.Effects;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Powers
{
    [TestFixture]
    public class TestRange
    {
        [Test]
        public void TestPersonal_NullName()
        {
            Assert.That(() => Range.Personal(null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));
        }

        [Test]
        public void TestPersonal()
        {
            AttackTypeAndRange attackTypeAndRange = Range.Personal("test");

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Personal));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo(null));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Personal"));
        }


        [Test]
        public void TestMeleeWeapon_NullName()
        {
            Assert.That(() => Range.MeleeWeapon(null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));
        }

        [Test]
        public void TestMeleeWeapon()
        {
            AttackTypeAndRange attackTypeAndRange = Range.MeleeWeapon("test");

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Melee));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo("weapon"));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Melee weapon"));
        }

        [Test]
        public void TestMelee_NullName()
        {
            Assert.That(() => Range.Melee(null, 1),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));
        }

        [Test]
        public void TestMelee_ZeroRange()
        {
            Assert.That(() => Range.Melee("test", 0),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("range"));
        }

        [Test]
        public void TestMelee_NegativeRange()
        {
            Assert.That(() => Range.Melee("test", -1),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("range"));
        }

        [Test]
        public void TestMelee()
        {
            AttackTypeAndRange attackTypeAndRange = Range.Melee("test", 2);

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Melee));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo("2"));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Melee 2"));
        }

        [Test]
        public void TestRangedWeapon_NullName()
        {
            Assert.That(() => Range.RangedWeapon(null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));
        }

        [Test]
        public void TestRangedWeapon()
        {
            AttackTypeAndRange attackTypeAndRange = Range.RangedWeapon("test");

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Ranged));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo("weapon"));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Ranged weapon"));
        }

        [Test]
        public void TestRanged_Null()
        {
            Assert.That(() => Range.Ranged(null, 1),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));
        }

        [Test]
        public void TestRanged_ZeroRange()
        {
            Assert.That(() => Range.Ranged("test", 0),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("range"));
        }

        [Test]
        public void TestRanged_NegativeRange()
        {
            Assert.That(() => Range.Ranged("test", -1),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("range"));
        }

        [Test]
        public void TestRanged()
        {
            AttackTypeAndRange attackTypeAndRange = Range.Ranged("test", 2);

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Ranged));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo("2"));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Ranged 2"));
        }

        [Test]
        public void TestCloseBurst_NullName()
        {
            Assert.That(() => Range.CloseBurst(null, 1),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));           
        }

        [Test]
        public void TestCloseBurst_ZeroSize()
        {
            Assert.That(() => Range.CloseBurst("test", 0),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("size"));
        }

        [Test]
        public void TestCloseBurst_NegativeSize()
        {
            Assert.That(() => Range.CloseBurst("test", -1),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("size"));
        }

        [Test]
        public void TestCloseBurst()
        {
            AttackTypeAndRange attackTypeAndRange = Range.CloseBurst("test", 2);

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Close));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo("burst 2"));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Close burst 2"));            
        }

        [Test]
        public void TestCloseBlast_NullName()
        {
            Assert.That(() => Range.CloseBlast(null, 1),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));
        }

        [Test]
        public void TestCloseBlast_ZeroSize()
        {
            Assert.That(() => Range.CloseBlast("test", 0),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("size"));
        }

        [Test]
        public void TestCloseBlast_NegativeSize()
        {
            Assert.That(() => Range.CloseBlast("test", -1),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("size"));
        }

        [Test]
        public void TestCloseBlast()
        {
            AttackTypeAndRange attackTypeAndRange = Range.CloseBlast("test", 2);

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Close));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo("blast 2"));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Close blast 2"));
        }

        [Test]
        public void TestAreaBurst_NullName()
        {
            Assert.That(() => Range.AreaBurst(null, 1, Where.WithinSquares(2, Of.Target)),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("powerName"));
        }

        [Test]
        public void TestAreaBurst_ZeroSize()
        {
            Assert.That(() => Range.AreaBurst("test", 0, Where.WithinSquares(2, Of.Target)),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("size"));
        }

        [Test]
        public void TestAreaBurst_NegativeSize()
        {
            Assert.That(() => Range.AreaBurst("test", -1, Where.WithinSquares(2, Of.Target)),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("size"));
        }


        [Test]
        public void TestAreaBurst_NullWhere()
        {
            Assert.That(() => Range.AreaBurst("test", 1, null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("where"));
        }

        [Test]
        public void TestAreaBurst()
        {
            AttackTypeAndRange attackTypeAndRange = Range.AreaBurst("test", 2, Where.WithinSquares(2, Of.You));

            Assert.That(attackTypeAndRange.AttackType, Is.EqualTo(AttackType.Area));
            Assert.That(attackTypeAndRange.Range, Is.EqualTo("burst 2 within 2 squares of you"));
            Assert.That(attackTypeAndRange.ToString(), Is.EqualTo("Area burst 2 within 2 squares of you"));
        }
    }
}
