using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Giant"/> utility power "Focused Strength".
    /// </summary>
    public class FocusedStrength: UtilityPower
    {
        /// <summary>
        /// Createa a new <see cref="FocusedStrength"/>.
        /// </summary>
        public FocusedStrength()
            : base("Focused Strength")
        {
            SetDescription("You flex your large muscles, bringing every ounce of strength to bear.");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Bio, DamageTypes.None, EffectTypes.None, ActionType.Minor, null);
            SetAttackTypeAndRange(Range.Personal(Name));
            SetEffect("You gain a +5 power bonus to damage rolls with melee attacks until the start of your next turn.");
        }
    }
}
