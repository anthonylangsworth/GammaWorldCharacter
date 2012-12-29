using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Levels
{
    /// <summary>
    /// Base class for all levels.
    /// </summary>
    public abstract class Level: ModifierSource
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
        /// The number of the level.
        /// </summary>
        public int Number
        {
            get;
            private set;
        }
    }
}
