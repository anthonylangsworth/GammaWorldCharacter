using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Empath origin.
    /// </summary>
    public class Empath: Origin
    {
        /// <summary>
        /// Create an <see cref="Empath"/>.
        /// </summary>
        public Empath()
            : base("Empath", ScoreType.Charisma, PowerSource.Psi)
        {
            AddTrait(new Trait("Pacifying Aura", "You and each ally adjacent to you never grant combat advantage."));
            AddTrait(new Trait("Vital Presence", "Allies adjacent to you get a +5 bonus to death saving throws."));
            AddPower(new VitalityTransfer());
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
        }
    }
}
