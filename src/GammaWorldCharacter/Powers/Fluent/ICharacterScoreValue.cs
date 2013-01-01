using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// A character's score used in a power.
    /// </summary>
    public interface ICharacterScoreValue
    {
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
        int GetValue(Character character);
    }
}
