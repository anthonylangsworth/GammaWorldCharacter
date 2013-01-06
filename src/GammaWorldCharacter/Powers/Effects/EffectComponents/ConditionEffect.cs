using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// Impose a <see cref="Condition"/> like immobilized or stunned (see condition descriptions on GW85).
    /// </summary>
    public class ConditionEffect: EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="ConditionEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> that is immobilized.
        /// </param>
        /// <param name="condition">
        /// The <see cref="Condition"/> imposed on the target.
        /// </param>
        /// <param name="until">
        /// Then the immoblization ceases.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="target"/> cannot be null.
        /// </exception>
        public ConditionEffect(Target target, Condition condition, Until until)
            : base(target)
        {
            this.Condition = condition;
            this.Until = until;
        }

        /// <summary>
        /// The condition imposed.
        /// </summary>
        public Condition Condition
        {
            get;
            set;
        }

        /// <summary>
        /// When the immobilization ceases.
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
            
            yield return new EffectSpan(string.Format("is {0} {1}",
                Condition.ToString().ToLower(), UntilHelper.ToString(Until)));
        }
    }
}
