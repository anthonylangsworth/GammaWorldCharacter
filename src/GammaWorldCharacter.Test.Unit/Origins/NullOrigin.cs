using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Test.Unit.Origins
{
    /// <summary>
    /// An <see cref="Origin"/> that provides no bonuses or modifiers.
    /// </summary>
    public class NullOrigin: Origin
    {
        /// <summary>
        /// Create a <see cref="NullOrigin"/>.
        /// </summary>
        public NullOrigin()
            : this(ScoreType.Strength)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a <see cref="NullOrigin"/>.
        /// </summary>
        /// <param name="scoreType">
        /// The primary ability score.
        /// </param>
        public NullOrigin(ScoreType scoreType)
            : base("Null", scoreType, PowerSource.Bio, "Sample critical benefit.")
        {
            // Do nothing
        }
    }
}
