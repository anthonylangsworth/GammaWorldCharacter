using System;
using System.Collections.Generic;
using System.Linq;
using GammaWorldCharacter.Scores;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter
{
    /// <summary>
    /// The character abilities, e.g. Strength.
    /// </summary>
    public class AbilityScores: ModifierSource
    {
        private Dictionary<ScoreType, int> scores;

        /// <summary>
        /// Create a new <see cref="AbilityScores"/>.
        /// </summary>
        /// <param name="primaryOrigin">
        /// The character's primary <see cref="Origin"/>.
        /// </param>
        /// <param name="secondaryOrigin">
        /// The character's secondary <see cref="Origin"/>.
        /// </param>
        /// <param name="abilityScores">
        /// Additional ability scores. This must contain at least 4 values or at least 5 when
        /// the primary ability score of the primary and secondary origins are 
        /// the same.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="abilityScores"/> must contain at least 4 values or at least 5 when 
        /// the primary ability score of the primary and secondary origins are 
        /// </exception>
        internal AbilityScores(Origin primaryOrigin, Origin secondaryOrigin, IEnumerable<int> abilityScores)
            : base("Ability Scores", "Ability Scores")
        {
            if (primaryOrigin == null)
            {
                throw new ArgumentNullException("primaryOrigin");
            }
            if (secondaryOrigin == null)
            {
                throw new ArgumentNullException("secondaryOrigin");
            }
            if (abilityScores == null)
            {
                throw new ArgumentNullException("abilityScores");
            }
            if ((secondaryOrigin.AbilityScore == primaryOrigin.AbilityScore
                && abilityScores.Count() < 5) || abilityScores.Count() < 4)
            {
                throw new ArgumentException("Too few ability scores", "abilityScores");
            }
            if (abilityScores.Any(x => !ScoreHelper.IsValidAbilityScore(x)))
            {
                throw new ArgumentException("Invalid attribute value", "abilityScores");
            }

            IEnumerator<int> currentAbilityScore;

            scores = CreateScores();

            // As per p30
            if (secondaryOrigin.AbilityScore == primaryOrigin.AbilityScore)
            {
                scores[primaryOrigin.AbilityScore] = 20;
            }
            else
            {
                scores[primaryOrigin.AbilityScore] = 18;
                scores[secondaryOrigin.AbilityScore] = 16;
            }

            // Assign in order for determinism
            currentAbilityScore = abilityScores.GetEnumerator();
            currentAbilityScore.MoveNext();
            foreach (ScoreType scoreType in ScoreTypeHelper.AbilityScores)
            {
                if (scores[scoreType] == 0)
                {
                    scores[scoreType] = currentAbilityScore.Current;
                    currentAbilityScore.MoveNext();
                }
            }
        }

        /// <summary>
        /// The ability score.
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public int this[ScoreType scoreType]
        {
            get
            {
                return scores[scoreType];
            }
        }

        /// <summary>
        /// Create the abilty scores.
        /// </summary>
        /// <returns>
        /// A <see cref="Dictionary{K, V}"/> mapping the <see cref="ScoreType"/>
        /// to the ability score.
        /// </returns>
        internal Dictionary<ScoreType, int> CreateScores()
        {
            Dictionary<ScoreType, int> result;

            result = new Dictionary<ScoreType, int>();
            foreach (ScoreType scoreType in ScoreTypeHelper.AbilityScores)
            {
                result.Add(scoreType, 0);
            }

            return result;
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

            foreach (ScoreType scoreType in ScoreTypeHelper.AbilityScores)
            {
                addModifier(new Modifier(this, character[scoreType], scores[scoreType]));
            }
        }
    }
}
