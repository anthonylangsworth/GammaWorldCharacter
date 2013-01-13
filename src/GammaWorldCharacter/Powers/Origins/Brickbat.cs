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
            : base("Brickbat")
        {
            SetDescription("You spin in a circle with your weapon, knocking down a wide swathe of foes.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Bio, DamageTypes.Physical, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(Range.CloseBurst(Name, 1));
            AddAttack("Each enemy in burst you can see", Hand.Main, ScoreType.Strength, ScoreType.ArmorClass, 1, 0,
                "physical damage and you knock the target prone", null);
        }
    }
}
