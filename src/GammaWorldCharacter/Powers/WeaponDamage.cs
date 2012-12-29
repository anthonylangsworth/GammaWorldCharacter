using System;
using System.Collections.Generic;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// The damage done by a weapon-based attack power.
    /// </summary>
    /// <remarks>
    /// Some powers allow a basic melee attack. This intentionally excludes such a rarely used ability.
    /// </remarks>
    public class WeaponDamage : PowerDamage
    {
        /// <summary>
        /// Create a new <see cref="WeaponDamage"/> that uses the currently equipped weapon
        /// or unarmed damage if no weapon is equipped.
        /// </summary>
        /// <param name="name">
        /// The name of the damage, usually "[Power] damage".
        /// </param>
        /// <param name="hand">
        /// The hand holding the weapon to use for damage.
        /// </param>
        /// <param name="multiplier">
        /// The number of times the weapon damage dice are rolled.
        /// </param>
        /// <exception cref="ArgumentException">
        /// multiplier must be positive.
        /// </exception>
        public WeaponDamage(string name, Hand hand, int multiplier)
            : base(name)
        {
            if (multiplier < 1)
            {
                throw new ArgumentException("multiplier must be positive", "multiplier");
            }

            Hand = hand;
            Multiplier = multiplier;
        }

        /// <summary>
        /// The hand of the weapon to use as the damage.
        /// </summary>
        public Hand Hand
        {
            get;
            private set;
        }

        /// <summary>
        /// The number of times the weapon damage dice are rolled.
        /// </summary>
        public int Multiplier
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
        /// <exception cref="InvalidOperationException">
        /// The character must be wielding a weapon.
        /// </exception>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            Weapon weapon;

            base.AddModifiers(stage, addModifier, character);

            weapon = character.GetHeldItem<Weapon>(Hand);
            if (weapon != null)
            {
                SetDice(new Dice(Multiplier * weapon.Damage.Number,
                    weapon.Damage.DiceType));
            }

            // Do nothing if the weapon is not present. Certain powers like a ranger's TwinStrike may
            // not be available depending on certain weapon configurations.
        }
    }
}
