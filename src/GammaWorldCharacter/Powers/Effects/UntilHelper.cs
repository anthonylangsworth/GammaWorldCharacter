using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Helper methods for <see cref="Until"/>.
    /// </summary>
    public static class UntilHelper
    {
        /// <summary>
        /// Get a human-readable representation of the given <see cref="Until"/>.
        /// </summary>
        /// <param name="until">
        /// The value to get an representation of.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Unknown value of <paramref name="until"/>.
        /// </exception>
        public static string ToString(Until until)
        {
            string result;
            switch (until)
            {
                case Until.Unspecified:
                    result = string.Empty;
                    break;
                case Until.EndOfEncounter:
                    result = "until the end of the encounter";
                    break;
                case Until.EndOfYourNextTurn:
                    result = "until the end of your next turn";
                    break;
                case Until.StartOfYourNextTurn:
                    result = "until the start of your next turn";
                    break;
                default:
                    throw new ArgumentException("Unknown Until value", "until");
            }
            return result;
        }
    }
}
