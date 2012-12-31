using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Used with some effects this indicates where an additional effect occurs
    /// relative to either the originator or target of a power.
    /// </summary>
    public class Where
    {
        /// <summary>
        /// Create a new <see cref="Where"/>.
        /// </summary>
        /// <param name="number">
        /// The number of squares distant the new target can be.
        /// </param>
        /// <param name="of">
        /// Relative to either the original target or originator of the power.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public Where(int number, Of of)
        {
            if (number <= 0)
            {
                throw new ArgumentException("number must be positive", "number");
            }

            this.Number = number;
            this.Of = of;
        }

        /// <summary>
        /// The number of squares the effect occurs from either the target
        /// of the power or originator of the power.
        /// </summary>
        /// <see cref="Of"/>
        public int Number
        {
            get;
            private set;
        }

        /// <summary>
        /// There the 
        /// </summary>
        public Of Of
        {
            get;
            private set;
        }

        /// <summary>
        /// The additional effect occurs relative to the original target.
        /// </summary>
        /// <param name="number">
        /// The number of squares distant the new target can be.
        /// </param>
        /// <param name="of">
        /// Relative to either the original target or originator of the power.
        /// </param>
        /// <returns>
        /// A constructed <see cref="Where"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public static Where WithinSquares(int number, Of of)
        {
            return new Where(number, of);
        }
    }
}
