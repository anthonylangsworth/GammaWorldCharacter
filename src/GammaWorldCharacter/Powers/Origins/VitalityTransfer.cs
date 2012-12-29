using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Empath"/> novice power Vitality Transfer.
    /// </summary>
    public class VitalityTransfer: AttackPower
    {
        /// <summary>
        /// Create a new <see cref="VitalityTransfer"/>.
        /// </summary>
        public VitalityTransfer()
            : base("Vitality Transfer", typeof(Empath), 1)
        {
            SetDescription("First do no harm-not to anyone you like, anyway.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Psi, DamageTypes.None, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Ranged, "3");
            AddAttack(new AttackDetails("One creature",
                new AbilityPlusLevelBonus("Vitality Transfer attack bonus", new ScoreType[] { ScoreType.Charisma }, 1),
                null,
                null,
                ScoreType.Fortitude, new ModifierSource[] { new AbilityBonus("Vitality Transfer temporary hit points", new []{ ScoreType.Charisma }) },
                "The target is weakened until the end of your next turn. In addition, you or one ally within 5 suares of you gains {0} temporary hit points.", null));
        }
    }
}
