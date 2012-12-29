using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// Details of an attack included in an <see cref="AttackPower"/>.
    /// </summary>
    public class AttackDetails
    {
        private List<ModifierSource> additionalScores;
        private string additionalText;
        private Score attackBonus;
        private ScoreType attackedDefense;
        private PowerDamage damage;
        private Score damageBonus;
        private string missText;
        private List<ModifierSource> modifierSources;
        private string target;

        // Convert the modifierSources list to an array for use in string.Format
        private readonly Converter<ModifierSource, object> converter
            = x => x is Score ? ((Score)x).Total.ToString() : x.ToString();

        /// <summary>
        /// Create a new <see cref="AttackDetails"/>.
        /// </summary>
        /// <param name="target">
        /// What the attack affects.
        /// </param>
        /// <param name="attackBonus">
        /// The <see cref="Score"/> for the attack bonus. If the attack does not use an 
        /// attack roll, this can be null. This is added to Scores if not null.
        /// </param>
        /// <param name="damage">
        /// The power's base damage. If the attack does not deal damage, this can be null.
        /// </param>
        /// <param name="damageBonus">
        /// The <see cref="Score"/> for the damage bonus. If the attack does not deal damage,
        /// this can be null. This is added to Scores if not null.
        /// </param>
        /// <param name="attackedDefense">
        /// The defense attacked. If there is no attack roll, this value is ignored.
        /// </param>
        /// <param name="additionalScores">
        /// Additional scores used by the attack, usually for additional effects like 
        /// "pushes the target Wisdom modifier squares" or "an ally gains Charisma modifier temporary hit points".
        /// </param>
        /// <param name="additionalText">
        /// Additional details of the attack included immediately after the damage bonus. This supports "{0}"
        /// being the first score in <paramref name="additionalScores"/>, "{1}" being the second and so on.
        /// </param>
        /// <param name="missText">
        /// A description of the effect of the power on a miss or null, if the power does nothing on a miss. This 
        /// supports the same string substitution parameters as <paramref name="additionalText"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="damage"/> and <paramref name="damageBonus"/> must be supplied or both must be null.
        /// <paramref name="additionalText"/> must also be specified. <paramref name="target"/> cannot be null.
        /// </exception>
        public AttackDetails(string target, Score attackBonus, PowerDamage damage, Score damageBonus, ScoreType attackedDefense,
            IList<ModifierSource> additionalScores, string additionalText, string missText)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target");
            }
            if ((damage != null && damageBonus == null) || (damage != null && damageBonus == null))
            {
                throw new ArgumentNullException("damage", 
                    "Either damage and damageBonus must be supplied or both must be null.");
            }
            if (additionalScores == null)
            {
                throw new ArgumentNullException("additionalScores");
            }
            if (additionalScores.Contains(null))
            {
                throw new ArgumentNullException("additionalScores", "One or more elements are null");
            }
            if (string.IsNullOrEmpty(additionalText))
            {
                throw new ArgumentNullException("additionalText");
            }

            this.attackBonus = attackBonus;
            this.attackedDefense = attackedDefense;
            this.damage = damage;
            this.damageBonus = damageBonus;
            this.additionalText = additionalText;
            this.missText = missText;
            this.modifierSources = new List<ModifierSource>();
            this.target = target;

            if (attackBonus != null)
            {
                AddModifierSource(attackBonus);
            }
            if (damage != null)
            {
                AddModifierSource(damage);
            }
            if (damageBonus != null)
            {
                AddModifierSource(damageBonus);
            }
            this.additionalScores = new List<ModifierSource>();
            this.additionalScores.AddRange(additionalScores);
            this.modifierSources.AddRange(additionalScores);
        }

        /// <summary>
        /// Additional details of the attack included immediately after the damage bonus.
        /// </summary>
        public string AdditionalText
        {
            get
            {
                return string.Format(additionalText,
                    additionalScores.ConvertAll<object>(converter).ToArray());
            }
        }

        /// <summary>
        /// The score that modifies the attack roll.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// This attack does not use an attack roll against a defense.
        /// </exception>
        /// <see cref="HasAttackRoll"/>
        public ScoreType AttackedDefense
        {
            get
            {
                if (!HasAttackRoll)
                {
                    throw new InvalidOperationException(
                        "This attack does not use an attack roll against a defense");
                }

                return attackedDefense;
            }
        }

        /// <summary>
        /// Bonus to attack rolls. This is null if there is no attack roll.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// This attack does not use an attack roll against a defense.
        /// </exception>
        /// <see cref="HasAttackRoll"/>
        public Score AttackBonus
        {
            get
            {
                if (!HasAttackRoll)
                {
                    throw new InvalidOperationException(
                        "This attack does not use an attack roll against a defense");
                }

                return attackBonus;
            }
        }

        /// <summary>
        /// The attack's base damage. This is null if there is no damage.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// This attack does not cause damage.
        /// </exception>
        /// <see cref="HasDamage"/>
        public PowerDamage Damage
        {
            get
            {
                if (!HasDamage)
                {
                    throw new InvalidOperationException(
                        "This attack does not cause damage");
                }

                return damage;
            }
        }

        /// <summary>
        /// Bonus to attack rolls. This is null if there is no damage.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// This attack does not cause damage.
        /// </exception>
        /// <see cref="HasDamage"/>
        public Score DamageBonus
        {
            get
            {
                if (!HasDamage)
                {
                    throw new InvalidOperationException(
                        "This attack does not cause damage");
                }

                return damageBonus;
            }
        }

        /// <summary>
        /// Does this have an attack roll?
        /// </summary>
        /// <remarks>
        /// If false, AttackBonus and AttackedDefense will be undefined.
        /// </remarks>
        /// <seealso cref="AttackBonus"/>
        /// <seealso cref="AttackedDefense"/>
        public bool HasAttackRoll
        {
            get
            {
                return attackBonus != null;
            }
        }

        /// <summary>
        /// Does this cause damage?
        /// </summary>
        /// <remarks>
        /// If false, Damage and DamageBonus will be undefined.
        /// </remarks>
        /// <seealso cref="Damage"/>
        /// <seealso cref="DamageBonus"/>
        public bool HasDamage
        {
            get
            {
                return damageBonus != null;
            }
        }

        /// <summary>
        /// Does this attack have an effect on a miss?
        /// </summary>
        /// <seealso cref="MissEffect"/>
        public bool HasMissEffect
        {
            get
            {
                return !string.IsNullOrEmpty(missText);
            }
        }

        /// <summary>
        /// The effect on a miss.
        /// </summary>
        /// <seealso cref="HasMissEffect"/>
        public string MissEffect
        {
            get
            {
                if (!HasMissEffect)
                {
                    throw new InvalidOperationException("No miss effect");
                }

                return string.Format(missText,
                    additionalScores.ConvertAll<object>(converter).ToArray());
            }
        }

        /// <summary>
        /// The <see cref="ModifierSource"/>s for this attack, usually the 
        /// attack bonus, damage bonus and the damage itself.
        /// </summary>
        public IList<ModifierSource> ModifierSources
        {
            get
            {
                return modifierSources.AsReadOnly();
            }
        }

        /// <summary>
        /// Construct a human-readable representation.
        /// </summary>
        /// <returns>
        /// A human readable representation.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} vs {1} for {2}{3}",
                AttackBonus.ToString("FM"), AttackedDefense, Damage.Dice, DamageBonus.ToString("FM"),
                AdditionalText);
        }

        /// <summary>
        /// A description of what the attack targets.
        /// </summary>
        public string Target
        {
            get
            {
                return target;
            }
        }

        /// <summary>
        /// Add a <see cref="ModifierSource"/> to the list of scores for this AttackDetails.
        /// </summary>
        /// <param name="modifierSource">
        /// The <see cref="ModifierSource"/> to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// score cannot be null.
        /// </exception>
        protected void AddModifierSource(ModifierSource modifierSource)
        {
            if (modifierSource == null)
            {
                throw new ArgumentNullException("modifierSource");
            }

            modifierSources.Add(modifierSource);
        }
    }
}
