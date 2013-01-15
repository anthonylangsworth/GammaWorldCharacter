using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Electrokinetic"/> utility power "Stand Clear!".
    /// </summary>
    public class StandClear: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="StandClear"/>.
        /// </summary>
        public StandClear()
            : base("Stand Clear!")
        {
            SetDescription("You jold an ally out of whatever funk he's in");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Dark, DamageTypes.Electricity, EffectTypes.Healing, ActionType.Standard, null);
            SetAttackTypeAndRange(Range.Melee(Name, 1));
            SetTarget("One ally.");
            SetEffect("Either the target regains {0} hit points or makes a saving through with a +{1} bonus",
                new AbilityPlusLevelBonus("Stand Clear hit points", new []{ ScoreType.Wisdom }, 1),
                new AbilityBonus("Stand clear saving throw", new []{ ScoreType.Wisdom} ));
        }
    }
}
