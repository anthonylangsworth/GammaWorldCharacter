using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Android"/> utility power "Be My Battery".
    /// </summary>
    public class BeMyBattery: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="BeMyBattery"/>.
        /// </summary>
        public BeMyBattery()
            : base("Be My Battery")
        {
            SetDescription("You transform energy attacks into reserve engery you use to protect and repair yourself.");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Dark, DamageTypes.None, EffectTypes.None, 
                ActionType.ImmediateInterrupt, "You take electricity, fire, laser or radiation damage.");
            SetAttackTypeAndRange(Range.Personal(Name));
            SetEffect("You gain temporary immunity to the triggering damage type until the start of your next turn. "
                + "You also gain {0} temporary hit points.", 
                new AbilityBonus("By My Battery temporary hit points", new []{ ScoreType.Intelligence }, 10));
        }
    }
}
