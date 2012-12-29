using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Armor
{
    /// <summary>
    /// A shield.
    /// </summary>
    public class Shield: Armor
    {
        /// <summary>
        /// Create a new <see cref="Shield"/>.
        /// </summary>
        public Shield()
            : base("Shield", 1, 0, Slot.Weapon)
        {
            // Do nothing
        }
    }
}
