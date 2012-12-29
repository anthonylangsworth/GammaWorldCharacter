using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// A skill, e.g. Athletics, Heal, Nature or Streewise.
    /// </summary>
    public class Skill : Score
    {
        private ScoreType abilityScore;

        /// <summary>
        /// Create a new <see cref="Skill"/>.
        /// </summary>
        /// <param name="name">
        /// The skill's name.
        /// </param>
        /// <param name="abilityScore">
        /// The ability score whose modifier will be added to the skill.
        /// </param>
        /// <exception cref="ArgumentException">
        /// abilityScore is not an ability score.
        /// </exception>
        public Skill(string name, ScoreType abilityScore)
            : base(name, name)
        {
            if (!ScoreTypeHelper.IsAbilityScore(abilityScore))
            {
                throw new ArgumentException("Not an ability score", "abilityScore");
            }

            this.abilityScore = abilityScore;
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

            // Add the specified ability score's modfiier.
            addModifier(new Modifier(character[abilityScore], this, 
                ((AbilityScore) character[abilityScore]).Modifier));

            // Add the character's level.
            addModifier(new Modifier(character[ScoreType.Level], this,
                character[ScoreType.Level].Total));
        }
    }
}
