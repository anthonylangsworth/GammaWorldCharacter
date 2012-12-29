using System;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// A <see cref="Score"/> that drives common benefits from gaining levels, such 
    /// as +1 per 2 levels to skills and defenses.
    /// </summary>
    /// <remarks>
    /// Although this score does nothing its type is used to ensure no other modifier source
    /// modifies it.
    /// </remarks>
    public class LevelScore: Score
    {
        /// <summary>
        /// Create a new <see cref="LevelScore"/>.
        /// </summary>
        internal LevelScore()
            : base("Levels", "Levels")
        {
            // Do nothing
        }

        /// <summary>
        /// Set the level score.
        /// </summary>
        /// <param name="stage">
        /// The stage at which this is called.
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
        }
    }
}
