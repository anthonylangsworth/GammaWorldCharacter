using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Weapons
{
    /// <summary>
    /// A melee weapon.
    /// </summary>
    public class MeleeWeapon: Weapon
    {
        /// <summary>
        /// Create a new <see cref="MeleeWeapon"/>.
        /// </summary>
        /// <param name="handedness">
        /// Is the weapon one or two handed?
        /// </param>
        /// <param name="weight">
        /// is the weapon light or heavy?
        /// </param>
        public MeleeWeapon(WeaponHandedness handedness, WeaponWeight weight)
            : base(WeaponHelper.GetMeleeWeaponName(handedness, weight), handedness, weight) 
        {
            // Do nothing
        }

        /// <summary>
        /// The melee weapon's accuracy bonus.
        /// </summary>
        public override int AccuracyBonus
        {
            get 
            {
                return Weight == WeaponWeight.Heavy ? 2 : 3;
            }
        }

        /// <summary>
        /// The damage the weapon does.
        /// </summary>
        public override Dice Damage
        {
            get 
            {
                Dice result;

                result = null;
                if (Weight == WeaponWeight.Light && Handedness == WeaponHandedness.OneHanded)
                {
                    result = new Dice(1, DiceType.d8);
                }
                else if (Weight == WeaponWeight.Light && Handedness == WeaponHandedness.TwoHanded)
                {
                    result = new Dice(1, DiceType.d12);
                }
                else if (Weight == WeaponWeight.Heavy && Handedness == WeaponHandedness.OneHanded)
                {
                    result = new Dice(1, DiceType.d10);
                }
                else if (Weight == WeaponWeight.Heavy && Handedness == WeaponHandedness.TwoHanded)
                {
                    result = new Dice(2, DiceType.d8);
                }

                return result;
            }
        }
    }
}
