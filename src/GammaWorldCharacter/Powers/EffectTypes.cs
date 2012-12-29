using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// Power classifications.
    /// </summary>
    [Flags]
    public enum EffectTypes
    {
        /// <summary>
        /// No effect types are used.
        /// </summary>
        None = 0,
        /// <summary>
        /// Powers that resotre hit points.
        /// </summary>
        Healing = 1,
        /// <summary>
        /// Powers that transport Characters instantly from one location to another.
        /// </summary>
        Teleportation = 2,
        /// <summary>
        /// Powers that create lingering effects that extend over an area.
        /// </summary>
        Zone = 4
    }
}
