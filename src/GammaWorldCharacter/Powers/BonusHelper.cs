using System;
using System.Collections.Generic;
using System.Linq;
using GammaWorldCharacter;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Scores;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// Helper functionality for dealing with bonuses.
    /// </summary>
    public static class BonusHelper
    {
        /// <summary>
        /// Add the character's level as a bonus to the given score.
        /// </summary>
        /// <param name="modifiedScore">
        /// The <see cref="Score"/> to receive the bonus.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add the modifier for.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public static void AddCharacterLevel(Score modifiedScore, Action<Modifier> addModifier, 
            Character character)
        {
            if (modifiedScore == null)
            {
                throw new ArgumentNullException("modifiedScore");
            }
            if (addModifier == null)
            {
                throw new ArgumentNullException("addModifiers");
            }
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            addModifier(new Modifier(character[ScoreType.Level], modifiedScore, 
                character[ScoreType.Level].Total));
        }

        /// <summary>
        /// Add the modifer from the highest ability score given in <paramref name="abilityScores"/>.
        /// </summary>
        /// <param name="modifiedScore">
        /// The <see cref="Score"/> to receive the bonus.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add the modifier for.
        /// </param>
        /// <param name="abilityScores">
        /// An <see cref="IEnumerable{ScoreType}"/> containing the ScoreTypes to search.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public static void AddGreatestScoreModifier(Score modifiedScore, Action<Modifier> addModifier,
            Character character, IEnumerable<ScoreType> abilityScores)
        {
            if (modifiedScore == null)
            {
                throw new ArgumentNullException("modifiedScore");
            }
            if (addModifier == null)
            {
                throw new ArgumentNullException("addModifiers");
            }
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }
            if (abilityScores == null)
            {
                throw new ArgumentNullException("abilityScores");
            }
            if(abilityScores.Any(x => !ScoreTypeHelper.IsAbilityScore(x)))
            {
                throw new ArgumentException("One or more ability scores are not ability scores", "abilityScores");
            }

            ScoreType greatestScoreType = abilityScores.OrderByDescending(x => character[x].Total).First();

            addModifier(new Modifier(character[greatestScoreType], modifiedScore,
                ((AbilityScore) character[greatestScoreType]).Modifier));
        }
    }
}
