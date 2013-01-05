using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// Gain a bonus.
    /// </summary>
    public class GainBonusEffect : EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="score">
        /// The <see cref="CharacterScore"/> that gains the bonus. This
        /// cannot be null.
        /// </param>
        /// <param name="bonus">
        /// The amount of the bonus.
        /// </param>
        /// <param name="until">
        /// Then the bonus ends.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public GainBonusEffect(Target target, CharacterScore score, int bonus, Until until)
            : this(target, score, new ConstantValue(bonus), until )
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="score">
        /// The <see cref="CharacterScore"/> that gains the bonus. This
        /// cannot be null.
        /// </param>
        /// <param name="bonus">
        /// A score that, when calculated, gives the number of hit points gained.
        /// </param>
        /// <param name="until">
        /// When the bonus ends.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public GainBonusEffect(Target target, CharacterScore score, ICharacterScoreValue bonus, Until until)
            : base(target)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }
            if (bonus == null)
            {
                throw new ArgumentNullException("bonus");
            }

            this.Score = score;
            this.Bonus = bonus;
            this.Until = until;
        }

        /// <summary>
        /// The damage dealt.
        /// </summary>
        public ICharacterScoreValue Bonus
        {
            get;
            private set;
        }

        /// <summary>
        /// The damage dealt.
        /// </summary>
        public CharacterScore Score
        {
            get;
            private set;
        }

        /// <summary>
        /// When the bonus ends.
        /// </summary>
        public Until Until
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
            // TODO: Need to get a better representation of a score type
            yield return new EffectSpan(string.Format("gain a {0:+0} bonus to {1} until the {2}",
                Bonus.GetValue(character), Score.ScoreType, UntilHelper.ToString(Until)));
        }
    }
}
