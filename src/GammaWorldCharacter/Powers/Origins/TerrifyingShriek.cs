using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The Terrifying Screech novice <see cref="Hawkoid"/> power.
    /// </summary>
    public class TerrifyingShriek : AttackPower
    {
        /// <summary>
        /// Create a new <see cref="TerrifyingShriek"/>.
        /// </summary>
        public TerrifyingShriek()
            : base("Terrifying Shriek", typeof(Hawkoid), 1)
        {
            SetDescription("You make a piercing shriek that sends nearby creatures reeling in terror.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Bio, DamageTypes.Psychic, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Close, "burst 2");
            AddAttack(new AttackDetails("Each creature in burst",
                new AbilityPlusLevelBonus("Terrifying Shriek attack bonus", new ScoreType[] { ScoreType.Wisdom }, 1),
                new PowerDamage("Terrifying Shriek damage", new Dice(1, DiceType.d6)),
                new AbilityPlusLevelBonus("Terrifying Shriek damage bonus", new ScoreType[] { ScoreType.Wisdom }, 1),
                ScoreType.Fortitude, new ModifierSource[] { },
                "psychic damage and you slide the target 1 square.", null));
        }
    }
}
