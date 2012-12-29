using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Gravity Controller origin (p43).
    /// </summary>
    public class GravityController: Origin
    {
        /// <summary>
        /// Create a new <see cref="Cockroach"/>.
        /// </summary>
        public GravityController()
            : base("Gravity Controller", ScoreType.Constitution, PowerSource.Dark,
            "Deal 1d10 extra damage and one creature within 2 squares of the target is immobilized until the end of your next turn.")
        {
            AddTrait(new Trait("Gravity by Choice", "You take no damage from falling."));
            AddPower(new GravitationalPulse());
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

            addModifier(new Modifier(this, character[ScoreType.Reflex], 2));
            addModifier(new Modifier(this, character[ScoreType.Athletics], 4));
        }
    }
}
