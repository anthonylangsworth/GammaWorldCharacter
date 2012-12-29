using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Armor
{
    /// <summary>
    /// Heavy armor.
    /// </summary>
    public class HeavyArmor: Armor
    {
        /// <summary>
        /// Create a light armor.
        /// </summary>
        public HeavyArmor()
            : base("Heavy Armor", 7, -1, Slot.Body)
        {
            // Do nothing
        }
    }
}
