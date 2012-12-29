using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// How often a power can be used.
    /// </summary>
    public enum PowerFrequency
    {
        /// <summary>
        /// Can be used at will.
        /// </summary>
        AtWill,
        /// <summary>
        /// Can be used once per encounter. Recharges after a short rest (5 mins).
        /// </summary>
        Encounter,
        /// <summary>
        /// Using this power destroys or otherwise renders the item unusable. Effectively a single use power.
        /// </summary>
        Consumable
    }
}
