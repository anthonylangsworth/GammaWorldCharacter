using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Methods should look similar to the static methods on <see cref="Effect"/>.
    /// </remarks>
    public class EffectConjunction
    {
        /// <summary>
        /// Create a new <see cref="EffectConjunction"/>.
        /// </summary>
        /// <param name="expression"></param>
        public EffectConjunction(EffectExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            this.Expression = expression;
        }

        /// <summary>
        /// 
        /// </summary>
        public EffectExpression Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// A creature.
        /// </summary>
        public Target Creature
        {
            get
            {
                return new Target(Expression, TargetType.Creature, Where.Unspecified);
            }
        }

        /// <summary>
        /// The power originator.
        /// </summary>
        public Target You
        {
            get
            {
                return new Target(Expression, TargetType.You, Where.Unspecified);
            }
        }

        /// <summary>
        /// A friendly creature.
        /// </summary>
        public Target Ally(Where where)
        {
            return new Target(Expression, TargetType.Creature, where);
        }

        /// <summary>
        /// Either the power originator or a friendly creature.
        /// </summary>
        public Target YouOrAlly(Where where)
        {
            return new Target(Expression, TargetType.Creature, where);
        }

        /// <summary>
        /// An unfriendly creature.
        /// </summary>
        public Target Enemy(Where where)
        {
            return new Target(Expression, TargetType.Creature, where);
        }
 
        /// <summary>
        /// The same target as the previous <see cref="EffectConjunction"/>.
        /// </summary>
        public Target TheTarget
        {
            get
            {
                return new Target(Expression, TargetType.TheTarget, Where.Unspecified);
            }
        }
    }
}
