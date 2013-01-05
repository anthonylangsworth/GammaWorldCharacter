using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// The power originator can use a <see cref="Power"/>.
    /// </summary>
    /// <remarks>
    /// Preferably, this would have been templated, i.e. UseEffectPower&lt;MyPower&gt;. 
    /// However, the <see cref="EffectParser"/> parses based on type and templating 
    /// it would have meant creating a new parser component for every possible power.
    /// </remarks>
    public class UsePowerEffect : EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="UsePowerEffect"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="powerType">
        /// The <see cref="Type"/> of <see cref="Power"/> that can be used.
        /// </param>
        /// <param name="actionType"></param>
        public UsePowerEffect(Target target, Type powerType, ActionType actionType)
            : base(target)
        {
            if (powerType == null)
            {
                throw new ArgumentNullException("powerType");
            }
            if (!typeof(Power).IsAssignableFrom(powerType))
            {
                throw new ArgumentException("Not a power", "powerType");
            }
            if (powerType.GetConstructor(new Type[0]) == null)
            {
                throw new ArgumentException("No parameterless constructor", "powerType");
            }

            this.ActionType = actionType;
            this.PowerName = ((Power) powerType.GetConstructor(new Type[0]).Invoke(new object[0])).Name;
        }

        /// <summary>
        /// The <see cref="ActionType"/> the power can be used as, usually
        /// Free.
        /// </summary>
        public ActionType ActionType
        {
            get;
            private set;
        }

        /// <summary>
        /// The name of the power used.
        /// </summary>
        public string PowerName
        {
            get;
            private set;
        }
    }
}
