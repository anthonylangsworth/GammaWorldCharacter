using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Empath"/> utility power "Share Strength".
    /// </summary>
    public class ShareStrength: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="ShareStrength"/>.
        /// </summary>
        public ShareStrength()
            : base("Share Strength")
        {
            SetDescription("You link the life forces of two allies together, allowing one to use his or her vitality to heal the other.");    
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Psi, DamageTypes.Psychic, EffectTypes.Healing, ActionType.Standard, null);
            SetAttackTypeAndRange(Range.CloseBurst(Name, 5));
            SetTarget("You and one ally in burst, or two allies in burst.");
            SetEffect("One target of your choice takes 10 psychic damage and the other target regains 10 hit points and makes a saving through.");
        }
    }
}
