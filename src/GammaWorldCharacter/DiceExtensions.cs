using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Extension methods on <see cref="Int32"/> to create <see cref="Dice"/> using a more fluent syntax, such ash:
    /// <code>
    /// Dice dice = 3.D4(); // Creates a Dice object representing 3d4
    /// </code>
    /// </summary>
    public static class DiceExtensions
    {
        /// <summary>
        /// Create a new <see cref="Dice"/> object initialised to  <paramref name="number"/> d4.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// A <see cref="Dice"/> object initialized to <paramref name="number"/> d4.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Dice D4(this int number)
        {
            return new Dice(number, DiceType.d4);
        }

        /// <summary>
        /// Create a new <see cref="Dice"/> object initialised to  <paramref name="number"/> d6.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// A <see cref="Dice"/> object initialized to <paramref name="number"/> d6.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Dice D6(this int number)
        {
            return new Dice(number, DiceType.d6);
        }

        /// <summary>
        /// Create a new <see cref="Dice"/> object initialised to  <paramref name="number"/> d8.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// A <see cref="Dice"/> object initialized to <paramref name="number"/> d8.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Dice D8(this int number)
        {
            return new Dice(number, DiceType.d8);
        }

        /// <summary>
        /// Create a new <see cref="Dice"/> object initialised to  <paramref name="number"/> d10.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// A <see cref="Dice"/> object initialized to <paramref name="number"/> d10.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Dice D10(this int number)
        {
            return new Dice(number, DiceType.d10);
        }

        /// <summary>
        /// Create a new <see cref="Dice"/> object initialised to  <paramref name="number"/> d12.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// A <see cref="Dice"/> object initialized to <paramref name="number"/> d12.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Dice D12(this int number)
        {
            return new Dice(number, DiceType.d12);
        }

        /// <summary>
        /// Create a new <see cref="Dice"/> object initialised to  <paramref name="number"/> d20.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// A <see cref="Dice"/> object initialized to <paramref name="number"/> d20.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Dice D20(this int number)
        {
            return new Dice(number, DiceType.d20);
        }

        /// <summary>
        /// Create a new <see cref="Dice"/> object initialised to  <paramref name="number"/> d100.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>
        /// A <see cref="Dice"/> object initialized to <paramref name="number"/> d100.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Dice D100(this int number)
        {
            return new Dice(number, DiceType.d100);
        }
    }
}
