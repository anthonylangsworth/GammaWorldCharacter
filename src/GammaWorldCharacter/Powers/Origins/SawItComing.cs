using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Hypercognitive"/> utility power "Saw it Coming".
    /// </summary>
    public class SawItComing: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="SawItComing"/>.
        /// </summary>
        public SawItComing()
            : base("Saw It Coming")
        {
            SetDescription("You anticipate your enemy's attack and respond accordingly.");    
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Psi, DamageTypes.None, EffectTypes.None, 
                ActionType.ImmediateInterrupt, "An enemy hits you.");
            SetAttackTypeAndRange(Range.Personal(Name));
            SetEffect("The triggering enemy rerolls the attack and must use the new result.");
        }
    }
}
