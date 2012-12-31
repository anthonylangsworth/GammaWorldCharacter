using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
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
            this.Components = new List<EffectComponent>();
        }

        /// <summary>
        /// The <see cref="Effect"/>s the expression includes.
        /// </summary>
        public IList<EffectComponent> Components
        {
            get;
            private set;
        }

        /// <summary>
        /// Add another <see cref="EffectComponent"/> that occurs in addition to other
        /// effect components.
        /// </summary>
        public EffectConjunction And
        {
            get
            {
                return new EffectConjunction(this);
            }
        }
    }
}
