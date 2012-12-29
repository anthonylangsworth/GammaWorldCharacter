using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A <see cref="Score"/> used by a power.
    /// </summary>
    public class PowerScore: Score
    {
        /// <summary>
        /// Create a new <see cref="PowerScore"/>.
        /// </summary>
        /// <param name="name">
        /// The name for the score, e.g. "[Power name] attack bonus".
        /// </param>
        /// <param name="baseValue">
        /// The base value of the score.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        protected PowerScore(string name, int baseValue)
            : base(name, name, baseValue)
        {
            // Do nothing
        }
    }
}
