using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// The power originator can use a <see cref="Power"/>.
    /// </summary>
    /// <typeparam name="TPower">
    /// The <see cref="Power"/> that can be used.
    /// </typeparam>
    public class UsePowerEffect<TPower>: EffectComponent
        where TPower: Power, new()
    {
        /// <summary>
        /// Create a new <see cref="UsePowerEffect{T}"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="actionType"></param>
        public UsePowerEffect(Target target, ActionType actionType)
            : base(target)
        {
            this.ActionType = actionType;
            this.PowerName = new TPower().Name;
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
