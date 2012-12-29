using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A utility power.
    /// </summary>
    public class UtilityPower: Power
    {
        private string target;

        /// <summary>
        /// Create a new <see cref="UtilityPower"/>.
        /// </summary>
        /// <param name="name">
        /// The power's name.
        /// </param>
        /// <param name="requiredClass">
        /// The class the character must be a memeber of to take this power or null,
        /// if this power does not require a class.
        /// </param>
        /// <param name="requiredLevel">
        /// The minimum character level for this power.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Niether <paramref name="name"/> nor <paramref name="requiredClass"/> can be null.
        /// </exception>
        protected UtilityPower(string name, Type requiredClass, int requiredLevel)
            : base(name, requiredClass, requiredLevel)
        {
            // Do nothing
        }

        /// <summary>
        /// Does the utility power have a target?
        /// </summary>
        public bool HasTarget
        {
            get
            {
                return !string.IsNullOrEmpty(target);
            }
        }

        /// <summary>
        /// The target of the power.
        /// </summary>
        public string Target
        {
            get
            {
                if (!HasTarget)
                {
                    throw new InvalidOperationException("Power does not have a target");
                }

                return target;
            }
        }

        /// <summary>
        /// Check that Effect has been set.
        /// </summary>
        /// <param name="stage">
        /// The stage when this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add modifiers for.
        /// </param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);

            if (!HasEffect)
            {
                throw new InvalidOperationException(string.Format("Effect not set in power '{0}'. Call SetEffect().",
                    Name));
            }
        }

        /// <summary>
        /// Set the target of the power.
        /// </summary>
        /// <param name="target">
        /// The target of the power.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="target"/> cannot be null.
        /// </exception>
        protected void SetTarget(string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target");
            }

            this.target = target;
        }
    }
}
