using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter.Origins
{
    /// <summary>
    /// A character origin.
    /// </summary>
    public abstract class Origin : ModifierSource, IPowerSource, ITraitSource, IEquatable<Origin>
    {
        private readonly List<Trait> traits;
        private readonly List<Power> powers;

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
        /// The benefit gained at level 2 or 6 when a critical hit occurs. 
        /// </param>
        /// <exception cref="ArgumentException">
        /// Neither <paramref name="abilityScore"/> nor <paramref name="criticalHitBenefit"/> can be null.
        /// </exception>
        protected Origin(string name, ScoreType abilityScore, PowerSource powerSource, EffectExpression criticalHitBenefit)
            : base(name, name)
        {
            if (!ScoreTypeHelper.IsAbilityScore(abilityScore))
            {
                throw new ArgumentException("Not an ability score", "abilityScore");
            }
            if (criticalHitBenefit == null)
            {
                throw new ArgumentNullException("criticalHitBenefit");
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
        /// Are two <see cref="Origin"/>s equal?
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Origin other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other)
                   && AbilityScore == other.AbilityScore
                   && PowerSource == other.PowerSource;
        }

        /// <summary>
        /// Object equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Origin) obj);
        }

        /// <summary>
        /// Hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (int) AbilityScore;
                hashCode = (hashCode*397) ^ (int) PowerSource;
                hashCode = (hashCode*397) ^ (CriticalHitBenefit != null ? CriticalHitBenefit.GetHashCode() : 0);
                return hashCode;
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
        public EffectExpression CriticalHitBenefit
        {
            get; 
            private set;
        }

        /// <summary>
        /// The novice <see cref="Power"/>.
        /// </summary>
        public Power NovicePower
        {
            get;
            protected set;
        }

        /// <summary>
        /// Ther utility <see cref="Power"/>.
        /// </summary>
        public Power UtilityPower
        {
            get; 
            protected set;
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

        /// <summary>
        /// Check that <see cref="NovicePower"/> has been set.
        /// </summary>
        /// <remarks>
        /// This implementation calls AddModifiers and creates a dependency for each modifier added during 
        /// the DependencyMappting stage.
        /// <para/>
        /// This needs to be overridden if a requirements check or bonus involves a Score or ModifierSource
        /// not referenced in AddModifiers.
        /// </remarks>
        /// <param name="addDependency">
        /// Add by calling this method.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add dependencies for.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Neither dependencies nor Character can be null.
        /// </exception>
        protected override void AddDependencies(Action<ModifierSource, ModifierSource> addDependency, Character character)
        {
            base.AddDependencies(addDependency, character);

            if (NovicePower == null)
            {
                throw new InvalidOperationException(string.Format("Novice power not specified for origin {0}.", Name));
            }
            //if (UtilityPower == null)
            //{
            //    throw new InvalidOperationException(string.Format("Utility power not specified for origin {0}.", Name));
            //}
        }

        /// <summary>
        /// The <see cref="Power"/>s provided by having this origin, specifically the novice power only
        /// </summary>
        /// <seealso cref="NovicePower"/>
        public IEnumerable<Power> Powers
        {
            get
            {
                yield return NovicePower;
            }
        }
    }
}
