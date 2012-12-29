using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Weapons
{
    /// <summary>
    /// Whether the ranged weapon is a gun or a weapon.
    /// </summary>
    public enum RangedType
    {
        /// <summary>
        /// The weapon does not require ammunition or, more accurately, the character can 
        /// create ammunition as required.
        /// </summary>
        Weapon,
        /// <summary>
        /// The weapon requires the player to track ammunition use.
        /// </summary>
        Gun
    }
}
