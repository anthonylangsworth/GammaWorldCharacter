using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// Fluent wrapper for <see cref="AttackTypeAndRange"/>.
    /// </summary>
    public static class Range
    {
        /// <summary>
        /// Personal.
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="powerName"/> cannot be null.
        /// </exception>
        public static AttackTypeAndRange Personal(string powerName)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }

            return new AttackTypeAndRange(powerName, AttackType.Personal, null);
        }

        /// <summary>
        /// Melee weapon.
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="powerName"/> cannot be null.
        /// </exception>
        public static AttackTypeAndRange MeleeWeapon(string powerName)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }

            return new AttackTypeAndRange(powerName, AttackType.Melee, "weapon");
        }

        /// <summary>
        /// Melee 1 (or any integer).
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <param name="range">
        /// The power range. This must be positive.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="powerName"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="range"/> must be positive.
        /// </exception>
        public static AttackTypeAndRange Melee(string powerName, int range)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }
            if (range <= 0)
            {
                throw new ArgumentException("range must be positive", "range");
            }

            return new AttackTypeAndRange(powerName, AttackType.Melee, range.ToString());
        }

        /// <summary>
        /// Ranged weapon.
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="powerName"/> cannot be null.
        /// </exception>
        public static AttackTypeAndRange RangedWeapon(string powerName)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }

            return new AttackTypeAndRange(powerName, AttackType.Ranged, "weapon");
        }

        /// <summary>
        /// Ranged 1 (or any integer).
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <param name="range">
        /// The power range. This must be positive.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="powerName"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="range"/> must be positive.
        /// </exception>
        public static AttackTypeAndRange Ranged(string powerName, int range)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }
            if (range <= 0)
            {
                throw new ArgumentException("range must be positive", "range");
            }

            return new AttackTypeAndRange(powerName, AttackType.Ranged, range.ToString());
        }

        /// <summary>
        /// Close burst n (where N is a positive integer).
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <param name="size">
        /// The size of the burst in squares. This must be positive.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="powerName"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="size"/> must be positive.
        /// </exception>
        public static AttackTypeAndRange CloseBurst(string powerName, int size)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }
            if (size <= 0)
            {
                throw new ArgumentException("range must be positive", "size");
            }

            return new AttackTypeAndRange(powerName, AttackType.Close, "burst " + size.ToString());
        }

        /// <summary>
        /// Close blast n (where N is a positive integer).
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <param name="size">
        /// The size of the blast in squares. This must be positive.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="powerName"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="size"/> must be positive.
        /// </exception>
        public static AttackTypeAndRange CloseBlast(string powerName, int size)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }
            if (size <= 0)
            {
                throw new ArgumentException("range must be positive", "size");
            }

            return new AttackTypeAndRange(powerName, AttackType.Close, "blast " + size.ToString());
        }

        /// <summary>
        /// Close burst n (where N is a positive integer).
        /// </summary>
        /// <param name="powerName">
        /// The <see cref="Power"/> this is for. This cannot be null or empty.
        /// </param>
        /// <param name="size">
        /// The size of the burst in squares. This must be positive.
        /// </param>
        /// <param name="where">
        /// Where the burst occurs. This cannot be null.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// Neither <paramref name="powerName"/> nor <paramref name="where"/> can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="size"/> must be positive.
        /// </exception>
        public static AttackTypeAndRange AreaBurst(string powerName, int size,  Where where)
        {
            if (string.IsNullOrEmpty(powerName))
            {
                throw new ArgumentNullException("powerName");
            }
            if (where == null)
            {
                throw new ArgumentNullException("where");
            }
            if (size <= 0)
            {
                throw new ArgumentException("range must be positive", "size");
            }

            return new AttackTypeAndRange(powerName, AttackType.Area, 
                "burst " + size.ToString() + " " + where.ToString().ToLower());
        }
    }
}
