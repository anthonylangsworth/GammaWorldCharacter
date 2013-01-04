using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Multiply the given score vale.
    /// </summary>
    public class Times: ICharacterScoreValue
    {
        /// <summary>
        /// Create a new <see cref="Times"/>.
        /// </summary>
        /// <param name="inner">
        /// The score multiplied by <paramref name="multiplicand"/>. This
        /// cannot be null.
        /// </param>
        /// <param name="multiplicand">
        /// The value the score is multiplied by.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="inner"/> cannot be null.
        /// </exception>
        public Times(ICharacterScoreValue inner, int multiplicand)
        {
            if (inner == null)
            {
                throw new ArgumentNullException("inner");
            }

            this.Inner = inner;
            this.Multiplicand = multiplicand;
        }

        /// <summary>
        /// The <see cref="CharacterScore"/> that this modifies.
        /// </summary>
        public ICharacterScoreValue Inner
        {
            get;
            private set;
        }

        /// <summary>
        /// The value the score is multiplied by.
        /// </summary>
        public int Multiplicand
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the value or total of the <see cref="Score"/> on the
        /// given <paramref name="character"/>.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to get the <see cref="Score"/> from.
        /// </param>
        /// <returns>
        /// The value or toal of the score.
        /// </returns>
        public virtual int GetValue(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            return Inner.GetValue(character) * Multiplicand;
        }
    }
}
