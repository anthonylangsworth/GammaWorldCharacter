using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The Eau de Roach novice <see cref="Cockroach"/> power.
    /// </summary>
    public class EauDeRoach : AttackPower
    {
        /// <summary>
        /// Create a new <see cref="MachineGrip"/>.
        /// </summary>
        public EauDeRoach()
            : base("Eau de Roach", typeof(Cockroach), 1)
        {
            SetDescription("You spit at your foe. The spit is a combination of excrement, scent gland fluid, regurgitated food and stomach acid. Yep, it's nasty and it burns your foe and forces it away from you.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Bio, DamageTypes.Acid, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Melee, "1");
            AddAttack(new AttackDetails("One creature",
                new AbilityPlusLevelBonus("Eau de Roach attack bonus", new ScoreType[] { ScoreType.Constitution }, 1),
                new PowerDamage("Eau de Roach damage", 2.D8()),
                new AbilityPlusLevelBonus("Eau de Roach damage bonus", new ScoreType[] { ScoreType.Constitution }, 2),
                ScoreType.Fortitude, new ModifierSource[] { },
                "acid damage and you push the target 1 square.", null));
        }
    }
}
