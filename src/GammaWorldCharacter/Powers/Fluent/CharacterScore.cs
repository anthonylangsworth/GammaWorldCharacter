using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// A character's score used in a fluent power.
    /// </summary>
    /// <remarks>
    /// Might need a better name.
    /// </remarks>
    public class CharacterScore: ICharacterScoreValue
    {
        /// <summary>
        /// Create a new <see cref="CharacterScore"/>.
        /// </summary>
        /// <param name="scoreType">
        /// The <see cref="ScoreType"/> used.
        /// </param>
        public CharacterScore(ScoreType scoreType)
        {
            this.ScoreType = scoreType;
        }

        /// <summary>
        /// The <see cref="ScoreType"/> used.
        /// </summary>
        public ScoreType ScoreType
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
            if(character == null)
            {
                throw new ArgumentNullException("character");
            }

            return character[ScoreType].Total;
        }
    }
}
