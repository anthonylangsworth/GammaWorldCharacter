using System;
using System.Collections.Generic;

namespace GammaWorldCharacter
{
    /// <summary>
    /// A dummy or null <see cref="ModifierSource"/> used internally.
    /// </summary>
    internal class NullModifierSource: ModifierSource
    {
        /// <summary>
        /// Create a new <see cref="NullModifierSource"/>.
        /// </summary>
        public NullModifierSource()
            : base("Null", "Null")
        {
        }
    }
}
