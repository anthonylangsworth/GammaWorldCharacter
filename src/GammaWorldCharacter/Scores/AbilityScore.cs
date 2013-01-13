using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GammaWorldCharacter.Scores
{
    /// <summary>
    /// An ability score, e.g. Dexterity or Charisma.
    /// </summary>
    public class AbilityScore: Score
    {
        /// <summary>
        /// Create a new <see cref="AbilityScore"/>.
        /// </summary>
        /// <param name="name">
        /// The ability score name.
        /// </param>
        /// <param name="abbreviation">
        /// An abbreviated ability score.
        /// </param>
        /// <param name="modifierScore">
        /// The <see cref="ScoreType"/> that
        /// </param>
        public AbilityScore(string name, string abbreviation, ScoreType modifierScore)
            : base(name, abbreviation)
        {
            if (!ScoreTypeHelper.AbilityScoreModifiers.Contains((modifierScore)))
            {
                throw new ArgumentException("modifierScore is not an ability score modifier.");
            }

            this.ModifierScore = modifierScore;
        }

        /// <summary>
        /// The attribute modifier.
        /// </summary>
        public int Modifier
        {
            get
            {
                int result;

                if (Total > 10)
                {
                    result = (Total - 10) / 2;
                }
                else if (Total < 10)
                {
                    result = (Total - 11) / 2;
                }
                else
                {
                    result = 0;
                }

                return result;
            }
        }

        /// <summary>
        /// The modifier score.
        /// </summary>
        public ScoreType ModifierScore
        {
            get;
            private set;
        }

        /// <summary>
        /// Add the modifier to the modifier score.
        /// </summary>
        /// <param name="stage">
        /// The stage during character update this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The character to add modifiers for. This should not be modified directly.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Neither modifiers nor character can be null.
        /// </exception>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);

            addModifier(new Modifier(this, character[ModifierScore], Modifier));
        }

        /// <summary>
        /// Construct a human readable representation of the object.
        /// </summary>
        /// <param name="format">
        /// In addition to those provided by the base class:
        /// "SM" shows 'score' ('modifier')
        /// </param>
        /// <returns></returns>
        public override string ToString(string format)
        {
            string result;
            if (format == "SM")
            {
                result = string.Format("{0} ({1})", Total, ModifierHelper.FormatModifier(Modifier, true));
            }
            else
            {
                result = base.ToString(format);
            }

            return result;
        }
    }
}
