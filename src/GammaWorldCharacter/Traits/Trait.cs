using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter.Traits
{
    /// <summary>
    /// A "trait", a bonus or ability offered by a class that is not easily
    /// represented by a <see cref="Power"/> or <see cref="Modifier"/>.
    /// </summary>
    public class Trait: ModifierSource
    {
        /// <summary>
        /// Create a new <see cref="Trait"/>.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        public Trait(string name, string description)
            : base(name, name)
        {
            SetDescription(description);
        }
    }
}
