using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// The part of an effect that damages, buffs, debuffs, heals, applies a condition or
    /// otherwise effects the target(s) of the power.
    /// </summary>
    public abstract class EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="EffectComponent"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        protected EffectComponent(Target target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.Expression = target.Expression;
            this.Target = target;
        }

        /// <summary>
        /// The effect expression this is part of or null if it is not part
        /// of an expression.
        /// </summary>
        public EffectExpression Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// The target this effect component acts on.
        /// </summary>
        public Target Target
        {
            get;
            private set;
        }

        /// <summary>
        /// Target a creature.
        /// </summary>
        public Target Creature
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Target the power originator.
        /// </summary>
        public Target You
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Target a friendly creature that is not the power originator.
        /// </summary>
        public Target Ally
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Target either the power originator or a friendly creature.
        /// </summary>
        public Target YouOrAlly
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Target an unfriendly creature.
        /// </summary>
        public Target Enemy
        {
            get
            {
                return null;
            }
        }
    }
}
