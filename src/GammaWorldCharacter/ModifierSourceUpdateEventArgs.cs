using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Arguments to events raised when modifiers are updated.
    /// </summary>
    public class ModifierSourceUpdateEventArgs : EventArgs
    {
        private ModifierSource modifierSource;

        /// <summary>
        /// Create a new <see cref="ModifierSourceUpdateEventArgs"/>.
        /// </summary>
        /// <param name="modifierSource">
        /// The <see cref="ModifierSource"/> being updated.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="modifierSource"/> cannot be null.
        /// </exception>
        public ModifierSourceUpdateEventArgs(ModifierSource modifierSource)
        {
            if (modifierSource == null)
            {
                throw new ArgumentNullException("modifierSource");
            }

            this.modifierSource = modifierSource;
        }

        /// <summary>
        /// The <see cref="ModifierSource"/> being updated.
        /// </summary>
        public ModifierSource ModifierSource
        {
            get
            {
                return modifierSource;
            }
        }
    }
}
