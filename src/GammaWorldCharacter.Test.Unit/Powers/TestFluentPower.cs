using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Fluent;
using GammaWorldCharacter.Powers.Fluent.EffectComponents;
using GammaWorldCharacter.Samples;
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
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect) expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1].Target, Is.Not.Null);
            Assert.That(expression.Components[1].Target.TargetType, Is.EqualTo(TargetType.SameTarget));
            Assert.That(expression.Components[1].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[1].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[1], Is.TypeOf<PushEffect>());
            Assert.That(((PushEffect) expression.Components[1]).Squares, Is.EqualTo(3));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The attack deals 1d10 damage and you push the target 3 squares"));
        }

        [Test]
        public void TestEmpathCritical()
        {
            EffectExpression expression = Effect.Ally(Where.WithinSquares(5, Of.Target)).GainsTemporaryHitPoints(Your.Level.Times(2));
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(1));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0], Is.TypeOf<TemporaryHitPointsEffect>());
            TemporaryHitPointsEffect effectComponent = (TemporaryHitPointsEffect)expression.Components[0];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.Ally));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(new Where(5, Of.Target)));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));

            // Test Times
            Assert.That(effectComponent.TemporaryHitPoints, Is.TypeOf<Times>());
            Assert.That(effectComponent.TemporaryHitPoints, Is.Not.Null);
            Times times = (Times)effectComponent.TemporaryHitPoints;
            Assert.That(times.Multiplicand, Is.EqualTo(2));
            Assert.That(times.GetValue(Level01Characters.Clip), Is.EqualTo(2));
            Assert.That(times.GetValue(Level02Characters.Clip), Is.EqualTo(4));

            // Test Level
            Assert.That(times.Inner, Is.Not.Null);
            Assert.That(times.Inner, Is.TypeOf<CharacterScore>());
            CharacterScore level = (CharacterScore) ((Times) effectComponent.TemporaryHitPoints).Inner;
            Assert.That(level.ScoreType, Is.EqualTo(ScoreType.Level));
            Assert.That(level.GetValue(Level01Characters.Clip), Is.EqualTo(1));
            Assert.That(level.GetValue(Level02Characters.Clip), Is.EqualTo(2));

            Assert.That(expression.ToString(Level01Characters.Kitty),
                Is.EqualTo("One Ally within 5 squares of the target regains 4 hit points."));
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
                //Effect.Creature.Damage(1.D10()).And.You.CanUsePower<DoubleTrouble>(ActionType.Free); // DoppelGanger critical
                //Effect.Creature.Damage(1.D10()).And.Ally(Where.WithinSquares(5, Of.Target).GainsBonus(ScoreType.TemporaryHitPoints, 10)); // Electrokinetic critical
                Effect.Ally(Where.WithinSquares(5, Of.Target)).GainsTemporaryHitPoints(Your.Level.Times(2)); // Empath critical
                //Effect.Creature.Damage(1.D10()).And.You.Shift(3, ActionType.Free); // Felinoid critical
                Effect.Creature.Damage(1.D10()).And.SameTarget.Pushed(3); // Giant critical
                //Effect.Creature.Damage(1.D10()).And.Creature(Where.WithinSquares(2, Of.Target)).Immobilized(Until.EndOfNextTurn); // Gravity Controller critical
                //Effect.Creature.Damage(1.D10()).And.You.CanFly(Score(ScoreType.Speed), ActionType.Free); // Hawkoid critical
                //Effect.Creature.Damage(1.D10()).And.YouOrAlly(Where.WithinSquares(5, Of.You)).GainsBonusToAllDefenses(2, Until.EndOfNextTurn); // Hypercognitive critical
            }
        }
    }
}
