using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// Create a new <see cref="Bloodied"/> score.
    /// </summary>
    public class Bloodied: Score
    {
        /// <summary>
        /// Create a new <see cref="Bloodied"/>.
        /// </summary>
        public Bloodied()
            : base("Bloodied", "Bloodied")
        {
            // Do nothing
        }

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

            addModifier(new Modifier(character[ScoreType.HitPoints], this, 
                character[ScoreType.HitPoints].Total / 2));
        }
    }
}
