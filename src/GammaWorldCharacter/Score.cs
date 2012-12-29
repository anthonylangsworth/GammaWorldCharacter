using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter;

namespace GammaWorldCharacter
{
    /// <summary>
    /// A numeric value associated with an aspect of a character, such as an ability score.
    /// </summary>
    public class Score: ModifierSource
    {
        private List<Modifier> appliedModifiers;

        /// <summary>
        /// Create a new <see cref="Score"/> with a base value of 0.
        /// </summary>
        /// <param name="name">
        /// The name of the score.
        /// </param>
        /// <param name="abbreviation">
        /// The name of the abbreviation.
        /// </param>
        public Score(string name, string abbreviation)
            : this(name, abbreviation, 0)
        {
            BaseValue = 0;
        }

        /// <summary>
        /// Create a new <see cref="Score"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the score.
        /// </param>
        /// <param name="abbreviation">
        /// The name of the abbreviation.
        /// </param>
        /// <param name="baseValue">
        /// The base value of the score.
        /// </param>
        public Score(string name, string abbreviation, int baseValue)
            : base(name, abbreviation)
        {
            appliedModifiers = new List<Modifier>();
            BaseValue = baseValue;
        }

        /// <summary>
        /// The modifiers that apply to this score.
        /// </summary>
        public IList<Modifier> AppliedModifiers
        {
            get
            {
                return appliedModifiers.AsReadOnly();
            }
        }

        /// <summary>
        /// The base or initial value of the score.
        /// </summary>
        public virtual int BaseValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Remove existing modifiers.
        /// </summary>
        internal void ClearModifiers()
        {
            appliedModifiers.Clear();
        }

        /// <summary>
        /// The total or current value of the score.
        /// </summary>
        public int Total
        {
            get
            {
                return BaseValue + GetEffectiveModifiers().Sum(x => x.ModifierValue);
            }
        }

        /// <summary>
        /// The conditional modifiers applied to this score, i.e. those that may
        /// affect game play but cannot be modelled by this system.
        /// </summary>
        /// <remarks>
        /// Callers should cache this value as it is calculated on each call.
        /// </remarks>
        public virtual IEnumerable<Modifier> GetConditionalModifiers()
        {
            return ModifierHelper.GetConditionalModifiers(AppliedModifiers);
        }

        /// <summary>
        /// The effective modifiers applied to this score, i.e. the largets of each
        /// type, excluding untyped.
        /// </summary>
        /// <remarks>
        /// Callers should cache this value as it is calculated on each call.
        /// </remarks>
        public virtual IEnumerable<Modifier> GetEffectiveModifiers()
        {
            return ModifierHelper.GetEffectiveModifiers(AppliedModifiers);
        }

        /// <summary>
        /// Construct a human readable representation of this object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString("D");
        }

        /// <summary>
        /// Construct a human readable representation of this object.
        /// </summary>
        /// <param name="format">
        /// "S" shows just the score.
        /// "M" shows the score as a modifier.
        /// "FS" shows the score and a parenthetical list of modifiers.
        /// "FM" shows the score as a modifier and a parenthetical list of modifiers.
        /// "D" shows the score followed by a parenthetical list of modifiers with additional debug information.
        /// "CS" shows the score followed by a parenthetical list of conditional modifiers.
        /// "CM" shows the score as a modifier followed by a parenthetical list of conditional modifiers.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// format cannot be null.
        /// </exception>
        public virtual string ToString(string format)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            string result;

            switch (format)
            {
                case "S":
                    result = Total.ToString();
                    break;
                case "M":
                    result = ModifierHelper.FormatModifier(Total, false);
                    break;
                case "FS":
                    result = FullToString(false);
                    break;
                case "FM":
                    result = FullToString(true);
                    break;
                case "CS":
                    result = ScoreAndConditionalsToString(false);
                    break;
                case "CM":
                    result = ScoreAndConditionalsToString(true);
                    break;
                case "D":
                    result = Name + " "  + FullToString(false);
                    break;
                default:
                    throw new ArgumentException("Unknown format", "format");
            }

            return result;
        }

        /// <summary>
        /// Add a <see cref="Modifier"/>. This is called during character update.
        /// </summary>
        /// <param name="modifier">
        /// The <see cref="Modifier"/> to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 'modifier' cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Modifier not for this score.
        /// </exception>
        internal void AddModifier(Modifier modifier)
        {
            if(modifier == null)
            {
                throw new ArgumentNullException("modifier");
            }
            if (modifier.ModifiedScore != this)
            {
                throw new ArgumentException("Not a modifier for this score", "modifier");
            }

            appliedModifiers.Add(modifier);
        }

        /// <summary>
        /// Construct a representation of this score that includes conditional modifiers.
        /// </summary>
        /// <param name="modifier">
        /// True if this score should be treated as a modifier, false if it is a score.
        /// </param>
        /// <returns>
        /// A human readable string.
        /// </returns>
        private string ScoreAndConditionalsToString(bool modifier)
        {
            StringBuilder stringBuilder;
            IEnumerable<Modifier> conditionalModifiers;
            bool addedConditionalModifier;

            stringBuilder = new StringBuilder();

            if (modifier)
            {
                stringBuilder.AppendFormat("{0}", ModifierHelper.FormatModifier(Total, false));
            }
            else
            {
                stringBuilder.AppendFormat("{0}", Total);
            }

            conditionalModifiers = GetConditionalModifiers();
            if (conditionalModifiers.Any())
            {
                stringBuilder.Append(" (");
                addedConditionalModifier = false;
                foreach (Modifier conditionalModifier in conditionalModifiers)
                {
                    if (addedConditionalModifier)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(conditionalModifier.ToString("S"));
                    addedConditionalModifier = true;
                }
                stringBuilder.Append(")");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Constuct a representation of the Score that includes all the modifiers.
        /// </summary>
        /// <param name="modifier">
        /// True if this score should be treated as a modifier, false if it is a score.
        /// </param>
        /// <returns>
        /// A human readable string.
        /// </returns>
        private string FullToString(bool modifier)
        {
            StringBuilder stringBuilder;
            IEnumerable<Modifier> effectiveModifiers;
            IEnumerable<Modifier> conditionalModifiers;
            bool addedEffectiveModifier;
            bool addedConditionalModifier;
            
            stringBuilder = new StringBuilder();

            if (modifier)
            {
                stringBuilder.AppendFormat("{0}", ModifierHelper.FormatModifier(Total, false));
            }
            else
            {
                stringBuilder.AppendFormat("{0}", Total);
            }

            effectiveModifiers = GetEffectiveModifiers();
            if (effectiveModifiers.Any())
            {
                stringBuilder.Append(" (");
                addedEffectiveModifier = false;
                foreach (Modifier effectiveModifier in effectiveModifiers)
                {
                    if (addedEffectiveModifier)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(effectiveModifier.ToString("S"));
                    addedEffectiveModifier = true;
                }
                stringBuilder.Append(")");
            }

            conditionalModifiers = GetConditionalModifiers();
            if (conditionalModifiers.Any())
            {
                stringBuilder.Append("; ");
                addedConditionalModifier = false;
                foreach (Modifier conditionalModifier in conditionalModifiers)
                {
                    if (addedConditionalModifier)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(conditionalModifier.ToString("S"));
                    addedConditionalModifier = true;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
