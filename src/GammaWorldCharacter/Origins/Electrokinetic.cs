using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Electrokinetic <see cref="Origin"/>.
    /// </summary>
    public class Electrokinetic: Origin
    {
        /// <summary>
        /// Create a new <see cref="Electrokinetic"/>.
        /// </summary>
        public Electrokinetic()
            : base("Electrokinetic", ScoreType.Wisdom, PowerSource.Dark)
        {
            AddPower(new ElectricBoogaloo());
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
            addModifier(new Modifier(this, character[ScoreType.ElectricityResistance], 10));
        }

    }
}
