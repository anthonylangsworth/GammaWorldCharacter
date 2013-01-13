using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// The range, area and attack type of a <see cref="Power"/>.
    /// </summary>
    /// <remarks>
    /// Personally, I don't like this name but it is exactly what the PHB calls it on PHB 56.
    /// </remarks>
    public class AttackTypeAndRange: ModifierSource
    {
        private AttackType attackType;
        private string range;

        /// <summary>
        /// Create a new <see cref="AttackTypeAndRange"/>.
        /// </summary>
        /// <param name="name">
        /// The name used to describe this object during debugging.
        /// </param>
        /// <param name="attackType">
        /// The <see cref="AttackType"/>, e.g. Personal, Ranged or Melee.
        /// </param>
        /// <param name="range">
        /// The text following the attack type in the power description. This cannot be null
        /// but it can be an empty string.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Neither name nor description can be null.
        /// </exception>
        public AttackTypeAndRange(string name, AttackType attackType, string range)
            : base(name, name)
        {
            this.attackType = attackType;
            this.range = range;
        }

        /// <summary>
        /// The <see cref="AttackType"/> of the power, e.g. Melee, Personal, Close or Area.
        /// </summary>
        public AttackType AttackType
        {
            get
            {
                return attackType;
            }
        }

        /// <summary>
        /// Construct a human readable representation.
        /// </summary>
        /// <returns>
        /// A human readable description.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}{1}", attackType, !string.IsNullOrEmpty(Range) ? " " + Range : string.Empty);
        }

        /// <summary>
        /// The range of the power (may be empty or null).
        /// </summary>
        public string Range
        {
            get
            {
                return range;
            }
        }
    }
}
