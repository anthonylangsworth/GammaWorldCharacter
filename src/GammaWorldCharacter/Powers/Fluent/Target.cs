using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// The portion of an <see cref="EffectComponent"/> that describes what the effect component acts on.
    /// </summary>
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
            Expression.Components.Add(new DiceDamageEffect(this, dice));
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
            Expression.Components.Add(new PushEffect(this, squares));
            return Expression;
        }
    }
}
