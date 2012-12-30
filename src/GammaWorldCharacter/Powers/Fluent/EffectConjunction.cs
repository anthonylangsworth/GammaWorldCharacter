using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// 
    /// </summary>
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
        /// 
        /// </summary>
        public Target Creature
        {
            get
            {
                return new Target(Expression, TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target You
        {
            get
            {
                return new Target(Expression, TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target Ally
        {
            get
            {
                return new Target(Expression, TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target YouOrAlly
        {
            get
            {
                return new Target(Expression, TargetType.Creature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Target Enemy
        {
            get
            {
                return new Target(Expression, TargetType.Creature);
            }
        }
 
        /// <summary>
        /// 
        /// </summary>
        public Target SameTarget
        {
            get
            {
                return new Target(Expression, TargetType.SameTarget);
            }
        }
    }
}
