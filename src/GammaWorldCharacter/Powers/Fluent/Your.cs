using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Helper functions for <see cref="CharacterScore"/>.
    /// </summary>
    public static class Your
    {
        /// <summary>
        /// The character's level.
        /// </summary>
        public static ICharacterScoreValue Level
        {
            get
            {
                return new CharacterScore(ScoreType.Level);
            }
        }
    }
}
