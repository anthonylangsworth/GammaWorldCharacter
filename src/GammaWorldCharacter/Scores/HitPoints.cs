using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// The character's max hit points.
    /// </summary>
    public class HitPoints: Score
    {
        /// <summary>
        /// Create a new <see cref="HitPoints"/>.
        /// </summary>
        public HitPoints()
            : base("Hit Points", "hps", HitPointsAtFirstLevel)
        {
            // Do nothing.
        }

        /// <summary>
        /// Hit points at first level.
        /// </summary>
        public static readonly int HitPointsAtFirstLevel = 12;

        /// <summary>
        /// Hit points gained per level.
        /// </summary>
        public static readonly int HitPointsPerLevel = 5;

        /// <summary>
        /// Add modifiers based on this score's current value(s).
        /// </summary>
        /// <param name="stage">
        /// The stage during character update this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The character to add modifiers for.
        /// </param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);

            // Add Con to hps
            addModifier(new Modifier(character[ScoreType.Constitution], this, 
                character[ScoreType.Constitution].Total));

            // At (Level - 1) * (hps per level) to hps
            addModifier(new Modifier(character[ScoreType.Level], character.HitPoints, 
                (character.Level - 1) * HitPointsPerLevel));
        }
    }
}
