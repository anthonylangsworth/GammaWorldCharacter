using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Add this effect to <see cref="Target"/>.
    /// </summary>
    public static class PushEffectExtensions
    {
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
    }
}
