using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Helper methods for scores.
    /// </summary>
    public static class ScoreHelper
    {
        /// <summary>
        /// Is the given value a valid ability score?
        /// </summary>
        /// <param name="abilityScore">
        /// The attribute to check.
        /// </param>
        /// <returns>
        /// True if the value is valid, false otherwise.
        /// </returns>
        public static bool IsValidAbilityScore(int abilityScore)
        {
            return abilityScore >= 3 && abilityScore <= 18;
        }
    }
}
