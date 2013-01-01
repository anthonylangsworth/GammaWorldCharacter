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
        /// <param name="where">
        /// Where the target is, or null if that is unspecified.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///  <paramref name="expression"/> cannot be null.
        /// </exception>
        public Target(EffectExpression expression, TargetType targetType, Where where)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            this.Expression = expression;
            this.TargetType = targetType;
            this.Where = where;
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
        /// Where the target is.
        /// </summary>
        public Where Where
        {
            get;
            private set;
        }
    }
}
