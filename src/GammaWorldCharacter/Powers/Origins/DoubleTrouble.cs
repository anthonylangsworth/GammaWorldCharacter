using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The <see cref="Doppelganger"/> novice power Double Trouble (p38).
    /// </summary>
    public class DoubleTrouble: UtilityPower
    {
        /// <summary>
        /// Create a <see cref="DoubleTrouble"/>.
        /// </summary>
        public DoubleTrouble()
            : base("Double Trouble", typeof(Doppelganger), 1)
        {
            SetDescription("You create a duplicate of yourself for a short time.");
            SetAttackTypeAndRange(Range.Personal(Name));
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Dark, DamageTypes.Physical,
                EffectTypes.None, ActionType.Standard, null);
            SetEffect("You create a duplicate of yourself in an unnoccupied square within 5 squares of you. "
                + "The duplicate acts in the initiative order directly after you and can take all actions that you can take, except that it can't use doppelganger powers, Alpha Mutation or Omega Tech. "
                + "Its statistics are the same as yours, except that it only has 1 hit point. Your duplicate disappears when it drops to 0 hit points or at the end of your next turn." );
        }
    }
}
