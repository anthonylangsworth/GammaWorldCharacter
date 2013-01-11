using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// The part of an effect that damages, buffs, debuffs, heals, applies a condition or
    /// otherwise effects the target(s) of the power.
    /// </summary>
    public abstract class EffectComponent: IEffectParsable
    {
        /// <summary>
        /// Create a new <see cref="EffectComponent"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        protected EffectComponent(Target target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.Expression = target.Expression;
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
        /// Return <see cref="EffectSpan"/>s representing a human 
        /// readable display.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to base the representation from.
        /// This cannot be null.
        /// </param>
        /// <returns>
        /// <see cref="EffectSpan"/>s representing this component.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="character"/> cannot be null.
        /// </exception>
        public abstract IEnumerable<EffectSpan> Parse(Character character);
    }
}
