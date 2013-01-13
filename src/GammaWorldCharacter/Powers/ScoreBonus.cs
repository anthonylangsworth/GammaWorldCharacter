using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A bonus that adds one or more <see cref="Character "/> scores.
    /// </summary>
    public class ScoreBonus : PowerScore
    {
        /// <summary>
        /// Create a  new <see cref="ScoreBonus"/>.
        /// </summary>
        /// <param name="name">
        /// The name for this score.
        /// </param>
        /// <param name="scoreTypes">
        /// These scores from the character will be added to base.
        /// </param>
        public ScoreBonus(string name, params ScoreType[] scoreTypes)
            : this(name, 0, scoreTypes)
        {
            // Do nothing
        } 

        /// <summary>
        /// Create a  new <see cref="ScoreBonus"/>.
        /// </summary>
        /// <param name="name">
        /// The name for this score.
        /// </param>
        /// <param name="scoreTypes">
        /// These scores from the character will be added to base.
        /// </param>
        /// <param name="baseValue">
        /// Add this times the character's level.
        /// </param>
        public ScoreBonus(string name, int baseValue, params ScoreType[] scoreTypes)
            : base(name, baseValue)
        {
            if (scoreTypes.Length == 0)
            {
                throw new ArgumentException("scoreTypes cannot be empty");    
            }

            ScoreTypes = new List<ScoreType>(scoreTypes);
        }

        /// <summary>
        /// The scores to add.
        /// </summary>
        public IEnumerable<ScoreType> ScoreTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Add the specified modifiers.
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
            foreach (ScoreType scoreType in ScoreTypes)
            {
                addModifier(new Modifier(character[scoreType], this, character[scoreType].Total));
            }
        }
    }
}
