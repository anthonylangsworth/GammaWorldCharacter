using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Giant"/> origin Novice power.
    /// </summary>
    public class Brickbat: WeaponAttackPower
    {
        /// <summary>
        /// Create a new <see cref="Brickbat"/>.
        /// </summary>
        public Brickbat()
            : base("Brickbat", typeof(Giant), 1)
        {
            SetDescription("You spin in a circle with your weapon, knocking down a wide swathe of foes.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Bio, DamageTypes.Physical, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Close, "burst 1");
            AddAttack("Each enemy in burst you can see", Hand.Main, ScoreType.Strength, ScoreType.ArmorClass, 1, 0,
                "physical damage and you knock the target prone", null);
        }

        /// <summary>
        /// This power is only usable if the character is wielding a melee weapon.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public override bool IsUsable(Character character)
        {
            return base.IsUsable(character)
                && character.GetHeldItem<MeleeWeapon>(Hand.Main) != null;
        }
    }
}
