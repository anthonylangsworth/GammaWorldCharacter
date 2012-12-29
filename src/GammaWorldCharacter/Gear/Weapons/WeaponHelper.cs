using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Weapons
{
    /// <summary>
    /// Helper methods for <see cref="Weapon"/>s.
    /// </summary>
    public static class WeaponHelper
    {
        /// <summary>
        /// Get the melee weapon's name.
        /// </summary>
        /// <param name="handedness">
        /// Whether the weapon is one or two handed.
        /// </param>
        /// <param name="weight">
        /// Whether the weapon is light or heavy.
        /// </param>
        /// <returns>
        /// The weapon's name.
        /// </returns>
        public static string GetMeleeWeaponName(WeaponHandedness handedness, WeaponWeight weight)
        {
            return string.Format("{0} {1}-handed melee weapon",
                weight == WeaponWeight.Heavy ? "Heavy" : "Light",
                handedness == WeaponHandedness.TwoHanded ? "two" : "one");
        }

        /// <summary>
        /// Get the ranged weapon's name.
        /// </summary>
        /// <param name="rangedType">
        /// Whether the weapon requires ammunition.
        /// </param>
        /// <param name="handedness">
        /// Whether the weapon is one or two handed.
        /// </param>
        /// <param name="weight">
        /// Whether the weapon is light or heavy.
        /// </param>
        /// <returns>
        /// The weapon's name.
        /// </returns>
        internal static string GetRangedWeaponName(RangedType rangedType, WeaponHandedness handedness, WeaponWeight weight)
        {
            return string.Format("{0} {1}-handed {2}",
                weight == WeaponWeight.Heavy ? "Heavy" : "Light",
                handedness == WeaponHandedness.TwoHanded ? "two" : "one",
                rangedType == RangedType.Weapon ? "ranged weapon" : "gun");
        }
    }
}
