using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Fluent;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Powers
{
    [TestFixture]
    public class TestFluentPower
    {
        [Test]
        public void TestGiantCritical()
        {
            EffectExpression expression = Effect.Creature.Damage(1.D10()).And.SameTarget.Pushed(3);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.Creature));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect) expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1].Target, Is.Not.Null);
            Assert.That(expression.Components[1].Target.TargetType, Is.EqualTo(TargetType.SameTarget));
            Assert.That(expression.Components[1], Is.TypeOf<PushEffect>());
            Assert.That(((PushEffect) expression.Components[1]).Squares, Is.EqualTo(3));
        }

        /// <summary>
        /// 
        /// </summary>
        public class Test
        {
            /// <summary>
            /// 
            /// </summary>
            public void TestFluent()
            {
                //Effect.Creature.Damage(1.D10()).And.SameTarget.GrantsCombatAdvantage(Until.EncounterEnd); // Android critical
                //Effect.Creature.Damage(1.D10()).And.You.GainBonus(Score(ScoreType.AC), 4, Until.EndOfNextTurn); // Cockroach critical
                //Effect.Creature.Damage(1.D10()).And.You.CanUsePower(typeof(DoubleTrouble), ActionType.Free); // DoppelGanger critical
                //Effect.Creature.Damage(1.D10()).And.Ally(Where.WithinSquares(5, Of.Target).GainsBonus(ScoreType.TemporaryHitPoints, 10)); // Electrokinetic critical
                //Effect.Ally(Where.WithinSquares(5)).GainsTemporaryHitPoints(You.Level.Times(2)); // Empath critical
                //Effect.Creature.Damage(1.D10()).And.You.Shift(3, ActionType.Free); // Felinoid critical
                Effect.Creature.Damage(1.D10()).And.SameTarget.Pushed(3); // Giant critical
                //Effect.Creature.Damage(1.D10()).And.Creature(Where.WithinSquares(2, Of.Target)).Immobilized(Until.EndOfNextTurn); // Gravity Controller critical
                //Effect.Creature.Damage(1.D10()).And.You.CanFly(Score(ScoreType.Speed), ActionType.Free); // Hawkoid critical
                //Effect.Creature.Damage(1.D10()).And.YouOrAlly(Where.WithinSquares(5, Of.You)).GainsBonusToAllDefenses(2, Until.EndOfNextTurn); // Hypercognitive critical
            }
        }
    }
}
