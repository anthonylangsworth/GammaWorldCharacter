using System;
using System.Collections.Generic;
using GammaWorldCharacter.Scores;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A score that adds the modifier from one or more abilities.
    /// </summary>
    public class AbilityBonus: PowerScore
    {
        private List<ScoreType> abilityScores;

        /// <summary>
        /// Create a new <see cref="AbilityBonus"/> that does not
        /// add an ability score's modifier.
        /// </summary>
        /// <param name="name">
        /// The name of the bonus, usually [power name] ["attack bonus"|"damage bonus"|.
        /// </param>
        /// <param name="abilityScores">
        /// Add the modifiers for these ability scores to the bonus.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One or more non-ability scores were supplied in abilityScores.
        /// </exception>
        public AbilityBonus(string name, IList<ScoreType> abilityScores)
            : this(name, abilityScores, 0)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="AbilityBonus"/> that does not
        /// add an ability score's modifier.
        /// </summary>
        /// <param name="name">
        /// The name of the bonus, usually [power name] ["attack bonus"|"damage bonus"].
        /// </param>
        /// <param name="abilityScores">
        /// Add the modifiers for these ability scores to the bonus.
        /// </param>
        /// <param name="baseValue">
        /// The base value of the score.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One or more non-ability scores were supplied in abilityScores.
        /// </exception>
        public AbilityBonus(string name, IList<ScoreType> abilityScores, int baseValue)
            : base(name, baseValue)
        {
            if (abilityScores == null)
            {
                throw new ArgumentNullException("abilityScores");
            }
            foreach(ScoreType abilityScore in abilityScores)
            {
                if (!ScoreTypeHelper.IsAbilityScore(abilityScore))
                {
                    throw new ArgumentException("One or more non-ability scores supplied", "abilityScores");
                }
            }

            this.abilityScores = new List<ScoreType>();
            this.abilityScores.AddRange(abilityScores);
        }

        /// <summary>
        /// Add modifiers.
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

            AbilityScore attackingAbilityScore;

            // Add the ability score modifier.
            foreach(ScoreType score in abilityScores)
            {
                attackingAbilityScore = (AbilityScore)character[score];
                addModifier(new Modifier(attackingAbilityScore, this, 
                    attackingAbilityScore.Modifier));
            }
        }
    }
}
