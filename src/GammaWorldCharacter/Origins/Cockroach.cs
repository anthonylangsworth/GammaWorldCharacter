using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Cockroach Origin (p 37).
    /// </summary>
    public class Cockroach: Origin
    {
        /// <summary>
        /// Create a new <see cref="Cockroach"/>.
        /// </summary>
        public Cockroach()
            : base("Cockroach", ScoreType.Constitution, PowerSource.Bio)
        {
            AddTrait(new AdditionalMovementMode("Bug Legs", 
                "You can climb your speed. You can even climb upside down across horizontal surfaces. You can't attack while climbing.", ScoreType.Climb));
            AddPower(new EauDeRoach());
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
            addModifier(new Modifier(this, character[ScoreType.Mechanics], 4));
        }
    }
}
