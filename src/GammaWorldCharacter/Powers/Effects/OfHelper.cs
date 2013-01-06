using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Helper methods for <see cref="Of"/>.
    /// </summary>
    public static class OfHelper
    {
        /// <summary>
        /// Return a human-readable representation.
        /// </summary>
        /// <param name="of"></param>
        /// <returns></returns>
        public static string ToString(Of of)
        {
            string result;
            switch (of)
            {
                case Of.Target:
                    result = "the target";
                    break;
                case Of.You:
                    result = "you";
                    break;
                default:
                    throw new ArgumentException("Unknown of value", "of");
            }
            return result;
        }
    }
}
