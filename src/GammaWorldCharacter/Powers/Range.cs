using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// Fluent wrapper for <see cref="AttackTypeAndRange"/>.
    /// </summary>
    public static class Range
    {
        /// <summary>
        /// Melee weapon.
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> this is for. This cannot be null.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="power"/> cannot be null.
        /// </exception>
        public static AttackTypeAndRange MeleeWeapon(Power power)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }

            return new AttackTypeAndRange(power.Name, AttackType.Melee, "weapon");
        }

        /// <summary>
        /// Melee 1 (or any integer).
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> this is for. This cannot be null.
        /// </param>
        /// <param name="range">
        /// The power range. This must be positive.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="power"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="range"/> must be positive.
        /// </exception>
        public static AttackTypeAndRange Melee(Power power, int range)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }
            if (range <= 0)
            {
                throw new ArgumentException("range must be positive", "range");
            }

            return new AttackTypeAndRange(power.Name, AttackType.Melee, range.ToString());
        }

        /// <summary>
        /// Ranged weapon.
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> this is for. This cannot be null.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="power"/> cannot be null.
        /// </exception>
        public static AttackTypeAndRange RangedWeapon(Power power)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }

            return new AttackTypeAndRange(power.Name, AttackType.Ranged, "weapon");
        }

        /// <summary>
        /// Ranged 1 (or any integer).
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> this is for. This cannot be null.
        /// </param>
        /// <param name="range">
        /// The power range. This must be positive.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="power"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="range"/> must be positive.
        /// </exception>
        public static AttackTypeAndRange Ranged(Power power, int range)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }
            if (range <= 0)
            {
                throw new ArgumentException("range must be positive", "range");
            }

            return new AttackTypeAndRange(power.Name, AttackType.Ranged, range.ToString());
        }
    }
}
