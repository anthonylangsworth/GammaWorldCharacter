using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Doppelganger"/> utility power "Two Places At Once".
    /// </summary>
    public class TwoPlacesAtOnce: UtilityPower
    {
        /// <summary>
        /// Create a <see cref="TwoPlacesAtOnce"/>.
        /// </summary>
        public TwoPlacesAtOnce()
            : base("Two Places at Once")
        {
            SetDescription("You're literallty in two places at once.");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Dark, DamageTypes.None, EffectTypes.Teleportation, 
                ActionType.Minor, null);
            SetAttackTypeAndRange(Range.Personal(Name));
            SetEffect("Choose an unoccupied square within 5 squares of you. You simultaneously occupy that square and your current square. "
                + "Before the start of your next turn, you can teleport to the chosen square as a free action.");
        }
    }
}
