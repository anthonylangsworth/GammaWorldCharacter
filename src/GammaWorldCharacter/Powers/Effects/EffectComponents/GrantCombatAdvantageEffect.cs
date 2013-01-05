using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// Grant combat advantage.
    /// </summary>
    public class GrantCombatAdvantageEffect: EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="PushEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="to">
        /// Who combat advantage is granted to.
        /// </param>
        /// <param name="until">
        /// When the effect will end.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>        
        public GrantCombatAdvantageEffect(Target target, To to, Until until)
            : base(target)
        {
            this.To = to;
            this.Until = until;
        }

        /// <summary>
        /// When the target ceases granting combat advantage.
        /// </summary>
        public Until Until
        {
            get;
            private set;
        }

        /// <summary>
        /// Who combat advantage applies to.
        /// </summary>
        public To To
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
            yield return new EffectSpan(string.Format("grants combat advantage to {0} until the {1}",
                To.ToString().ToLower(), UntilHelper.ToString(Until)));
        }
    }
}
