using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Felinoid"/> utility power <see cref="Pounce"/>.
    /// </summary>
    public class Pounce: UtilityPower
    {
        /// <summary>
        /// Create a new <see cref="Pounce"/>.
        /// </summary>
        public Pounce()
            : base("Pounce")
        {
            SetDescription("Your springy muscles let you leap a long distance.");
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.Bio, DamageTypes.None, EffectTypes.None, ActionType.Move, null);
            SetAttackTypeAndRange(Range.Personal(Name));
            SetEffect("You jump {0} squares, either vertically or horizontally.", 
                new ScoreBonus("Pounce squares", ScoreType.Speed));
        }
    }
}
