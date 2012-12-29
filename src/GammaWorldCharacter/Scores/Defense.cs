using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// A defense, i.e. Armor Class, Fortitude, Reflex or Will.
    /// </summary>
    public class Defense: Score
    {
        private List<ScoreType> abilityScores;

        /// <summary>
        /// Create a new <see cref="Defense"/>.
        /// </summary>
        /// <param name="name">
        /// The defense's name.
        /// </param>
        /// <param name="abbreviation">
        /// Abbreviation for the defense.
        /// </param>
        /// <param name="abilityScores">
        /// From these ability scores' modifiers, add the largest modifier.
        /// </param>
        public Defense(string name, string abbreviation, IList<ScoreType> abilityScores)
            : base(name, abbreviation)
        {
            if (abilityScores == null)
            {
                throw new ArgumentNullException("abilityScores");
            }
            foreach (ScoreType abilityScore in abilityScores)
            {
                if (!ScoreTypeHelper.IsAbilityScore(abilityScore))
                {
                    throw new ArgumentException("One or more scores are not ability scores", "abilityScore");
                }
            }

            this.abilityScores = new List<ScoreType>();
            this.abilityScores.AddRange(abilityScores);
        }

        /// <summary>
        /// The base value of the defense.
        /// </summary>
        public override int BaseValue
        {
            get
            {
                return 10;
            }
        }

        /// <summary>
        /// Add dependencies.
        /// </summary>
        /// <param name="addDependency">
        /// Add a dependency by calling this method.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add dependencies for.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Neither dependencies nor Character can be null.
        /// </exception>
        protected override void AddDependencies(Action<ModifierSource, ModifierSource> addDependency,
            Character character)
        {
            base.AddDependencies(addDependency, character);

            foreach (ScoreType abilityScore in abilityScores)
            {
                addDependency(character[abilityScore], this);
            }
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
            AbilityScore abilityScoreWithLargestModifier;
            AbilityScore currentAbilityScore;

            base.AddModifiers(stage, addModifier, character);

            abilityScoreWithLargestModifier = null;
            foreach (ScoreType abilityScore in abilityScores)
            {
                currentAbilityScore = (AbilityScore) character[abilityScore];
                if (abilityScoreWithLargestModifier == null)
                {
                    abilityScoreWithLargestModifier = currentAbilityScore;
                }
                else if(currentAbilityScore.Modifier> abilityScoreWithLargestModifier.Modifier)
                {
                    abilityScoreWithLargestModifier = currentAbilityScore;
                }
            }

            addModifier(new Modifier(abilityScoreWithLargestModifier,this, 
                abilityScoreWithLargestModifier.Modifier));

            // Add the character's level.
            addModifier(new Modifier(character[ScoreType.Level], this, 
                character[ScoreType.Level].Total));
        }
    }
}
