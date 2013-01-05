using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Helper functions for <see cref="CharacterScore"/>.
    /// </summary>
    public static class Your
    {
        /// <summary>
        /// The character's level.
        /// </summary>
        public static CharacterScore Level
        {
            get
            {
                return new CharacterScore(ScoreType.Level);
            }
        }

        /// <summary>
        /// The character's Armor Class (AC).
        /// </summary>
        public static CharacterScore AC
        {
            get
            {
                return new CharacterScore(ScoreType.ArmorClass);
            }
        }
    }
}
