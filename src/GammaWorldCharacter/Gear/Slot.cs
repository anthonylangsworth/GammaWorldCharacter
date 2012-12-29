using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear
{
    /// <summary>
    /// The slot on a character an item occupies.
    /// </summary>
    public enum Slot
    {
        /// <summary>
        /// The item does not occupy a slot.
        /// </summary>
        None,
        /// <summary>
        /// Body slot, e.g. armour.
        /// </summary>
        Body,
        /// <summary>
        /// Weapon
        /// </summary>
        Weapon
    }
}
