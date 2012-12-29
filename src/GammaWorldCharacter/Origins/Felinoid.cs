using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Felinoid origin.
    /// </summary>
    public class Felinoid: Origin
    {
        /// <summary>
        /// Create an <see cref="Empath"/>.
        /// </summary>
        public Felinoid()
            : base("Felinoid", ScoreType.Dexterity, PowerSource.Bio)
        {
            AddTrait(new Trait("Catfall", "You take no damage from falls of 50 feet or less and you always land on your feet when you fall."));
            AddPower(new SlashingClaws());
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

            addModifier(new Modifier(this, character[ScoreType.Stealth], 4));
            addModifier(new Modifier(this, character[ScoreType.Reflex], 2));
            if (!character.IsWearingHeavyArmor())
            {
                addModifier(new Modifier(this, character[ScoreType.Speed], 1));
            }
        }
    }
}
