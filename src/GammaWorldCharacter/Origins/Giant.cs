using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Giant origin (p42).
    /// </summary>
    public class Giant: Origin
    {
        /// <summary>
        /// Create a new <see cref="Giant"/>.
        /// </summary>
        public Giant()
            : base("Giant", ScoreType.Strength, PowerSource.Bio,
                Effect.TheTarget.SuffersDamage(1.D10()).And.TheTarget.Pushed(3))
        {
            AddTrait(new Trait("Encumbered Speed", "You move your speed, even while wearing heavy armor or carrying a heavy load."));
            AddPower(new Brickbat());
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

            addModifier(new Modifier(this, character[ScoreType.Athletics], 4));
            addModifier(new Modifier(this, character[ScoreType.Fortitude], 2));

            if (character.IsWearingHeavyArmor())
            {
                addModifier(new Modifier(this, character[ScoreType.Speed], 1));
            }
        }
    }
}
