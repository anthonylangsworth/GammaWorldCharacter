using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Levels
{
    /// <summary>
    /// Base class for all levels.
    /// </summary>
    public abstract class Level: ModifierSource, IEquatable<Level>
    {
        /// <summary>
        /// Minimum possible level.
        /// </summary>
        public static readonly int Min = 2;

        /// <summary>
        /// Maximum possible level.
        /// </summary>
        public static readonly int Max = 30;

        /// <summary>
        /// Create a new <see cref="Level"/>.
        /// </summary>
        /// <param name="number">
        /// The level number.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Level must be between 1 and 30. requiredClass must be the type of a 
        /// class derived from Class.
        /// </exception>
        protected Level(int number)
            : base(string.Format("Level {0}", number), string.Format("Lvl{0}", number))
        {
            if (number < Min || number > Max)
            {
                throw new ArgumentException(
                    string.Format("Level must be between {0} and {1} inclusive", Min, Max), "level");
            }

            Number = number;
        }

        /// <summary>
        /// Are two <see cref="Level"/>s equal?
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Level other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Number == other.Number;
        }

        /// <summary>
        /// Are two objects equal?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Level)obj);
        }

        /// <summary>
        /// Hash code support.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ Number;
            }
        }

        /// <summary>
        /// The number of the level.
        /// </summary>
        public int Number
        {
            get;
            private set;
        }
    }
}
