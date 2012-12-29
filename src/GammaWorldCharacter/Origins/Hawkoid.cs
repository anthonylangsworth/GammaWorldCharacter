using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Hawkoid origin (p44).
    /// </summary>
    public class Hawkoid: Origin
    {
        /// <summary>
        /// Create a new <see cref="Hawkoid"/>.
        /// </summary>
        public Hawkoid()
            : base("Hawkoid", ScoreType.Wisdom, PowerSource.Bio,
            "Deal 1d10 extra damage and you can fly your speed as a free action.")
        {
            AddTrait(new AdditionalMovementMode("Flight", 
                "You have a fly speed equal to your speed. Whilst flying, you take a -2 penalty to attack rolls.", ScoreType.Fly));
            AddPower(new TerrifyingShriek());
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

            addModifier(new Modifier(this, character[ScoreType.Perception], 4));
        }
    }
}
