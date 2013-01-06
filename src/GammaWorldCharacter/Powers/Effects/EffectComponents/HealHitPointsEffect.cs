using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// Heal the target, i.e. give it hit points.
    /// </summary>
    public class HealHitPointsEffect : EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="HealHitPointsEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="hitPoints">
        /// The number of hit points healed. This must be positive.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public HealHitPointsEffect(Target target, int hitPoints)
            : this(target, new ConstantValue(hitPoints))
        {
            if (hitPoints <= 0)
            {
                throw new ArgumentException("hitPoints");
            }
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
        public HealHitPointsEffect(Target target, ICharacterScoreValue characterScoreValue)
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

        /// <summary>
        /// Return <see cref="EffectSpan"/>s representing a human 
        /// readable display.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to base the representation from.
        /// This cannot be null.
        /// </param>
        /// <returns>
        /// <see cref="EffectSpan"/>s representing this component.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="character"/> cannot be null.
        /// </exception>
        public override IEnumerable<EffectSpan> Parse(Character character)
        {
            yield return new EffectSpan(string.Format("regains {0} hit points",
                TemporaryHitPoints.GetValue(character)));
        }
    }
}
