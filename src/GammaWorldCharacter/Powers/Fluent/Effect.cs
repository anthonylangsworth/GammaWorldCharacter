using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// An effect, such as damage or healing, on a target, such as a creature or an ally.
    /// </summary>
    public class Effect
    {
        /// <summary>
        /// Create a new <see cref="Effect"/>.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="EffectExpression"/> this is part of, or null for
        /// no previous expression.
        /// </param>
        public Effect(EffectExpression expression)
        {
            this.Expression = expression == null ? new EffectExpression(null) : expression;
        }

        /// <summary>
        /// The <see cref="EffectExpression"/> this is part of or null, if it
        /// is not part of an experssion.
        /// </summary>
        public EffectExpression Expression
        {
            get; 
            private set; 
        }

        /// <summary>
        /// Combine multiple
        /// </summary>
        public EffectExpression And
        {
            get
            {
                // TODO: Add an "And" operator to the expression and move on
                return null;
            }
        }
    }

    /// <summary>
    /// An expression that involves multiple effects, such as <see cref="AndEffectExpression"/>.
    /// </summary>
    public class EffectExpression : Effect
    {
        /// <summary>
        /// Create a new <see cref="EffectExpression"/>.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="EffectExpression"/> this is part of, or null for
        /// no previous expression.
        /// </param>
        protected EffectExpression(EffectExpression expression)
            : base(expression)
        {
            this.Expressions = new List<Effect>();
        }

        /// <summary>
        /// The <see cref="Effect"/>s the expression includes.
        /// </summary>
        public IList<Effect> Expressions
        {
            get; 
            private set;
        }
    }

    /// <summary>
    /// An expression where all the included effects occur.
    /// </summary>
    public abstract class AndEffectExpression : EffectExpression
    {
        /// <summary>
        /// Create a new <see cref="AndEffectExpression"/>.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="EffectExpression"/> this is part of, or null for
        /// no previous expression.
        /// </param>
        protected AndEffectExpression(EffectExpression expression)
            : base(expression)
        {
            // Do nothing
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
    }

    /// <summary>
    /// Apply damage to target(s) determined by a hard coded number of dice.
    /// </summary>
    public class DiceDamageEffect: EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="EffectExpression"/> this is part of, or null
        /// if it is not part of an expression.
        /// </param>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="dice">
        /// The damage dealt. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Neither <paramref name="target"/> nor <paramref name="dice"/> can be null.
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
    /// 
    /// </summary>
    /// <returns>
    /// TODO: Add DamageType
    /// </returns>
    public abstract class Target
    {
        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="expression">
        /// The <see cref="EffectExpression"/> this is part of, or null
        /// if it is not part of an expression.
        /// </param>
        protected Target(EffectExpression expression)
        {
            this.Expression = expression;
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
        /// 
        /// </summary>
        /// <param name="dice"></param>
        /// <returns></returns>
        public EffectComponent Damage(Dice dice)
        {
            return new DiceDamageEffect(Expression, this, dice);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Creature : Target
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class You: Target
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public class Ally : Target
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class YouOrAlly : Target
    {
    }
        
    /// <summary>
    /// 
    /// </summary>
    public class Enemy : Target
    {
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

    // Aim:
    // 1. Create easier to read and understand syntax. Fluent is good but not required (strongly influenced by NUnit).
    // 2. Be easy to convert into human readable text.
    // 3. Allow other uses, such as in a game.
    //
    // Want to be able to write something like:
    //
    // Creature.Damage(1.D10()).And.SameTarget.GrantsCombatAdvantage(Until.EncounterEnd); // Android critical
    // Creature.Damage(1.D10()).And.You.GainBonus(Score(ScoreType.AC), 4, Until.EndOfNextTurn); // Cockroach critical
    // Creature.Damage(1.D10()).And.You.CanUsePower(typeof(DoubleTrouble, ActionType.Free); // DoppelGanger critical
    // Creature.Damage(1.D10()).And.Ally(Where.WithinSquares(5, Of.Target).GainsBonus(ScoreType.TemporaryHitPoints, 10); // Electrokinetic critical
    // Ally(Where.WithinSquares(5)).GainsTemporaryHitPoints(You.Level.Times(2); // Empath critical
    // Creature.Damage(1.D10()).And.You.Shift(3, ActionType.Free); // Felinoid critical
    // Creature.Damage(1.D10()).And.SameTarget.Push(3); // Giant critical
    // Creature.Damage(1.D10()).And.Creature(Where.WithinSquares(2, Of.Target)).Immobilized(Until.EndOfNextTurn); // Gravity Controller critical
    // Creature.Damage(1.D10()).And.You.CanFly(Score(ScoreType.Speed), ActionType.Free); // Hawkoid critical
    // Creature.Damage(1.D10()).And.YouOrAlly(Where.WithinSquares(5, Of.You)).GainsBonusToAllDefenses(2, Until.EndOfNextTurn); // Hypercognitive critical
    //
    // Form:
    // Target (subject; either Creature, You, Ally, YouOrAlly, Enemy or SameTarget (property only, indicates same target as previous expression) 
    //    -> Effect (e.g. Damage, GrantsCombatAdvantage, GainBonus, Push, CanFly, etc) (verb and object) 
    //    (optional) -> Conjunction -> Target -> Effect (etc) ...
    //
    // * Need to be careful to not leave out required parts, therefore moving some parts to arguments.
}
