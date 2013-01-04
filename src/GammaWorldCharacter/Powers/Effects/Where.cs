using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Used with some effects this indicates where an additional effect occurs
    /// relative to either the originator or target of a power.
    /// </summary>
    public class Where: IEquatable<Where>
    {
        /// <summary>
        /// Create a new <see cref="Where"/>.
        /// </summary>
        /// <param name="squares">
        /// The number of squares distant the new target can be.
        /// </param>
        /// <param name="of">
        /// Relative to either the original target or originator of the power.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="squares"/> must be positive.
        /// </exception>
        public Where(int squares, Of of)
        {
            if (squares <= 0)
            {
                throw new ArgumentException("number must be positive", "number");
            }

            this.Squares = squares;
            this.Of = of;
        }

        /// <summary>
        /// The number of squares the effect occurs from either the target
        /// of the power or originator of the power.
        /// </summary>
        /// <see cref="Of"/>
        public int Squares
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
        /// Compare two <see cref="Where"/> objects.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Where other)
        {
            if (other != null)
            {
                return this.Squares == other.Squares
                       && this.Of == other.Of;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Compare two objects.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Where)
            {
                return Equals((Where) obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A hash code for this object.
        /// </summary>
        /// <returns>
        /// A hash code.
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = 17;
            unchecked // Let it wrap
            {
                hashCode = hashCode * 23 + this.Squares.GetHashCode();
                hashCode = hashCode * 23 + this.Of.GetHashCode();               
            }
            return hashCode;
        }

        /// <summary>
        /// Show a human-readable representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Within {0} squares of {1}", Squares, Of);
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

        /// <summary>
        /// Where the additional effect occurs is unspecified or no
        /// additional information is needed.
        /// </summary>
        public static Where Unspecified
        {
            get
            {
                return null;
            }
        }

    }
}
