using System;
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
        public AbilityScore(string name, string abbreviation)
            : base(name, abbreviation)
        {
            // Do nothing
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
