using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent.EffectComponents
{
    /// <summary>
    /// Apply damage to target(s) determined by a hard coded number of dice.
    /// </summary>
    public class DiceDamageEffect : EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <remarks>
        /// TODO: Add DamageType
        /// </remarks>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="dice">
        /// The damage dealt. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public DiceDamageEffect(Target target, Dice dice)
            : base(target)
        {
            if (dice == null)
            {
                throw new ArgumentNullException("dice");
            }

            this.Dice = dice;
        }

        /// <summary>
        /// The damage dealt.
        /// </summary>
        public Dice Dice
        {
            get;
            private set;
        }
    }
}
