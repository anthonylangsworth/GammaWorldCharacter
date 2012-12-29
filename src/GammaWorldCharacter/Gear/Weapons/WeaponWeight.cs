using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Weapons
{
    /// <summary>
    /// Whether a weapon is heavy (and Str/Con based) or light (Dex/Int based).
    /// </summary>
    public enum WeaponWeight
    {
        /// <summary>
        /// The weapon is Dex/Int based.
        /// </summary>
        Light,
        /// <summary>
        /// The weapon is Str/Con based.
        /// </summary>
        Heavy
    }
}
