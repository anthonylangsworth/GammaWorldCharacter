using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter
{
    /// <summary>
    /// The type of die.
    /// </summary>
    /// <remarks>
    /// Each value is represented by an int equal to the number of sizes, 
    /// e.g. a d4 is an int with the value of '4'.
    /// </remarks>
    public enum DiceType
    {
        /// <summary>
        /// 4 sided die.
        /// </summary>
        d4 = 4,
        /// <summary>
        /// 6 sided die.
        /// </summary>
        d6 = 6,
        /// <summary>
        /// 8 sided die.
        /// </summary>
        d8 = 8,
        /// <summary>
        /// 10 sided die.
        /// </summary>
        d10 = 10,
        /// <summary>
        /// 12 sided die.
        /// </summary>
        d12 = 12,
        /// <summary>
        /// 20 sided die.
        /// </summary>
        d20 = 20,
        /// <summary>
        /// Percentage (usually to d10s)
        /// </summary>
        d100 = 100
    }
}
