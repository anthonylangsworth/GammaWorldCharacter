using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// A condition like stunned, slowed or prone (see GW 84-85).
    /// </summary>
    public enum Condition
    {
        /// <summary>
        /// The target cannot see.
        /// </summary>
        Blinded,
        /// <summary>
        /// The target only gets a single action.
        /// </summary>
        Dazed,
        /// <summary>
        /// The target cannot hear.
        /// </summary>
        Deafened,
        /// <summary>
        /// The target is controlled by another.
        /// </summary>
        Dominated,
        /// <summary>
        /// The target is at zero or fewer hit points.
        /// </summary>
        Dying,
        /// <summary>
        /// The target grants combat advantage and (usually) unconscious.
        /// </summary>
        Helpless,
        /// <summary>
        /// The target cannot move.
        /// </summary>
        Immobilized,
        /// <summary>
        /// The target is lying down.
        /// </summary>
        Prone,
        /// <summary>
        /// The target cannot move and grants combat advantage.
        /// </summary>
        Restrained,
        /// <summary>
        /// The target's speed is reduced to 2.
        /// </summary>
        Slowed,
        /// <summary>
        /// The target cannot take any actions.
        /// </summary>
        Stunned,
        /// <summary>
        /// The target cannot take any actions (same as stunned but different cause).
        /// </summary>
        Surprised,
        /// <summary>
        /// The target is unconscious.
        /// </summary>
        Unconscious,
        /// <summary>
        /// The target deals half damage.
        /// </summary>
        Weakened
    }
}
