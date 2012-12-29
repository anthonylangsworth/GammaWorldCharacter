using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// An ability check.
    /// </summary>
    public class AbilityCheck: Score
    {
        private ScoreType abilityScore;

        /// <summary>
        /// Create a new <see cref="AbilityCheck"/>.
        /// </summary>
        /// <param name="abilityScore"></param>
        public AbilityCheck(ScoreType abilityScore)
            : base(GetName(abilityScore), GetName(abilityScore))
        {
            if (!ScoreTypeHelper.IsAbilityScore(abilityScore))
            {
                throw new ArgumentException("Not an ability score", "abilityScore");
            }

            this.abilityScore = abilityScore;
        }

        /// <summary>
        /// The ability score this is the check for.
        /// </summary>
        public ScoreType AbilityScore
        {
            get
            {
                return abilityScore;
            }
        }

        /// <summary>
        /// Add the ability score modifier and half level.
        /// </summary>
        /// <param name="stage">
        /// The stage during which this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add modifiers for.
        /// </param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);
            addModifier(new Modifier(character[ScoreType.Level], this, 
                character[ScoreType.Level].Total));
            addModifier(new Modifier(character[abilityScore], this, 
                ((AbilityScore) character[abilityScore]).Modifier));
        }

        /// <summary>
        /// Return the name for the ability check.
        /// </summary>
        /// <param name="abilityScore">
        /// The ability score to use in the name.
        /// </param>
        /// <returns>
        /// The score's name.
        /// </returns>
        private static string GetName(ScoreType abilityScore)
        {
            return string.Format("{0} check", abilityScore);
        }
    }
}
