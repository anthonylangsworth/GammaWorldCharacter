using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Cockroach"/> utility power "Hard to Kill".
    /// </summary>
    public class HardToKill: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="HardToKill"/>.
        /// </summary>
        public HardToKill()
            : base("Hard To Kill")
        {
            SetDescription("When others count you out, your roach exoskeleton gives you a second chance.");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Bio, DamageTypes.None, EffectTypes.Healing, 
                ActionType.ImmediateInterrupt, "You drop to 0 hit points.");
            SetAttackTypeAndRange(Range.Personal(Name));
            SetEffect("You regain {0} hitpoints.", new ScoreBonus("Hard to kill hit points", 10, ScoreType.Level));
        }
    }
}
