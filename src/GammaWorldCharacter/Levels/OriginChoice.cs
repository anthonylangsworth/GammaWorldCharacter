using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Levels
{
    /// <summary>
    /// Specifies either a character's primary or secondary <see cref="Origin"/>.
    /// </summary>
    public enum OriginChoice
    {
        /// <summary>
        /// The primary origin.
        /// </summary>
        Primary,
        /// <summary>
        /// The secondary origin.
        /// </summary>
        Secondary
    }
}
