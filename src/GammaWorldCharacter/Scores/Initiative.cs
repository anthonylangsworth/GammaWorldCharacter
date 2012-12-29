using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// Create a new <see cref="Initiative"/> score.
    /// </summary>
    public class Initiative : Score
    {
        /// <summary>
        /// Create a new <see cref="Initiative"/>.
        /// </summary>
        public Initiative()
            : base("Initiative", "Init")
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
            
            addModifier(new Modifier(character[ScoreType.Dexterity], this, 
                ((AbilityScore) character[ScoreType.Dexterity]).Modifier));

            // Add the character's level.
            addModifier(new Modifier(character[ScoreType.Level], this,
                character[ScoreType.Level].Total));
        }
    }
}
