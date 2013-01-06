using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Powers.Effects;
using GammaWorldCharacter.Powers.Effects.EffectComponents;
using GammaWorldCharacter.Powers.Origins;
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
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.TheTarget.Pushed(3);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect) expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1].Target, Is.Not.Null);
            Assert.That(expression.Components[1].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[1].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[1].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[1], Is.TypeOf<PushEffect>());
            Assert.That(((PushEffect) expression.Components[1]).Squares, Is.EqualTo(new ConstantValue(3)));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The target suffers 1d10 damage and you push the target 3 squares."));
        }

        [Test]
        public void TestEmpathCritical()
        {
            EffectExpression expression = Effect.Ally(Where.WithinSquares(5, Of.Target)).RegainsHitPoints(Your.Level.Times(2));
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(1));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0], Is.TypeOf<HealHitPointsEffect>());
            HealHitPointsEffect effectComponent = (HealHitPointsEffect)expression.Components[0];
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
            Assert.That(level.GetValue(Level01Characters.Kitty), Is.EqualTo(1));
            Assert.That(level.GetValue(Level02Characters.Kitty), Is.EqualTo(2));

            Assert.That(expression.ToString(Level01Characters.Kitty),
                Is.EqualTo("One ally within 5 squares of the target regains 2 hit points."));
        }

        [Test]
        public void TestDoppelgangerCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.You.CanUsePower<DoubleTrouble>(ActionType.Free);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf < UsePowerEffect<DoubleTrouble>>());
            UsePowerEffect<DoubleTrouble> effectComponent = (UsePowerEffect<DoubleTrouble>)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.You));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.PowerName, Is.EqualTo("Double Trouble"));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The target suffers 1d10 damage and you can use the power Double Trouble as a free action."));

            Assert.That(new EffectParser().Parse(Level01Characters.Keravnos, expression),
                Is.EquivalentTo(new []
                    {
                        new EffectSpan("The target suffers 1d10 damage and you can use the power "),
                        new EffectSpan("Double Trouble", EffectSpanType.Power),
                        new EffectSpan(" as a free action."),
                    }));
        }

        [Test]
        public void TestAndroidCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.TheTarget.GrantsCombatAdvantage(To.You, Until.EndOfEncounter);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf<GrantCombatAdvantageEffect>());
            GrantCombatAdvantageEffect effectComponent = (GrantCombatAdvantageEffect)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.To, Is.EqualTo(To.You));
            Assert.That(effectComponent.Until, Is.EqualTo(Until.EndOfEncounter));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The target suffers 1d10 damage and grants combat advantage to you until the end of the encounter."));

            Assert.That(new EffectParser().Parse(Level01Characters.Keravnos, expression),
                Is.EquivalentTo(new[]
                    {
                        new EffectSpan("The target suffers 1d10 damage and grants combat advantage to you until the end of the encounter."),
                    }));
        }

        [Test]
        public void TestCockroachCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.You.GainsModifier(Your.AC, 4, Until.EndOfYourNextTurn);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf<GainModifierEffect>());
            GainModifierEffect effectComponent = (GainModifierEffect)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.You));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.Scores.Count(), Is.EqualTo(1));
            Assert.That(effectComponent.Scores.First(), Is.EqualTo(Your.AC));
            Assert.That(effectComponent.Modifier, Is.EqualTo(new ConstantValue(4)));
            Assert.That(effectComponent.Until, Is.EqualTo(Until.EndOfYourNextTurn));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The target suffers 1d10 damage and you gain a +4 bonus to Armor Class until the end of your next turn."));

            Assert.That(new EffectParser().Parse(Level01Characters.Keravnos, expression),
                Is.EquivalentTo(new[]
                    {
                        new EffectSpan("The target suffers 1d10 damage and you gain a +4 bonus to Armor Class until the end of your next turn."),
                    }));
        }


        [Test]
        public void TestElectrokineticCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.Ally(Where.WithinSquares(5, Of.Target)).GainsTemporaryHitPoints(10);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf<TemporaryHitPointsEffect>());
            TemporaryHitPointsEffect effectComponent = (TemporaryHitPointsEffect)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.Ally));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.WithinSquares(5, Of.Target)));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.TemporaryHitPoints, Is.EqualTo(new ConstantValue(10)));
            Assert.That(effectComponent.TemporaryHitPoints.GetValue(Level01Characters.Keravnos), Is.EqualTo(10));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The target suffers 1d10 damage and one ally within 5 squares of the target gains 10 temporary hit points."));

            Assert.That(new EffectParser().Parse(Level01Characters.Keravnos, expression),
                Is.EquivalentTo(new[]
                    {
                        new EffectSpan("The target suffers 1d10 damage and one ally within 5 squares of the target gains 10 temporary hit points."),
                    }));
        }

        [Test]
        public void TestFelinoidCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.You.Shift(3, ActionType.Free);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf<ShiftEffect>());
            ShiftEffect effectComponent = (ShiftEffect)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.You));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.Squares, Is.EqualTo(new ConstantValue(3)));
            Assert.That(effectComponent.ActionType, Is.EqualTo(ActionType.Free));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The target suffers 1d10 damage and you can shift 3 squares as a free action."));

            Assert.That(new EffectParser().Parse(Level01Characters.Keravnos, expression),
                Is.EquivalentTo(new[]
                    {
                        new EffectSpan("The target suffers 1d10 damage and you can shift 3 squares as a free action."),
                    }));
        }

        [Test]
        public void TestGravityControllerCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.Creature(Where.WithinSquares(2, Of.Target)).IsImmobilized(Until.EndOfYourNextTurn);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf<ConditionEffect>());
            ConditionEffect effectComponent = (ConditionEffect)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.Creature));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.WithinSquares(2, Of.Target)));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.Until, Is.EqualTo(Until.EndOfYourNextTurn));

            Assert.That(expression.ToString(Level01Characters.Keravnos),
                Is.EqualTo("The target suffers 1d10 damage and one creature within 2 squares of the target is immobilized until the end of your next turn."));

            Assert.That(new EffectParser().Parse(Level01Characters.Keravnos, expression),
                Is.EquivalentTo(new[]
                    {
                        new EffectSpan("The target suffers 1d10 damage and one creature within 2 squares of the target is immobilized until the end of your next turn."),
                    }));
        }

        [Test]
        public void TestHawkoidCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.You.CanFly(Your.Speed, ActionType.Free);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf<FlyEffect>());
            FlyEffect effectComponent = (FlyEffect)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.You));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.Squares, Is.EqualTo(Your.Speed));
            Assert.That(effectComponent.Squares.GetValue(Level01Characters.Hermes), Is.EqualTo(6));

            Assert.That(expression.ToString(Level01Characters.Hermes),
                Is.EqualTo("The target suffers 1d10 damage and you can fly 6 squares as a free action."));

            Assert.That(new EffectParser().Parse(Level01Characters.Hermes, expression),
                Is.EquivalentTo(new[]
                    {
                        new EffectSpan("The target suffers 1d10 damage and you can fly 6 squares as a free action."),
                    }));
        }

        [Test]
        public void TestHypercognitiveCritical()
        {
            EffectExpression expression = Effect.TheTarget.SuffersDamage(1.D10()).And.YouOrAlly(Where.WithinSquares(5, Of.You)).GainsModifiers(Your.Defenses, 2, Until.EndOfYourNextTurn);
            Assert.That(expression, Is.Not.Null);
            Assert.That(expression.Components.Count(), Is.EqualTo(2));

            // Test the first component
            Assert.That(expression.Components[0], Is.Not.Null);
            Assert.That(expression.Components[0].Target, Is.Not.Null);
            Assert.That(expression.Components[0].Target.TargetType, Is.EqualTo(TargetType.TheTarget));
            Assert.That(expression.Components[0].Target.Where, Is.EqualTo(Where.Unspecified));
            Assert.That(expression.Components[0].Target.Expression, Is.SameAs(expression));
            Assert.That(expression.Components[0], Is.TypeOf<DiceDamageEffect>());
            Assert.That(((DiceDamageEffect)expression.Components[0]).Dice, Is.EqualTo(1.D10()));

            // Test the second component
            Assert.That(expression.Components[1], Is.Not.Null);
            Assert.That(expression.Components[1], Is.TypeOf<GainModifierEffect>());
            GainModifierEffect effectComponent = (GainModifierEffect)expression.Components[1];
            Assert.That(effectComponent.Target, Is.Not.Null);
            Assert.That(effectComponent.Target.TargetType, Is.EqualTo(TargetType.YouOrAlly));
            Assert.That(effectComponent.Target.Where, Is.EqualTo(Where.WithinSquares(5, Of.You)));
            Assert.That(effectComponent.Target.Expression, Is.SameAs(expression));
            Assert.That(effectComponent.Scores.Count(), Is.EqualTo(4));
            Assert.That(effectComponent.Scores, Contains.Item(new CharacterScore(ScoreType.Fortitude)));
            Assert.That(effectComponent.Scores, Contains.Item(new CharacterScore(ScoreType.Reflex)));
            Assert.That(effectComponent.Scores, Contains.Item(new CharacterScore(ScoreType.Will)));
            Assert.That(effectComponent.Scores, Contains.Item(new CharacterScore(ScoreType.ArmorClass)));
            Assert.That(effectComponent.Modifier, Is.EqualTo(new ConstantValue(2)));
            Assert.That(effectComponent.Until, Is.EqualTo(Until.EndOfYourNextTurn));

            Assert.That(expression.ToString(Level01Characters.Hermes),
                Is.EqualTo("The target suffers 1d10 damage and you or one ally within 5 squares of you gains a +2 bonus to all defenses until the end of your next turn."));

            Assert.That(new EffectParser().Parse(Level01Characters.Hermes, expression),
                Is.EquivalentTo(new[]
                    {
                        new EffectSpan("The target suffers 1d10 damage and you or one ally within 5 squares of you gains a +2 bonus to all defenses until the end of your next turn."),
                    }));
        }
    }
}
