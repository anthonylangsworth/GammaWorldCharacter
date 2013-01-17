using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Hypercognitive <see cref="Origin"/> (p46).
    /// </summary>
    public class Hypercognitive: Origin
    {
        /// <summary>
        /// Create a new <see cref="Hawkoid"/>.
        /// </summary>
        public Hypercognitive()
            : base("Hypercognitive", ScoreType.Wisdom, PowerSource.Psi,
                Effect.TheTarget.SuffersDamage(1.D10()).And.YouOrAlly(Where.WithinSquares(5, Of.You)).GainsModifiers(Your.Defenses, 2, Until.EndOfYourNextTurn))
        {
            NovicePower = new UncannyStrike();
            UtilityPower = new SawItComing();
        }

        /// <summary>
        /// Add modifiers.
        /// </summary>
        /// <param name="stage">
        /// The stage during character update this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The character to add modifiers for.
        /// </param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);

            addModifier(new Modifier(this, character[ScoreType.Insight], 4));
            addModifier(new Modifier(this, character[ScoreType.Reflex], 2));
            addModifier(new Modifier(this, character[ScoreType.Initiative], 8));
        }
    }
}
