using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// A character origin.
    /// </summary>
    public abstract class Origin : ModifierSource, IPowerSource, ITraitSource
    {
        private List<Trait> traits;
        private List<Power> powers;

        /// <summary>
        /// Create an <see cref="Origin"/>.
        /// </summary>
        /// <param name="name">
        /// The origin name.
        /// </param>
        /// <param name="abilityScore">
        /// The ability score that starts at 18 (if primary) or 16 (if secondary).
        /// </param>
        /// <param name="powerSource">
        /// The <see cref="PowerSource"/> for Alpha mutations the character gets a 
        /// +2 bonus with to overcharge.
        /// </param>
        /// <param name="criticalHitBenefit">
        /// The description of the benefit gained at level 2 or 6 when a critical hit
        /// occurs. 
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="abilityScore"/> cannot be null.
        /// </exception>
        public Origin(string name, ScoreType abilityScore, PowerSource powerSource, string criticalHitBenefit)
            : base(name, name)
        {
            if (!ScoreTypeHelper.IsAbilityScore(abilityScore))
            {
                throw new ArgumentException("Not an ability score", "abilityScore");
            }

            traits = new List<Trait>();
            powers = new List<Power>();
            AbilityScore = abilityScore;
            PowerSource = powerSource;
            CriticalHitBenefit = criticalHitBenefit;
        }

        /// <summary>
        /// The origin's ability score.
        /// </summary>
        public ScoreType AbilityScore
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="Power"/>s supplied by this <see cref="Origin"/>.
        /// </summary>
        public IEnumerable<Power> Powers
        {
            get
            {
                return powers.AsReadOnly();
            }
        }

        /// <summary>
        /// The <see cref="PowerSource"/> the origin has an affinity for.
        /// </summary>
        public PowerSource PowerSource
        {
            get;
            private set;
        }

        /// <summary>
        /// Traits.
        /// </summary>
        public IEnumerable<Trait> Traits
        {
            get
            {
                return traits.AsReadOnly();
            }
        }

        /// <summary>
        /// The description of the benefit gained at level 2 or 6 when a critical hit
        /// occurs. 
        /// </summary>
        public string CriticalHitBenefit
        {
            get; 
            private set;
        }

        /// <summary>
        /// Add a <see cref="Power"/>.
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        protected void AddPower(Power power)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }

            powers.Add(power);
        }

        /// <summary>
        /// Add a <see cref="Trait"/>.
        /// </summary>
        /// <param name="trait">
        /// The <see cref="Trait"/> to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        protected void AddTrait(Trait trait)
        {
            if (trait == null)
            {
                throw new ArgumentNullException("trait");
            }

            traits.Add(trait);
        }

    }
}
