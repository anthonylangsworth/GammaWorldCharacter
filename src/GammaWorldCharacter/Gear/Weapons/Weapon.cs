using System;
using System.Collections.Generic;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Scores;

namespace GammaWorldCharacter.Gear.Weapons
{
    /// <summary>
    /// A weapon, e.g. longsword or crossbow.
    /// </summary>
    public abstract class Weapon: Item
    {
        /// <summary>
        /// Create a new <see cref="Weapon"/>.
        /// </summary>
        /// <param name="name">
        /// The weapon's name.
        /// </param>
        /// <param name="handedness">
        /// Is the weapon one or two handed?
        /// </param>
        /// <param name="weight">
        /// is the weapon light or heavy?
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> cannot be null or empty.
        /// </exception>
        protected Weapon(string name, WeaponHandedness handedness, WeaponWeight weight)
            : base(name, Slot.Weapon)
        {
            Handedness = handedness;
            Weight = weight;
        }

        /// <summary>
        /// Get the ability scores used for attack and damage with the given <see cref="WeaponWeight"/>.
        /// </summary>
        public IEnumerable<ScoreType> BasicAttackAbilityScores
        {
            get 
            {
                return Weight == WeaponWeight.Heavy ?
                    new[] { ScoreType.Strength, ScoreType.Constitution } :
                    new[] { ScoreType.Dexterity, ScoreType.Intelligence };
            }
        }

        /// <summary>
        /// The damage the weapon does with a basic attack.
        /// </summary>
        public abstract Dice Damage
        {
            get;
        }

        /// <summary>
        /// The number of hands required to wield this weapon effectively.
        /// </summary>
        public WeaponHandedness Handedness
        {
            get;
            private set;
        }

        /// <summary>
        /// The weapon's weight class.
        /// </summary>
        public WeaponWeight Weight
        {
            get;
            private set;
        }

        /// <summary>
        /// the bonus to attack rolls if the user is proficient with this weapon.
        /// </summary>
        public abstract int AccuracyBonus
        {
            get;
        }
    }
}
