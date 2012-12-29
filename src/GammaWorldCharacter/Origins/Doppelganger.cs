using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers.Origins;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// The Doppelganger origin (see p38).
    /// </summary>
    public class Doppelganger: Origin
    {
        /// <summary>
        /// Create a new <see cref="Doppelganger"/>.
        /// </summary>
        public Doppelganger()
            : base("Doppelganger", ScoreType.Intelligence, PowerSource.Dark,
            "Deal 1d10 extra damage and you can use double trouble as a free action.")
        {
            AddTrait(new Trait("Two Possibilities", 
                "Whenever you draw an Alpha Mutation card, draw two cards from the same deck and choose which one to keep. Put the other on the bottom of the deck."));
            AddPower(new DoubleTrouble());
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
            addModifier(new Modifier(this, character[ScoreType.Conspiracy], 4));
        }
    }
}
