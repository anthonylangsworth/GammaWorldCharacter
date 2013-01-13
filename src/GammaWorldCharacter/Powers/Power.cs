using System;
using System.Collections.Generic;
using System.Linq;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A power.
    /// </summary>
    public abstract class Power: ModifierSource, IEquatable<Power>
    {
        private ActionType action;
        private AttackTypeAndRange attackTypeAndRange;
        private DamageTypes damageTypes;
        private string effect;
        private List<ModifierSource> effectScores;
        private EffectTypes effectTypes;
        private PowerFrequency frequency;
        private bool powerDetailsSet;
        private PowerSource powerSource;
        private string trigger;
        private SustainDetails sustainDetails;
        private bool unique;

        /// <summary>
        /// Create a new <see cref="Power"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the power.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// name cannot be null.
        /// </exception>
        protected Power(string name)
            : base(name, name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            this.powerDetailsSet = false;
        }

        /// <summary>
        /// The action required to use this power.
        /// </summary>
        public ActionType Action
        {
            get
            {
                return action;
            }
        }

        /// <summary>
        /// The attack type and range, e.g. "melee weapon" or "close burst 5".
        /// </summary>
        public AttackTypeAndRange AttackTypeAndRange
        {
            get
            {
                return attackTypeAndRange;
            }
        }

        /// <summary>
        /// Does the specified <see cref="Character"/> meet the requirements of this power?
        /// If not they cannot take the power, for example if the character already has it.
        /// </summary>
        /// <param name="character">
        /// The character to test.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// character cannot be null.
        /// </exception>
        /// <exception cref="UnmetPrerequisiteException">
        /// The character does not meet the power's requirements
        /// </exception>
        public virtual void CheckRequirements(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            if (IsUnique && character.GetPowers().Any(x => Equals(x)))
            {
                throw new UnmetPrerequisiteException("Power not already taken", this);
            }
        }

        /// <summary>
        /// The type(s) of damage this power uses.
        /// </summary>
        public DamageTypes DamageTypes
        {
            get
            {
                return damageTypes;
            }
        }

        /// <summary>
        /// The effect of the power.
        /// </summary>
        public string Effect
        {
            get
            {
                return string.Format(effect, effectScores.ConvertAll<object>(ModifierSourceHelper.Converter).ToArray());
            }
        }

        /// <summary>
        /// The type(s) of effects this power uses.
        /// </summary>
        public EffectTypes EffectTypes
        {
            get
            {
                return effectTypes;
            }
        }

        /// <summary>
        /// Are the given objects equal?
        /// </summary>
        /// <param name="other">
        /// The object to compare with this one.
        /// </param>
        /// <returns>
        /// True if the objects are equal, false otherwise.
        /// </returns>
        public override bool Equals(object other)
        {
            if (other is Power)
            {
                return Equals((Power)other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Two powers are equal if they are of the same type.
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> to compare with this one.
        /// </param>
        /// <returns>
        /// True if the objects are equal, false otherwise.
        /// </returns>
        public virtual bool Equals(Power power)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }

            return power.GetType().Equals(GetType());
        }


        /// <summary>
        /// How often the power can be used.
        /// </summary>
        public PowerFrequency Frequency
        {
            get
            {
                return frequency;
            }
        }

        /// <summary>
        /// Generate a hash code.
        /// </summary>
        /// <returns>
        /// The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return (GetType().FullName).GetHashCode();
        }

        /// <summary>
        /// Has an effect been specified?
        /// </summary>
        public bool HasEffect
        {
            get
            {
                return effect != null;
            }
        }

        /// <summary>
        /// Is the power sustainable?
        /// </summary>
        public bool HasSustainDetails
        {
            get
            {
                return sustainDetails != null;
            }
        }

        /// <summary>
        /// Does the attack power have a tigger?
        /// </summary>
        /// <seealso cref="Trigger"/>
        public bool HasTrigger
        {
            get
            {
                return action == ActionType.ImmediateInterrupt
                    || action == ActionType.ImmediateReaction
                    || action == ActionType.Free;
            }
        }

        /// <summary>
        /// True if the character can only have one instance of this power, false if the character can have multiple instances.
        /// </summary>
        public bool IsUnique
        {
            get
            {
                return unique;
            }
        }

        /// <summary>
        /// Can the specified <see cref="Character"/> use this power at this time?
        /// </summary>
        /// <remarks>
        /// For example, if a power requires a melee weapon and one is not equipped, this
        /// would return false.
        /// </remarks>
        /// <param name="character">
        /// The character to test.
        /// </param>
        /// <returns>
        /// True if the Character can use this power, false if not.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Character cannot be null.
        /// </exception>
        public virtual bool IsUsable(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("Character");
            }

            return true;
        }

        /// <summary>
        /// The power's power source.
        /// </summary>
        public PowerSource PowerSource
        {
            get
            {
                return powerSource;
            }
        }

        /// <summary>
        /// The trigger for an immediate reaction or interrupt.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The trigger is only available for free, immediate reaction or interrupt powers.
        /// </exception>
        /// <seealso cref="HasTrigger"/>
        public string Trigger
        {
            get
            {
                if (!HasTrigger)
                {
                    throw new InvalidOperationException(
                        "Trigger is only valid when the Action is either ActionType.ImmediateInterrupt, ActionType.ImmediateReaction or ActionType.Free");
                }

                return trigger;
            }
        }

        /// <summary>
        /// How to sustain the power, or null if the power cannot be sustained.
        /// </summary>
        public SustainDetails SustainDetails
        {
            get
            {
                if (!HasSustainDetails)
                {
                    throw new InvalidOperationException("No sustain details specified.");
                }

                return sustainDetails;
            }
        }

        /// <summary>
        /// Check whether the <see cref="SetPowerDetails"/> and <see cref="SetAttackTypeAndRange(AttackTypeAndRange)"/> 
        /// have been called. Also add a dummy dependency between the class and the power to ensure requirement checks occur.
        /// </summary>
        /// <param name="addDependency">
        /// Add a dependency by calling this method.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add dependencies for.
        /// </param>
        protected override void AddDependencies(Action<ModifierSource, ModifierSource> addDependency,
            Character character)
        {
            base.AddDependencies(addDependency, character);

            if (attackTypeAndRange == null)
            {
                throw new InvalidOperationException(
                    string.Format("SetAttackTypeAndRange not called for power '{0}'", Name));
            }
            if (!powerDetailsSet)
            {
                throw new InvalidOperationException(
                    string.Format("SetPowerDetails not called for power '{0}'", Name));
            }
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
            if (stage == CharacterUpdateStage.UpdatingScores)
            {
                CheckRequirements(character);
            }

            base.AddModifiers(stage, addModifier, character);
        }

        /// <summary>
        /// Set the <see cref="AttackTypeAndRange"/> for this <see cref="Power"/>.
        /// This also adds the power to ModifierSources.
        /// </summary>
        /// <param name="attackTypeAndRange">
        /// The <see cref="AttackTypeAndRange"/> to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// attackTypeAndRange cannot be null.
        /// </exception>
        protected void SetAttackTypeAndRange(AttackTypeAndRange attackTypeAndRange)
        {
            if (attackTypeAndRange == null)
            {
                throw new ArgumentNullException("attackTypeAndRange");
            }

            this.attackTypeAndRange = attackTypeAndRange;

            // Add it to the power's list of modifier sources
            AddModifierSource(attackTypeAndRange);
        }

        /// <summary>
        /// Set the power's effect.
        /// </summary>
        /// <param name="effect">
        /// A description of the power's effect. When returned from <see cref="Effect"/>,
        /// "{0}" will be substituted for the first entry in <paramref name="args"/>,
        /// "{1}" for the second entry and so on.
        /// </param>
        /// <param name="args">
        /// Scores to substitute into <paramref name="effect"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <seealso cref="Effect"/>
        protected void SetEffect(string effect, params Score[] args)
        {
            SetEffect(effect, args as IList<ModifierSource>);
        }

        /// <summary>
        /// Set the power's effect.
        /// </summary>
        /// <param name="effect">
        /// A description of the power's effect. When returned from <see cref="Effect"/>,
        /// "{0}" will be substituted for the first entry in <paramref name="scores"/>,
        /// "{1}" for the second entry and so on.
        /// </param>
        /// <param name="scores">
        /// Scores to substitute into <paramref name="effect"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Niether <paramref cref="effect"/> nor <paramref name="scores"/> can be null.
        /// </exception>
        /// <seealso cref="Effect"/>
        protected void SetEffect(string effect, IList<ModifierSource> scores)
        {
            if (effect == null)
            {
                throw new ArgumentNullException("effect");
            }
            if (scores == null)
            {
                throw new ArgumentNullException("scores");
            }
            if (scores.Contains(null))
            {
                throw new ArgumentNullException("scores", "One or more elements are null");
            }

            this.effect = effect;
            this.effectScores = new List<ModifierSource>();
            this.effectScores.AddRange(scores);

            AddModifierSources(effectScores);
        }

        /// <summary>
        /// Set the immutable properties of a power.
        /// </summary>
        /// <param name="frequency">
        /// How often the power can be used.
        /// </param>
        /// <param name="powerSource">
        /// The power source, usually the same as the character.
        /// </param>
        /// <param name="damageTypes">
        /// The types of damage the power does, if any. This is usually ignored for non-attack powers.
        /// </param>
        /// <param name="effectTypes">
        /// The types of effects this power uses, if any.
        /// </param>
        /// <param name="action">
        /// The actiopn required to use the power.
        /// </param>
        /// <param name="trigger">
        /// If <paramref name="action"/> is immediate (whether reaction or interrupt), this describes
        /// the trigger for the power. If action is not immedate, this must be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="trigger"/> must be specified when <paramref name="action"/> is
        /// ActionType.ImmediateInterrupt or ActionType.ImmediateReaction and must be null when 
        /// it is not.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <see cref="SetPowerDetails"/> has already been called.
        /// </exception>
        protected void SetPowerDetails(PowerFrequency frequency, PowerSource powerSource,
            DamageTypes damageTypes, EffectTypes effectTypes, ActionType action, string trigger)
        {
            if (powerDetailsSet)
            {
                throw new InvalidOperationException("Power details already set");
            }
            if ((action == ActionType.ImmediateInterrupt
                || action == ActionType.ImmediateReaction)
                && string.IsNullOrEmpty(trigger))
            {
                throw new ArgumentException(
                    "trigger must be specified if action is either ActionType.ImmediateInterrupt or ActionType.ImmediateReaction",
                    "trigger");
            }
            else if ((action != ActionType.ImmediateInterrupt
                && action != ActionType.ImmediateReaction
                && action != ActionType.Free)
                && !string.IsNullOrEmpty(trigger))
            {
                throw new ArgumentException(
                    "trigger must be null if action is not ActionType.ImmediateInterrupt, ActionType.ImmediateReaction or ActionType.Free",
                    "trigger");
            }

            this.action = action;
            this.damageTypes = damageTypes;
            this.effectTypes = effectTypes;
            this.frequency = frequency;
            this.powerSource = powerSource;
            this.trigger = trigger;

            powerDetailsSet = true;
        }

        /// <summary>
        /// Describe how the power can be sustained.
        /// </summary>
        /// <param name="sustainDetails">
        /// The <see cref="SustainDetails"/> describing how to sustain this power.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="sustainDetails"/> cannot be null.
        /// </exception>
        protected void SetSustainDetails(SustainDetails sustainDetails)
        {
            if (sustainDetails == null)
            {
                throw new ArgumentNullException("sustainDetails");
            }

            this.sustainDetails = sustainDetails;
        }

        /// <summary>
        /// Set whether a character can have multiple instances of this power.
        /// </summary>
        /// <param name="unique">
        /// True if the character can have multiple instances of this power, false if the character can only have one.
        /// </param>
        protected void SetUnique(bool unique)
        {
            this.unique = unique;
        }
    }
}
