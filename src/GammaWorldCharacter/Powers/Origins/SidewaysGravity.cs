using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="GravityController"/> utility power "Sideways Gravity".
    /// </summary>
    public class SidewaysGravity: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="SidewaysGravity"/>.
        /// </summary>
        public SidewaysGravity()
            : base("Sideways Gravity")
        {
            SetDescription("An enemy moves next to you, and you send it away in a flash of quantum radiance.");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Dark, DamageTypes.None, EffectTypes.None,
                ActionType.ImmediateReaction, "An enemy enters a square adjacent to you.");
            SetAttackTypeAndRange(Range.Personal(Name));
            SetTarget("The triggering enemy");
            SetEffect("You slide the target 6 squares.");
        }
    }
}
