using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Felinoid"/> novice power SlashingClaws.
    /// </summary>
    public class SlashingClaws: AttackPower
    {
        /// <summary>
        /// Create a new <see cref="SlashingClaws"/>.
        /// </summary>
        public SlashingClaws()
            : base("Slashing Claws", typeof(Felinoid), 1)
        {
            SetDescription("You rake at your foe's face with a lighting fast flurry of razor sharp claws.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Bio, DamageTypes.Physical, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Melee, "1");
            AddAttack(new AttackDetails("One creature, roll twice",
                new AbilityPlusLevelBonus("Slashing Claws attack bonus", new ScoreType[] { ScoreType.Dexterity }, 1),
                null,
                null,
                ScoreType.Reflex, 
                new [] { new AbilityPlusLevelBonus("Slashing Claws damage bonus", new[] { ScoreType.Dexterity }, 2) }, 
                "If one attack hits, the damage is 1d6+{0} physical. If both attacks hit, the damage is 2d6+{0} physical and the target is blinded until the start of your next turn.",
                null));
        }
    }
}
