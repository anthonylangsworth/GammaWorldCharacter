using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Gear.Armor
{
    /// <summary>
    /// A piece of armor, such as cloth, hide, leather or plate.
    /// </summary>
    public abstract class Armor: Item
    {
        /// <summary>
        /// Create a new <see cref="Armor"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the armor.
        /// </param>
        /// <param name="armorBonus">
        /// The AC bonus the armor provides.
        /// </param>
        /// <param name="speedPenalty">
        /// The reduction in speed when wearing this armor.
        /// </param>
        /// <param name="slot">
        /// The equipment slot the armor occupies.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Neither armorBonus nor enhancementBonus can be negative. Neither armorCheckPenalty nor speedPenalty 
        /// can be positive.
        /// </exception>
        protected Armor(string name, int armorBonus, int speedPenalty, Slot slot)
            : base(name, slot)
        {
            if (armorBonus < 0)
            {
                throw new ArgumentException("armorBonus cannot be negative", "armorBonus");
            }
            if (speedPenalty > 0)
            {
                throw new ArgumentException("speedPenalty cannot be positive", "speedPenalty");
            }

            ArmorBonus = armorBonus;
            SpeedPenalty = speedPenalty;
        }

        /// <summary>
        /// The total armor bonus provided with this item, including a magic bonus, if any.
        /// </summary>
        public int ArmorBonus
        {
            get;
            private set;
        }

        /// <summary>
        /// The reduction in speed when wearing this armor.
        /// </summary>
        public int SpeedPenalty
        {
            get;
            private set;
        }

        /// <summary>
        /// Add modifiers based on this score's current value(s).
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

            // Add the AC bonus
            addModifier(new Modifier(this, character[ScoreType.ArmorClass], ArmorBonus));

            if (SpeedPenalty < 0)
            {
                addModifier(new Modifier(this, character[ScoreType.Speed], SpeedPenalty));
            }
        }
    }
}
