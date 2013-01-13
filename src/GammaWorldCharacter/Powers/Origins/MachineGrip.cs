using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The Machine Grip novice <see cref="Android"/> power.
    /// </summary>
    public class MachineGrip: AttackPower
    {
        /// <summary>
        /// Create a new <see cref="MachineGrip"/>.
        /// </summary>
        public MachineGrip()
            : base("Machine Grip")
        {
            SetDescription("When you get a hand on an enemy, your grip tightens like a steel-jawed vice.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Dark, DamageTypes.Physical, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(Range.Melee(Name, 1));
            AddAttack(new AttackDetails("One creature",
                new AbilityPlusLevelBonus("Machine Grip attack bonus", new ScoreType[] { ScoreType.Intelligence }, 1),
                new PowerDamage("Machine Grip damage", 1.D10()),
                new AbilityPlusLevelBonus("Machine Grip damage bonus", new ScoreType[] { ScoreType.Intelligence }, 2),
                ScoreType.Reflex, new ModifierSource[] { },
                "physical damage and the target is immobilized until the start of your next turn. If you move to a square that isn't adjacent to the target, the immobilization ends.", null));
        }
    }
}
