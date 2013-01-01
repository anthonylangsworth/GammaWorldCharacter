using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent.EffectComponents
{
    /// <summary>
    /// Give the target temporary hit points.
    /// </summary>
    public class TemporaryHitPointsEffect: EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="temporaryHitPoints">
        /// The number of temporary hit points. This must be positive.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public TemporaryHitPointsEffect(Target target, int temporaryHitPoints)
            : base(target)
        {
            if (temporaryHitPoints <= 0)
            {
                throw new ArgumentException("temporaryHitPoints");
            }

            this.TemporaryHitPoints = new ConstantValue(temporaryHitPoints);
        }

        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="characterScoreValue">
        /// A score that, when calculated, gives the number of hit points gained.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public TemporaryHitPointsEffect(Target target, ICharacterScoreValue characterScoreValue)
            : base(target)
        {
            if (characterScoreValue == null)
            {
                throw new ArgumentNullException("characterScoreValue");
            }

            this.TemporaryHitPoints = characterScoreValue;
        }

        /// <summary>
        /// The damage dealt.
        /// </summary>
        public ICharacterScoreValue TemporaryHitPoints
        {
            get;
            private set;
        }
    }
}
