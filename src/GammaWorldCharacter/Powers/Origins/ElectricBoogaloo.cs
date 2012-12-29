using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Electrokinetic"/> Novice power Electric Boogaloo.
    /// </summary>
    public class ElectricBoogaloo: AttackPower
    {
        /// <summary>
        /// Create a new <see cref="ElectricBoogaloo"/>.
        /// </summary>
        public ElectricBoogaloo()
            : base("Electric Boogaloo", typeof(Electrokinetic), 1)
        {
            SetDescription("You zap your enemy with an arc of electricity, making your foe jerk and dance around like a spaz.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Dark, DamageTypes.Electricity,
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Melee, "1");
            AddAttack(new AttackDetails("One creature",
                new AbilityPlusLevelBonus("Electric Boogaloo attack bonus", new ScoreType[] { ScoreType.Wisdom }, 1),
                new PowerDamage("Electric Boogaloo damage", 1.D10()),
                new AbilityPlusLevelBonus("Electric Boogaloo damage bonus", new ScoreType[] { ScoreType.Wisdom }, 2),
                ScoreType.Fortitude, new ModifierSource[] { },
                "electricity damage and the target takes a -2 penalty to all defenses until the end of your next turn.", null));
        }
    }
}
