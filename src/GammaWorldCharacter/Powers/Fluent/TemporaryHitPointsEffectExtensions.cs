using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Add this effect to <see cref="Target"/>.
    /// </summary>
    public static class TemporaryHitPointsEffectExtensions
    {
        /// <summary>
        /// The target suffers the given damage.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This cannt be null.
        /// </param>
        /// <param name="temporaryHitPoints">
        /// The temporary hit points gained.
        /// </param>
        /// <returns>
        /// The current <see cref="EffectExpression"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="target"/> cannot be null.
        /// </exception>
        public static EffectExpression GainsTemporaryHitPoints(this Target target, int temporaryHitPoints)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.Expression.Components.Add(new TemporaryHitPointsEffect(target, temporaryHitPoints));
            return target.Expression;
        }

        /// <summary>
        /// The target suffers the given damage.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This cannt be null.
        /// </param>
        /// <param name="characterScoreValue">
        /// A score that, when calculated, gives the number of hit points gained.
        /// </param>
        /// <returns>
        /// The current <see cref="EffectExpression"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="target"/> cannot be null.
        /// </exception>
        public static EffectExpression GainsTemporaryHitPoints(this Target target, ICharacterScoreValue characterScoreValue)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.Expression.Components.Add(new TemporaryHitPointsEffect(target, characterScoreValue));
            return target.Expression;
        }
    }
}
