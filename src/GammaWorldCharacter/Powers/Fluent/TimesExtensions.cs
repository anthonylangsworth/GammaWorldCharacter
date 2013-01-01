using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Add extension methods to <see cref="CharacterScore"/>.
    /// </summary>
    public static class TimesExtensions
    {
        /// <summary>
        /// Multiple the given score by <paramref name="multiplicand"/>.
        /// </summary>
        /// <param name="yourScore">
        /// The score multiplied by <paramref name="multiplicand"/>. This
        /// cannot be null.
        /// </param>
        /// <param name="multiplicand">
        /// The value the score is multiplied by.
        /// </param>
        /// <returns>
        /// A <see cref="Times"/> object applied to <paramref name="yourScore"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="yourScore"/> cannot be null.
        /// </exception>
        public static ICharacterScoreValue Times(this ICharacterScoreValue yourScore, int multiplicand)
        {
            return new Times(yourScore, multiplicand);
        }
    }
}
