using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects;
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
                Effect.TheTarget.SuffersDamage(1.D10()).And.TheTarget.GrantsCombatAdvantage(To.You, Until.EndOfEncounter))
        {
            AddTrait(new Trait("Machine Powered", "You do not need to eat, drink or breathe"));
            NovicePower = new MachineGrip();
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
