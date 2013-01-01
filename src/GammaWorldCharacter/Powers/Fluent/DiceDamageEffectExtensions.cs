using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Add this effect to <see cref="Target"/>.
    /// </summary>
    public static class DiceDamageEffectExtensions
    {
        /// <summary>
        /// The target suffers the given damage.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This cannt be null.
        /// </param>
        /// <param name="dice">
        /// The amount of damage suffered. This cannot be null.
        /// </param>
        /// <returns>
        /// The current <see cref="EffectExpression"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Neither <paramref name="target"/> nor <paramref name="dice"/> can be null.
        /// </exception>
        public static EffectExpression Damage(this Target target, Dice dice)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.Expression.Components.Add(new DiceDamageEffect(target, dice));
            return target.Expression;
        }
    }
}
