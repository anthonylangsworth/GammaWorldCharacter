using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter.Test.Unit.Powers
{
    /// <summary>
    /// A test, dummy <see cref="Power"/>.
    /// </summary>
    public class NullPower: Power
    {
        /// <summary>
        /// Create a new <see cref="NullPower"/>.
        /// </summary>
        public NullPower()
            : base("Null power")
        {
            SetAttackTypeAndRange(Range.Personal(Name));
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.None, DamageTypes.None, 
                EffectTypes.None, ActionType.Free, null);
        }
    }
}
