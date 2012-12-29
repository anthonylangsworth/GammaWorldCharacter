using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// An attack power.
    /// </summary>
    public abstract class AttackPower: Power
    {
        private List<AttackDetails> attacks;
        private Score conditionals;

        /// <summary>
        /// Create an <see cref="AttackPower"/>.
        /// </summary>
        /// <param name="name">
        /// The power's name.
        /// </param>
        /// <param name="requiredOrigin">
        /// The class the character must have this origin or null,
        /// if this power does not require a class.
        /// </param>
        /// <param name="requiredLevel">
        /// The minimum character level for this power.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Niether name nor targetText can be null.
        /// </exception>
        protected AttackPower(string name, Type requiredOrigin, int requiredLevel)
            : base(name, requiredOrigin, requiredLevel)
        {
            this.attacks = new List<AttackDetails>();
            conditionals = new Score(string.Format("{0} conditionals", name), string.Format("{0} conditionals", name));
            AddModifierSource(conditionals);
        }

        /// <summary>
        /// This <see cref="Score"/> can be used for conditional attack or damage 
        /// modifiers for AttackPowers that have multiple attacks.
        /// </summary>
        public Score Conditionals
        {
            get
            {
                return conditionals;
            }
        }

        /// <summary>
        /// Check whether at least one attack has been added.
        /// </summary>
        /// <param name="addDependency">
        /// The list of depdencies.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add dependencies for.
        /// </param>
        protected override void AddDependencies(Action<ModifierSource, ModifierSource> addDependency,
            Character character)
        {
            base.AddDependencies(addDependency, character);

            // Some attack powers have no attacks, e.g. "Follow-up Attack". Therefore, do not check.
        }

        /// <summary>
        /// Primary attack(s).
        /// </summary>
        public IList<AttackDetails> Attacks
        {
            get
            {
                return attacks.AsReadOnly();
            }
        }

        /// <summary>
        /// Add a new attack. This also adds the power to ModifierSources.
        /// </summary>
        /// <param name="attack">
        /// The <see cref="AttackDetails"/> of the attack to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// primaryAttack cannot be null.
        /// </exception>
        protected void AddAttack(AttackDetails attack)
        {
            if (attack == null)
            {
                throw new ArgumentNullException("attack");
            }

            attacks.Add(attack);

            // Add the attack to the power's list of scores
            AddModifierSources(attack.ModifierSources);
        }

        /// <summary>
        /// Clear all primary attacks.
        /// </summary>
        protected void ClearAttacks()
        {
            attacks.Clear();
        }
    }
}
