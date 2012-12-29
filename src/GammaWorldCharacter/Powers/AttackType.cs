using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// The type of attack.
    /// </summary>
    /// <remarks>
    /// Clearly this is not suitable for a combat simulator but that can come later, if at all.
    /// </remarks>
    public enum AttackType
    {
        /// <summary>
        /// The attack is made within melee range, usually with a melee weapon.
        /// </summary>
        Melee,
        /// <summary>
        /// The attack is a ranged attack.
        /// </summary>
        Ranged,
        /// <summary>
        /// The attack affects an area adjacent to the caster.
        /// </summary>
        Close,
        /// <summary>
        /// The attack affects a square area.
        /// </summary>
        Area,
        /// <summary>
        /// The attack affects the caster only.
        /// </summary>
        Personal
    }
}
