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
    public class Level02: Level
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
    }
}
