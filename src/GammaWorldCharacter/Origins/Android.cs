using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Android origin (see p36).
    /// </summary>
    public class Android: Origin
    {
        /// <summary>
        /// Create a new <see cref="Android"/>.
        /// </summary>
        public Android()
            : base("Android", ScoreType.Intelligence, PowerSource.Dark,
            "Deal 1d10 extra damage and the target grants combat advantage to you until the end of the encounter.")
        {
            AddTrait(new Trait("Machine Powered", "You do not need to eat, drink or breathe"));
            AddPower(new MachineGrip());
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

            addModifier(new Modifier(this, character[ScoreType.Fortitude], 2));
            addModifier(new Modifier(this, character[ScoreType.Science], 4));
        }
    }
}
