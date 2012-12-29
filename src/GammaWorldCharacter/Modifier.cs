using System;
using System.Collections.Generic;
using GammaWorldCharacter.Scores;

namespace GammaWorldCharacter
{
    /// <summary>
    /// A bonus or penalty to a <see cref="Score"/>.
    /// </summary>
    /// <remarks>
    /// Conditional modifiers are those important enough to include on the character sheet
    /// but cannot be handled by this system, e.g. bonuses to saving throws against various
    /// effects.
    /// </remarks>
    public class Modifier: IEquatable<Modifier>
    {
        private string condition;
        private Score modifiedScore;
        private int modifierValue;
        private ModifierSource source;

        /// <summary>
        /// Create a new <see cref="Modifier"/>.
        /// </summary>
        /// <param name="source">
        /// The source of the modifier.
        /// </param>
        /// <param name="modifiedScore">
        /// The <see cref="Score"/> affected, such as "Strength" or "Hit Points".
        /// </param>
        /// <param name="modifierValue">
        /// The amount of the modifier.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The source and modifiedScore cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Cannot modify self.
        /// </exception>
        public Modifier(ModifierSource source, Score modifiedScore, int modifierValue)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (modifiedScore == null)
            {
                throw new ArgumentNullException("modifiedScore");
            }
            if (source.Equals(modifiedScore))
            {
                throw new ArgumentException("Cannot modify self");
            }

            this.source = source;
            this.modifiedScore = modifiedScore;
            this.modifierValue = modifierValue;
        }

        /// <summary>
        /// Create a new conditional <see cref="Modifier"/>.
        /// </summary>
        /// <param name="source">
        /// The source of the modifier.
        /// </param>
        /// <param name="modifiedScore">
        /// The <see cref="Score"/> affected, such as "Strength" or "Hit Points".
        /// </param>
        /// <param name="modifierValue">
        /// The amount of the modifier.
        /// </param>
        /// <param name="condition">
        /// The condition where this modifier applies.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The source cannot be null.
        /// </exception>
        public Modifier(ModifierSource source, Score modifiedScore, int modifierValue, 
            string condition)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (string.IsNullOrEmpty(condition))
            {
                throw new ArgumentNullException("condition");
            }
            if (modifiedScore == null)
            {
                throw new ArgumentNullException("modifiedScore");
            }

            this.condition = condition;
            this.source = source;
            this.modifiedScore = modifiedScore;
            this.modifierValue = modifierValue;
        }

        /// <summary>
        /// The name of the condition where the modifier applies.
        /// </summary>
        public string Condition
        {
            get
            {
                return condition;
            }
        }

        /// <summary>
        /// True if the modifier is conditional, false otherwise.
        /// </summary>
        public bool Conditional
        {
            get
            {
                return condition != null;
            }
        }

        /// <summary>
        /// Are the modifiers the same?
        /// </summary>
        /// <param name="obj">
        /// The modifier to compare.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Modifier)
            {
                return Equals((Modifier)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Are the modifiers the same?
        /// </summary>
        /// <param name="other">
        /// The modifier to compare.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        public bool Equals(Modifier other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            // TODO Check condition

            return Conditional.Equals(other.Conditional)
                && ModifiedScore.Equals(other.ModifiedScore)
                && ModifierValue.Equals(other.ModifierValue);
        }

        /// <summary>
        /// Get a hash code for the object.
        /// </summary>
        /// <returns>
        /// The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + Condition + "|" + ModifiedScore.GetHashCode()
                + "|" + ModifierValue).GetHashCode();
        } 

        /// <summary>
        /// The source of the modifier, i.e. the aspect of the character that gave this bonus or penalty.
        /// </summary>
        public ModifierSource Source
        {
            get
            {
                return source;
            }
        }

        /// <summary>
        /// The <see cref="Score"/> modified.
        /// </summary>
        public Score ModifiedScore
        {
            get
            {
                return modifiedScore;
            }
        }

        /// <summary>
        /// The amount or value of the modifier.
        /// </summary>
        public int ModifierValue
        {
            get
            {
                return modifierValue;
            }
        }

        /// <summary>
        /// Construct a human readable representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString("D");
        }

        /// <summary>
        /// Construct a human readable representation of the object.
        /// </summary>
        /// <param name="format">
        /// "S" returns 'modifier' ('type' from 'source').
        /// "D" returns the 'modifier' ('type' from 'source') to 'score' and is intended for debugging.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// 'format' cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Unknown format.
        /// </exception>
        public virtual string ToString(string format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            string result;
            string text;

            switch (format)
            {
                case "S":
                    if (!Conditional)
                    {
                        text = Source.Abbreviation;
                    }
                    else
                    {
                        text = Condition;
                    }

                    if(ModifierValue != 0)
                    {
                        result = string.Format("{0} {1}",
                            ModifierHelper.FormatModifier(ModifierValue, false), text);
                    }
                    else
                    {
                        result = text;
                    }
                    break;
                case "D":
                    result = string.Format("{0} (from {1}) to {2}", 
                        ModifierHelper.FormatModifier(ModifierValue, false),
                        Source.Name, ModifiedScore.ToString());
                    if (Conditional)
                    {
                        result += " " + Condition;
                    }
                    break;
                default:
                    throw new ArgumentException("format");
            }

            return result;
        }
    }
}
