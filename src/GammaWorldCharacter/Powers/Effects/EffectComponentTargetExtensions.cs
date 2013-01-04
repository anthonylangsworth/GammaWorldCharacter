using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects.EffectComponents;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Extension methods for <see cref="Target"/> from different <see cref="EffectComponent"/> classes.
    /// </summary>
    /// <remarks>
    /// This is here rather than the EffectComponents namespace to prevent those using this namespace
    /// from having to include the EffectComponents namespace (intended to separate out classes internally).
    /// </remarks>
    public static class EffectComponentTargetExtensions
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

        /// <summary>
        /// Push the target a number of squares.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This cannt be null.
        /// </param>
        /// <param name="squares">
        /// The number of squares the target is pushed.
        /// </param>
        /// <returns>
        /// The current <see cref="EffectExpression"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="squares"/> must be positive.
        /// </exception>
        public static EffectExpression Pushed(this Target target, int squares)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.Expression.Components.Add(new PushEffect(target, squares));
            return target.Expression;
        }

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
