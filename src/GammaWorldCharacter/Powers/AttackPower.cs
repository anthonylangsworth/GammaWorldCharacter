using System;
using System.Linq;
using System.Collections.Generic;
using GammaWorldCharacter.Levels;
using GammaWorldCharacter.Powers.Effects;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// An attack power.
    /// </summary>
    public abstract class AttackPower: Power
    {
        private readonly List<AttackDetails> attacks;
        private readonly List<EffectExpression> criticals;

        /// <summary>
        /// Create an <see cref="AttackPower"/>.
        /// </summary>
        /// <param name="name">
        /// The power's name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Niether name nor targetText can be null.
        /// </exception>
        protected AttackPower(string name)
            : base(name)
        {
            this.attacks = new List<AttackDetails>();
            this.criticals = new List<EffectExpression>();
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
        /// Effects that occur on a critical.
        /// </summary>
        public IList<EffectExpression> Criticals
        {
            get
            {
                return criticals.AsReadOnly();
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
        /// Ensure the character meets the minimum requirements for the power during the
        /// score updating phase.
        /// </summary>
        /// <param name="stage">
        /// The character update stage at which this is called.
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

            OriginChoice level2CriticalHitBenefitOrigin;

            criticals.Clear();
            if (character.Level >= 2)
            {
                level2CriticalHitBenefitOrigin = ((Level02) character.Levels.OfType<Level02>().First()).CriticalHitBenefitOrigin;
                if (level2CriticalHitBenefitOrigin == OriginChoice.Primary)
                {
                    criticals.Add(character.PrimaryOrigin.CriticalHitBenefit);
                }
                else
                {
                    criticals.Add(character.SecondaryOrigin.CriticalHitBenefit);
                }
            }
            else if (character.Level >= 6)
            {
                criticals.Add(character.PrimaryOrigin.CriticalHitBenefit);
                criticals.Add(character.SecondaryOrigin.CriticalHitBenefit);
            }
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
