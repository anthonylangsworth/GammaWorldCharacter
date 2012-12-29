using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// AC.
    /// </summary>
    public class ArmorClass: Defense
    {
        /// <summary>
        /// Create a new <see cref="ArmorClass"/>.
        /// </summary>
        public ArmorClass()
            : base("Armor Class", "AC", new ScoreType[]{ ScoreType.Dexterity, ScoreType.Intelligence })
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
            // Add to Armor Class
            if (!character.IsWearingHeavyArmor())
            {
                base.AddModifiers(stage, addModifier, character);
            }
            else
            {
                // Add half the character's level.
                addModifier(new Modifier(character[ScoreType.Level], this, 
                    character[ScoreType.Level].Total));
            }
        }
    }
}
