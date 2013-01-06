using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// Gain a bonus or penalty.
    /// </summary>
    /// <remarks>
    /// This may be modified to take a collection of scores or somehow add bonuses to multiple scores at once.
    /// </remarks>
    public class GainModifierEffect : EffectComponent
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
        public GainModifierEffect(Target target, CharacterScore score, int bonus, Until until)
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
        /// <param name="modifier">
        /// A score that, when calculated, gives the bonus or penalty.
        /// </param>
        /// <param name="until">
        /// When the bonus ends.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public GainModifierEffect(Target target, CharacterScore score, ICharacterScoreValue modifier, Until until)
            : base(target)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }
            if (modifier == null)
            {
                throw new ArgumentNullException("modifier");
            }

            this.Scores = new List<CharacterScore>(new [] { score });
            this.Modifier = modifier;
            this.Until = until;
        }

        /// <summary>
        /// Create a new <see cref="DiceDamageEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="scores">
        /// The <see cref="CharacterScore"/>s that gain the bonus. This 
        /// cannot be null or contain null.
        /// </param>
        /// <param name="modifier">
        /// A score that, when calculated, gives the bonus or penalty.
        /// </param>
        /// <param name="until">
        /// When the bonus ends.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public GainModifierEffect(Target target, IEnumerable<CharacterScore> scores, ICharacterScoreValue modifier, Until until)
            : base(target)
        {
            if (scores == null)
            {
                throw new ArgumentNullException("scores");
            }
            if (scores.Any(x => x == null))
            {
                throw new ArgumentNullException("scores");
            }
            if (modifier == null)
            {
                throw new ArgumentNullException("modifier");
            }

            this.Scores = new List<CharacterScore>(scores);
            this.Modifier = modifier;
            this.Until = until;
        }

        /// <summary>
        /// The bonus or penalty.
        /// </summary>
        public ICharacterScoreValue Modifier
        {
            get;
            private set;
        }

        /// <summary>
        /// The damage dealt.
        /// </summary>
        public IEnumerable<CharacterScore> Scores
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
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            StringBuilder stringBuilder;

            stringBuilder = new StringBuilder();
            if (Target.TargetType == TargetType.You || Target.TargetType == TargetType.TheTarget)
            {
                stringBuilder.Append("gain");
            }
            else
            {
                stringBuilder.Append("gains");
            }
            stringBuilder.AppendFormat(" a {0:+0} bonus to ", Modifier.GetValue(character));
            if (Scores.OrderBy(x => x.ScoreType).SequenceEqual(Your.Defenses.OrderBy(x => x.ScoreType)))
            {
                stringBuilder.Append("all defenses");
            }
            else
            {
                stringBuilder.Append(Scores.Aggregate(new StringBuilder(),
                    (accumulator, current) =>
                        {
                            if(string.IsNullOrEmpty(accumulator.ToString()))
                            {
                                accumulator.Append(ScoreTypeHelper.ToString(current.ScoreType));
                            }
                            else if (!ReferenceEquals(current, Scores.Last()))
                            {
                                accumulator.AppendFormat(", {0}", ScoreTypeHelper.ToString(current.ScoreType));
                            }
                            else
                            {
                                accumulator.AppendFormat(" and {0}", ScoreTypeHelper.ToString(current.ScoreType));
                            }
                            return accumulator;
                        }));
            }
            stringBuilder.AppendFormat(" {0}", UntilHelper.ToString(Until));
            yield return new EffectSpan(stringBuilder.ToString());
        }
    }
}
