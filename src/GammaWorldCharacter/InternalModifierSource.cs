using System;
using System.Collections.Generic;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Modifier sources that come from the character itself.
    /// </summary>
    internal class InternalModifierSource : ModifierSource, ITraitSource, IPowerSource
    {
        private List<Power> powers;
        private List<Trait> traits;

        /// <summary>
        /// Create a new <see cref="InternalModifierSource"/>.
        /// </summary>
        /// <param name="trainedSkill">
        /// The skill the character is trained in.
        /// </param>
        /// <param name="primaryPowerSource">
        /// The primary <see cref="Origin"/>'s power source.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name=" trainedSkill"/> is not a skill.
        /// </exception>
        public InternalModifierSource(ScoreType trainedSkill, PowerSource primaryPowerSource)
            : base("Character", "Character")
        {
            if (!ScoreTypeHelper.IsSkill(trainedSkill))
            {
                throw new ArgumentException("Not a skill", "trainedSkill");
            }

            TrainedSkill = trainedSkill;

            traits = new List<Trait>();
            traits.Add(new Trait("Primary Origin Power Source", 
                string.Format("+2 bonus to overcharge {0} Alpha powers", primaryPowerSource.ToString())));

            powers = new List<Power>();
            powers.Add(new BasicAttack());
            powers.Add(new SecondWind());
        }

        /// <summary>
        /// The skill the character is trained in.
        /// </summary>
        public ScoreType TrainedSkill
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="Trait"/>s provided.
        /// </summary>
        public IEnumerable<Trait> Traits
        {
            get 
            {
                return traits.AsReadOnly(); 
            }
        }

        /// <summary>
        /// The <see cref="Power"/>s provided.
        /// </summary>
        public IEnumerable<Power> Powers
        {
            get 
            { 
                return powers.AsReadOnly(); 
            }
        }

        /// <summary>
        /// Set the level score.
        /// </summary>
        /// <param name="stage">
        /// The stage at which this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The character to add modifiers for.
        /// </param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);

            // Set the level
            addModifier(new Modifier(this, character[ScoreType.Level], 
                character.Level));

            // Set the trained skill
            addModifier(new Modifier(this, character[TrainedSkill], 4));
        }
    }
}
