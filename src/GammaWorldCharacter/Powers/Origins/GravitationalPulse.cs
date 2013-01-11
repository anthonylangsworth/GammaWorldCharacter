using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The Gravity Controller novice power.
    /// </summary>
    public class GravitationalPulse: AttackPower
    {
        /// <summary>
        /// Create a new <see cref="GravitationalPulse"/>.
        /// </summary>
        public GravitationalPulse()
            : base("Gravitational Pulse", typeof(GravityController), 1)
        {
            SetDescription("You unleach a flood of gravitons that swarm your foe dragging down its every step.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Dark, DamageTypes.Physical, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Ranged, "10");
            AddAttack(new AttackDetails("One creature",
                new AbilityPlusLevelBonus("Gravitational Pulse attack bonus", new ScoreType[] { ScoreType.Constitution }, 1),
                new PowerDamage("Gravitational Pulse damage", 1.D10()),
                new AbilityPlusLevelBonus("Gravitational Pulse damage bonus", new ScoreType[] { ScoreType.Constitution }, 2),
                ScoreType.Fortitude, new ModifierSource[] { },
                "physical damage and the target is slowed until the end of your next turn.", null));
        }
    }
}
