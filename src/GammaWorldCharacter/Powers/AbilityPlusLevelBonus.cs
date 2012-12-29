using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A <see cref="AbilityBonus"/> that adds the character's level, too.
    /// </summary>
    public class AbilityPlusLevelBonus: AbilityBonus
    {
        /// <summary>
        /// Create a  new <see cref="AbilityPlusLevelBonus"/>.
        /// </summary>
        /// <param name="name">
        /// The name for this score.
        /// </param>
        /// <param name="abilityScores">
        /// Modifiers from these ability scores are added along with half the
        /// character's level.
        /// </param>
        /// <param name="levelMultiplier">
        /// Add this times the character's level.
        /// </param>
        public AbilityPlusLevelBonus(string name, IList<ScoreType> abilityScores, int levelMultiplier)
            : base(name, abilityScores)
        {
            LevelMulitplier = levelMultiplier;
        }

        /// <summary>
        /// Add this times the character's level.
        /// </summary>
        public int LevelMulitplier
        {
            get;
            private set;
        }

        /// <summary>
        /// Add half level to the existing modifiers.
        /// </summary>
        /// <param name="stage">
        /// The character update stage this is called.
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
            addModifier(new Modifier(character[ScoreType.Level], this, LevelMulitplier * character.Level));
        }
    }
}
