using System;
using System.Collections.Generic;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// An attack bonus with a weapon equipped by the character.
    /// </summary>
    public class WeaponAttackBonus: AbilityPlusLevelBonus
    {
        /// <summary>
        /// Create a new <see cref="WeaponAttackBonus"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the attack bonus, usually "[Power] attack bonus".
        /// </param>
        /// <param name="abilityScores">
        /// The ability scores whose modifiers are added to the bonus.
        /// </param>
        /// <param name="hand">
        /// The hand of the weapon this attack is for.
        /// </param>
        public WeaponAttackBonus(string name, IList<ScoreType> abilityScores, Hand hand)
            : base(name, abilityScores, 1)
        {
            Hand = hand;
        }

        /// <summary>
        /// Create a new <see cref="WeaponAttackBonus"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the attack bonus, usually "[Power] attack bonus".
        /// </param>
        /// <param name="abilityScores">
        /// The ability scores whose modifiers are added to the bonus.
        /// </param>
        /// <param name="hand">
        /// The hand of the weapon this attack is for.
        /// </param>
        /// <param name="bonus">
        /// An additional bonus to the attack.
        /// </param>
        public WeaponAttackBonus(string name, IList<ScoreType> abilityScores, Hand hand, int bonus)
            : base(name, abilityScores, bonus)
        {
            Hand = hand;
        }

        /// <summary>
        /// The hand of the weapon this attack is for.
        /// </summary>
        public Hand Hand
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
            Weapon weapon;

            base.AddModifiers(stage, addModifier, character);

            weapon = character.GetHeldItem<Weapon>(Hand);
            if (weapon != null)
            {
                // Add the accuracy bonus
                addModifier(new Modifier(weapon, this, weapon.AccuracyBonus));
            }
        }
    }
}
