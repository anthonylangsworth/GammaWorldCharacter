using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// The character's speed (i.e. the number of squares the character can move during a move action).
    /// </summary>
    public class Speed: Score
    {
        /// <summary>
        /// Base speed
        /// </summary>
        public static readonly int Base = 6;

        /// <summary>
        /// Create a new <see cref="Initiative"/>.
        /// </summary>
        public Speed()
            : base("Speed", "Speed", Base)
        {
            // Do nothing
        }
    }
}
