using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Origins;

// Aim:
// 1. Create easier synatx to describe power effects that (1) actually
//    describes the effects rather than just including it as text and
//    (2) is easy to read and understand effect syntax. Fluent is good
//    but not required (strongly influenced by NUnit).
// 2. Be easy to convert into human readable text.
// 3. Allow other uses, such as in a game.
//
// Want to be able to write something like:
//
// Effect.Creature.Damage(1.D10()).And.SameTarget.GrantsCombatAdvantage(Until.EncounterEnd); // Android critical
// Effect.Creature.Damage(1.D10()).And.You.GainBonus(Score(ScoreType.AC), 4, Until.EndOfNextTurn); // Cockroach critical
// Effect.Creature.Damage(1.D10()).And.You.CanUsePower(typeof(DoubleTrouble, ActionType.Free); // DoppelGanger critical
// Effect.Creature.Damage(1.D10()).And.Ally(Where.WithinSquares(5, Of.Target).GainsBonus(ScoreType.TemporaryHitPoints, 10); // Electrokinetic critical
// Effect.Ally(Where.WithinSquares(5)).GainsTemporaryHitPoints(You.Level.Times(2); // Empath critical
// Effect.Creature.Damage(1.D10()).And.You.Shift(3, ActionType.Free); // Felinoid critical
// Effect.Creature.Damage(1.D10()).And.SameTarget.Push(3); // Giant critical
// Effect.Creature.Damage(1.D10()).And.Creature(Where.WithinSquares(2, Of.Target)).Immobilized(Until.EndOfNextTurn); // Gravity Controller critical
// Effect.Creature.Damage(1.D10()).And.You.CanFly(Score(ScoreType.Speed), ActionType.Free); // Hawkoid critical
// Effect.Creature.Damage(1.D10()).And.YouOrAlly(Where.WithinSquares(5, Of.You)).GainsBonusToAllDefenses(2, Until.EndOfNextTurn); // Hypercognitive critical
//
// Form:
// Target (subject; either Creature, You, Ally, YouOrAlly, Enemy or SameTarget (property only, indicates same target as previous expression) 
//    -> Effect (e.g. Damage, GrantsCombatAdvantage, GainBonus, Push, CanFly, etc) (verb and object) 
//    (optional) -> Conjunction -> Target -> Effect (etc) ...
//
// * Need to be careful to not leave out required parts, therefore moving some parts to arguments.
//
// Current thoughts:
// * Does "Effect" become a static class to start the chain or should Effect merge with EffectComponent?

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// An effect, such as damage or healing, on a target, such as a creature or an ally. It may
    /// include mulitple <see cref="EffectComponent"/>s, each acting on the same or different
    /// targets.
    /// </summary>
    public static class Effect
    {
        /// <summary>
        /// 
        /// </summary>
        public static Target Creature
        {
            get
            {
                return new Target(new EffectExpression(), TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Target You
        {
            get
            {
                return new Target(new EffectExpression(), TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Target Ally
        {
            get
            {
                return new Target(new EffectExpression(), TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Target YouOrAlly
        {
            get
            {
                return new Target(new EffectExpression(), TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Target Enemy
        {
            get
            {
                return new Target(new EffectExpression(), TargetType.Creature);
            }
        }
    }

    /// <summary>
    /// An expression that involves multiple effect components.
    /// </summary>
    /// <remarks>
    /// Because EffectExpression does not inherit from <see cref="EffectComponent"/>, it
    /// is not possible to nest expressions at this time (might be added later). The current
    /// implementation applies all effects (effectively an 'and').
    /// </remarks>
    public class EffectExpression
    {
        /// <summary>
        /// Create a new <see cref="EffectExpression"/>.
        /// </summary>
        public EffectExpression()
        {
            this.EffectComponents = new List<EffectComponent>();
        }

        /// <summary>
        /// The <see cref="Effect"/>s the expression includes.
        /// </summary>
        public IList<EffectComponent> EffectComponents
        {
            get;
            private set;
        }

        /// <summary>
        /// TODO: Is this the right place for it?
        /// </summary>
        public EffectConjunction And
        {
            get
            {
                return new EffectConjunction(this);
            }
        }
    }

    /// <summary>
    /// The part of an effect that damages, buffs, debuffs, heals, applies a condition or
    /// otherwise effects the target(s) of the power.
    /// </summary>
    public abstract class EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="EffectComponent"/>.
        /// </summary>
        /// <param name="expression">
        /// The effect expression this is part of. This cannot be null.
        /// </param>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        protected EffectComponent(EffectExpression expression, Target target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            this.Expression = expression;
            this.Target = target;
        }

        /// <summary>
        /// The effect expression this is part of or null if it is not part
        /// of an expression.
        /// </summary>
        public EffectExpression Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// The target this effect component acts on.
        /// </summary>
        public Target Target
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Target Creature
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target You
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target Ally
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target YouOrAlly
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target Enemy
        {
            get
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Apply damage to target(s) determined by a hard coded number of dice.
    /// </summary>
    public class DiceDamageEffect: EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <remarks>
        /// TODO: Add DamageType
        /// </remarks>
        /// <param name="expression">
        /// The <see cref="EffectExpression"/> this is part of. This cannot
        /// be null.
        /// </param>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="dice">
        /// The damage dealt. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public DiceDamageEffect(EffectExpression expression, Target target, Dice dice)
            : base(expression, target)
        {
            if (dice == null)
            {
                throw new ArgumentNullException("dice");
            }

            this.Dice = dice;
        }

        /// <summary>
        /// The damage dealt.
        /// </summary>
        public Dice Dice
        { 
            get;
            private set;
        }
    }

    /// <summary>
    /// The entity the <see cref="EffectComponent"/> targets.
    /// </summary>
    public enum TargetType
    {
        /// <summary>
        /// Any creature.
        /// </summary>
        Creature,
        /// <summary>
        /// The power originator.
        /// </summary>
        You,
        /// <summary>
        /// A friendly target that is not the power originator.
        /// </summary>
        Ally,
        /// <summary>
        /// A friendly target including the power originator.
        /// </summary>
        YouOrAlly,
        /// <summary>
        /// An unfriendly target.
        /// </summary>
        Enemy,
        /// <summary>
        /// The same target as a previous <see cref="EffectComponent"/>.
        /// </summary>
        SameTarget
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// </returns>
    public class Target
    {
        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="EffectExpression"/> this is part of. This cannot be null.
        /// </param>
        /// <param name="targetType">
        /// The actual target of the <see cref="EffectComponent"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression"/> cannot be null.
        /// </exception>
        public Target(EffectExpression expression, TargetType targetType)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            this.Expression = expression;
            this.TargetType = targetType;
        }

        /// <summary>
        /// The effect expression this is part of or null if it is not part
        /// of an expression.
        /// </summary>
        public EffectExpression Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// The actual target of the <see cref="EffectComponent"/>.
        /// </summary>
        public TargetType TargetType
        {
            get;
            private set;
        }

        /// <summary>
        /// The target suffers the given damage.
        /// </summary>
        /// <param name="dice">
        /// The amount of damage suffered. This cannot be null.
        /// </param>
        /// <returns>
        /// The current <see cref="EffectExpression"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dice"/> cannot be null.
        /// </exception>
        public EffectExpression Damage(Dice dice)
        {
            Expression.EffectComponents.Add(new DiceDamageEffect(Expression, this, dice));
            return Expression;
        }
        
        /// <summary>
        /// Push the target a number of squares.
        /// </summary>
        /// <param name="squares">
        /// The number of squares the target is pushed.
        /// </param>
        /// <returns>
        /// The current <see cref="EffectExpression"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="squares"/> must be positive.
        /// </exception>
        internal EffectExpression Pushed(int squares)
        {
            Expression.EffectComponents.Add(new PushEffect(Expression, this, squares));
            return Expression;
        }
    }

    /// <summary>
    /// Used in conjunction with <see cref="Where"/>, this specifies where 
    /// <see cref="Where.WithinSquares"/> and other methods are relative to.
    /// </summary>
    public enum Of
    {
        /// <summary>
        /// Relative to the target of the power.
        /// </summary>
        Target,
        /// <summary>
        /// Relative to the originator of the power.
        /// </summary>
        You
    }

    /// <summary>
    /// Used with some effects this indicates where an additional effect occurs
    /// relative to either the originator or target of a power.
    /// </summary>
    public class Where
    {
        /// <summary>
        /// Create a new <see cref="Where"/>.
        /// </summary>
        /// <param name="number">
        /// The number of squares distant the new target can be.
        /// </param>
        /// <param name="of">
        /// Relative to either the original target or originator of the power.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public Where(int number, Of of)
        {
            if (number <= 0)
            {
                throw new ArgumentException("number must be positive", "number");
            }

            this.Number = number;
            this.Of = of;
        }

        /// <summary>
        /// The number of squares the effect occurs from either the target
        /// of the power or originator of the power.
        /// </summary>
        /// <see cref="Of"/>
        public int Number
        {
            get; 
            private set;
        }

        /// <summary>
        /// There the 
        /// </summary>
        public Of Of
        {
            get; 
            private set;
        }

        /// <summary>
        /// The additional effect occurs relative to the original target.
        /// </summary>
        /// <param name="number">
        /// The number of squares distant the new target can be.
        /// </param>
        /// <param name="of">
        /// Relative to either the original target or originator of the power.
        /// </param>
        /// <returns>
        /// A constructed <see cref="Where"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Where WithinSquares(int number, Of of)
        {
            return new Where(number, of);
        }
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
