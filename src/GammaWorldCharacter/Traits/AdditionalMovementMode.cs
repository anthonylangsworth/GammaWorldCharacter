using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Traits
{
    /// <summary>
    /// An additional movement mode, such as the limb speed of the Cockroach 
    /// </summary>
    public class AdditionalMovementMode: Trait
    {
        /// <summary>
        /// Create an <see cref="AdditionalMovementMode"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the trait. This cannot be null.
        /// </param>
        /// <param name="description">
        /// The description of the trait. This can be null.
        /// </param>
        /// <param name="movementMode">
        /// The name of the movement mode (e.g. "Climb" or "Fly").
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="movementMode"/> cannot be ScoreType.Speed or a non-movement mode.
        /// </exception>
        public AdditionalMovementMode(string name, string description, ScoreType movementMode)
            : base(name, description)
        {
            if (!ScoreTypeHelper.MovementModes.Contains(movementMode))
            {
                throw new ArgumentException("Not a movement mode", "movementMode");
            }
            if (movementMode == ScoreType.Speed)
            {
                throw new ArgumentException("Cannot be ScoreType.Speed", "movementMode");
            }

            MovementMode = movementMode;
        }

        /// <summary>
        /// The name of the movement mode.
        /// </summary>
        public ScoreType MovementMode
        {
            get;
            private set;
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

            addModifier(new Modifier(character[ScoreType.Speed], character[MovementMode], character[ScoreType.Speed].Total));
            addModifier(new Modifier(this, character[MovementMode], 0, string.Format("see {0}", Name)));
        }
    }
}
