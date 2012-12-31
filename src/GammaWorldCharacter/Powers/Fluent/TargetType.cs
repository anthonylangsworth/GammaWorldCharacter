using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// The entity the <see cref="EffectComponent"/> targets.
    /// </summary>
    public enum TargetType
    {
        /// <summary>
        /// Any creature.
        /// </summary>
        Creature,
        /// <summary>
        /// The power originator.
        /// </summary>
        You,
        /// <summary>
        /// A friendly target that is not the power originator.
        /// </summary>
        Ally,
        /// <summary>
        /// A friendly target including the power originator.
        /// </summary>
        YouOrAlly,
        /// <summary>
        /// An unfriendly target.
        /// </summary>
        Enemy,
        /// <summary>
        /// The same target as a previous <see cref="EffectComponent"/>.
        /// </summary>
        SameTarget
    }
}
