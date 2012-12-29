using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Traits
{
    /// <summary>
    /// A <see cref="ModifierSource"/> that provides <see cref="Trait"/>s.
    /// </summary>
    interface ITraitSource
    {
        /// <summary>
        /// The <see cref="Trait"/>s provided.
        /// </summary>
        IEnumerable<Trait> Traits
        {
            get;
        }
    }
}
