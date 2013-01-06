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

        /// <summary>
        /// The character's speed.
        /// </summary>
        public static CharacterScore Speed
        {
            get
            {
                return new CharacterScore(ScoreType.Speed);
            }
        }

        /// <summary>
        /// The character's Fortitude defense.
        /// </summary>
        public static CharacterScore Fortitude
        {
            get
            {
                return new CharacterScore(ScoreType.Fortitude);
            }
        }

        /// <summary>
        /// The character's Reflex defense.
        /// </summary>
        public static CharacterScore Reflex
        {
            get
            {
                return new CharacterScore(ScoreType.Reflex);
            }
        }

        /// <summary>
        /// The character's Will defense.
        /// </summary>
        public static CharacterScore Will
        {
            get
            {
                return new CharacterScore(ScoreType.Will);
            }
        }

        /// <summary>
        /// The character's Armor Class defense.
        /// </summary>
        public static CharacterScore ArmorClass
        {
            get
            {
                return new CharacterScore(ScoreType.ArmorClass);
            }
        }

        /// <summary>
        /// All defenses (that is Fort, Ref, Will and AC).
        /// </summary>
        public static IEnumerable<CharacterScore> Defenses
        {
            get
            {
                yield return new CharacterScore(ScoreType.Fortitude);
                yield return new CharacterScore(ScoreType.Reflex);
                yield return new CharacterScore(ScoreType.Will);
                yield return new CharacterScore(ScoreType.ArmorClass);
            }
        }
    }
}
