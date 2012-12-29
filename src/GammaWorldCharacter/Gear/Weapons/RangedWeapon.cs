using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear.Weapons
{
    /// <summary>
    /// A ranged weapon.
    /// </summary>
    public class RangedWeapon: Weapon
    {
        /// <summary>
        /// Create a new <see cref="RangedWeapon"/>.
        /// </summary>
        /// <param name="rangedType">
        /// Is the weapon a gun?
        /// </param>
        /// <param name="handedness">
        /// Is the weapon one or two handed?
        /// </param>
        /// <param name="weight">
        /// is the weapon light or heavy?
        /// </param>
        public RangedWeapon(RangedType rangedType, WeaponHandedness handedness, WeaponWeight weight)
            : base(WeaponHelper.GetRangedWeaponName(rangedType, handedness, weight), handedness, weight) 
        {
            Type = rangedType;
        }

        /// <summary>
        /// The melee weapon's accuracy bonus.
        /// </summary>
        public override int AccuracyBonus
        {
            get 
            {
                int result;

                result = 0;
                if (Weight == WeaponWeight.Light)
                {
                    if (Type == RangedType.Weapon)
                    {
                        result = 3;
                    }
                    else if (Type == RangedType.Gun)
                    {
                        result = 4;
                    }
                }
                else if (Weight == WeaponWeight.Heavy)
                {
                    result = 2;
                }

                return result;
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
                if (Weight == WeaponWeight.Light)
                {
                    if (Handedness == WeaponHandedness.OneHanded)
                    {
                        if (Type == RangedType.Weapon)
                        {
                            result = new Dice(1, DiceType.d8);
                        }
                        else if (Type == RangedType.Gun)
                        {
                            result = new Dice(1, DiceType.d8);
                        }
                    }
                    else if (Handedness == WeaponHandedness.TwoHanded)
                    {
                        if (Type == RangedType.Weapon)
                        {
                            result = new Dice(1, DiceType.d12);
                        }
                        else if (Type == RangedType.Gun)
                        {
                            result = new Dice(1, DiceType.d12);
                        }
                    }
                }
                else if (Weight == WeaponWeight.Heavy)
                {
                    if (Handedness == WeaponHandedness.OneHanded)
                    {
                        if (Type == RangedType.Weapon)
                        {
                            result = new Dice(1, DiceType.d10);
                        }
                        else if (Type == RangedType.Gun)
                        {
                            result = new Dice(2, DiceType.d6);
                        }
                    }
                    else if (Handedness == WeaponHandedness.TwoHanded)
                    {
                        if (Type == RangedType.Weapon)
                        {
                            result = new Dice(2, DiceType.d8);
                        }
                        else if (Type == RangedType.Gun)
                        {
                            result = new Dice(2, DiceType.d10);
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Whether the weapon is a gun or not.
        /// </summary>
        public RangedType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// The weapon's max range.
        /// </summary>
        public int Range
        {
            get
            {
                int result;

                // Looking at the table on p74 in Ch 3, weight is irrelevant

                result = 0;
                if (Handedness == WeaponHandedness.OneHanded && Type == RangedType.Weapon)
                {
                    result = 5;
                }
                else if (Handedness == WeaponHandedness.OneHanded && Type == RangedType.Gun)
                {
                    result = 10;
                }
                else if (Handedness == WeaponHandedness.TwoHanded && Type == RangedType.Weapon)
                {
                    result = 10;
                }
                else if (Handedness == WeaponHandedness.TwoHanded && Type == RangedType.Gun)
                {
                    result = 20;
                }

                return result;
            }
        }
    }
}
