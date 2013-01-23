using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter.Levels
{
    /// <summary>
    /// Level 2 (where characters choose a critical hit benefit).
    /// </summary>
    public class Level02: Level, IEquatable<Level02>
    {
        /// <summary>
        /// Create a new <see cref="Level02"/> object.
        /// </summary>
        /// <param name="criticalHitBenefitOrigin">
        /// Whether the primary or secondary origin critical hit benefit
        /// was selected.
        /// </param>
        /// <seealso cref="CriticalHitBenefitOrigin"/>
        public Level02(OriginChoice criticalHitBenefitOrigin)
            : base(2)
        {
            this.CriticalHitBenefitOrigin = criticalHitBenefitOrigin;
        }

        /// <summary>
        /// Whether the primary or secondary origin critical hit benefit
        /// was selected.
        /// </summary>
        public OriginChoice CriticalHitBenefitOrigin
        {
            get; 
            private set; 
        }

        /// <summary>
        /// Are two <see cref="Level02"/>s equal?
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Level02 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && CriticalHitBenefitOrigin == other.CriticalHitBenefitOrigin;
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
            return Equals((Level02) obj);
        }

        /// <summary>
        /// Hash code support.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (int) CriticalHitBenefitOrigin;
            }
        }

        /// <summary>
        /// Human readable version
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, CriticalHitBenefitOrigin: {1}", 
                base.ToString(), CriticalHitBenefitOrigin.ToString());
        }
    }
}
