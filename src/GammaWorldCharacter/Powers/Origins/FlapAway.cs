using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Hawkoid"/> utility power "Flap Away".
    /// </summary>
    public class FlapAway: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="FlapAway"/>.
        /// </summary>
        public FlapAway()
            : base("Flap Away")
        {
            SetDescription("With a flap of your wings, you quickly move away from a foe.");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Bio, DamageTypes.None, EffectTypes.None, 
                ActionType.ImmediateReaction, "An enemy enters a square adjacent to you.");
            SetAttackTypeAndRange(Range.Personal(Name));
            SetEffect("You fly 2 squares without provoking opportunity attacks. If you don't land at the end of this movement, you fall.");
        }
    }
}
