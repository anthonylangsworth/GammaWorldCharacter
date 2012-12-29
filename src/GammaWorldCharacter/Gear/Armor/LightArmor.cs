using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Armor
{
    /// <summary>
    /// Light armor.
    /// </summary>
    public class LightArmor: Armor
    {
        /// <summary>
        /// Create a light armor.
        /// </summary>
        public LightArmor()
            : base("Light Armor", 3, 0, Slot.Body)
        {
            // Do nothing
        }
    }
}
