using System;
using System.Collections.Generic;
using System.Linq;
using GammaWorldCharacter.Scores;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Helper methods to deal with <see cref="Modifier"/>s.
    /// </summary>
    public static class ModifierHelper
    {
        /// <summary>
        /// Format a modifier to add a plus sign if it is positive and, optionally, 
        /// a dash ("-") if it is zero.
        /// </summary>
        /// <param name="modifier">
        /// The modifier value.
        /// </param>
        /// <param name="dashOnZero">
        /// True if, instead of +0, it should show a dash.
        /// </param>
        /// <returns>
        /// </returns>
        public static string FormatModifier(int modifier, bool dashOnZero)
        {
            string result;
            if(modifier > 0)
            {
                result = "+" + modifier.ToString();
            }
            else if(modifier < 0)
            {
                result = modifier.ToString();
            }
            else if (dashOnZero)
            {
                result = "-";
            }
            else
            {
                result = "+0";
            }
            return result;
        }

        /// <summary>
        /// Construct a list of allt he conditional modifiers in the given list.
        /// </summary>
        /// <param name="modifiers">
        /// An <see cref="IEnumerable{Modifier}"/> of modifiers.
        /// </param>
        /// <returns>
        /// A list of all given conditional modifiers.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either 'modifiers' or one or more of its members are null.
        /// </exception>
        public static IEnumerable<Modifier> GetConditionalModifiers(IEnumerable<Modifier> modifiers)
        {
            if (modifiers == null)
            {
                throw new ArgumentNullException("modifiers");
            }
            if (modifiers.Contains(null))
            {
                throw new ArgumentNullException("modifiers", "One or more modifiers are null");
            }

            return modifiers.Where(x => x.Conditional);
        }

        /// <summary>
        /// Construct a list of modifiers wihout conditional modifiers.
        /// </summary>
        /// <param name="modifiers">
        /// An <see cref="IEnumerable{Modifier}"/> of modifiers.
        /// </param>
        /// <returns>
        /// A subset of modifiers containing only non-conditional modifiers.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either 'modifiers' or one or more of its members are null.
        /// </exception>
        public static IEnumerable<Modifier> GetEffectiveModifiers(IEnumerable<Modifier> modifiers)
        {
            if (modifiers == null)
            {
                throw new ArgumentNullException("modifiers");
            }
            if (modifiers.Contains(null))
            {
                throw new ArgumentNullException("modifiers", "One or more modifiers are null");
            }

            return modifiers.Where(x => !x.Conditional);
        }
    }
}
