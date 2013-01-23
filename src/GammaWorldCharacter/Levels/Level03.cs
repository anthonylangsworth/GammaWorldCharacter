using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Levels
{
    /// <summary>
    /// Level 3, where characters get an origin utility power.
    /// </summary>
    public class Level03: Level, IPowerSource, IEquatable<Level03>
    {
        private Power utilityPower;

        /// <summary>
        /// Create a new <see cref="Level03"/>.
        /// </summary>
        /// <param name="utilityPowerOrigin">
        /// The origin the character will use the utility power from.
        /// </param>
        public Level03(OriginChoice utilityPowerOrigin)
            : base(3)
        {
            this.UtilityPowerOrigin = utilityPowerOrigin;
        }

        /// <summary>
        /// The origin the utility power will come form.
        /// </summary>
        public OriginChoice UtilityPowerOrigin
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the power from the appropriate origin.
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="addModifier"></param>
        /// <param name="character"></param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);

            Origin origin;
            if (UtilityPowerOrigin == OriginChoice.Primary)
            {
                origin = character.PrimaryOrigin;
            }
            else
            {
                origin = character.SecondaryOrigin;
            }

            utilityPower = origin.UtilityPower;
        }

        /// <summary>
        /// The <see cref="Power"/>s supplied by this level, specifically
        /// the selected origin's utility power.
        /// </summary>
        public IEnumerable<Power> Powers
        {
            get 
            {
                if (utilityPower != null)
                {
                    yield return utilityPower;
                }
            }
        }

        /// <summary>
        /// Are two <see cref="Level03"/>s equal?
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Level03 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && UtilityPowerOrigin == other.UtilityPowerOrigin;
        }

        /// <summary>
        /// Are two objects equal?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Level03) obj);
        }

        /// <summary>
        /// Hash code support.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (int) UtilityPowerOrigin;
            }
        }

        /// <summary>
        /// Human readable version
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, UtilityPowerOrigin: {1}",
                base.ToString(), UtilityPowerOrigin.ToString());
        }

    }
}
