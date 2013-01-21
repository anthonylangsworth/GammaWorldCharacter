using System;
using System.Collections.Generic;
using System.Diagnostics;
using GammaWorldCharacter.Scores;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Something that can add <see cref="Modifier"/>s to <see cref="Score"/>s.
    /// Effectively, this is the base class for most classes.
    /// </summary>
    /// <remarks>
    /// A ModifierSource has "sub" ModifierSources, usually used to construct
    /// the description or effect other ModifierSources, such as a power's
    /// attack and damage bonuses. These are also used by other modifier sources
    /// such as some feats, class features or paragon path features.
    /// </remarks>
    public class ModifierSource : IEquatable<ModifierSource>
    {
        private string description;
        private List<ModifierSource> descriptionScores;
        private List<Modifier> modifiers;
        private List<ModifierSource> modifierSources;

        /// <summary>
        /// Create a new <see cref="ModifierSource"/>.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="abbreviation">
        /// The abbreviated name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 'name' is empty or null.
        /// </exception>
        protected ModifierSource(string name, string abbreviation)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (string.IsNullOrEmpty(abbreviation))
            {
                throw new ArgumentNullException("abbreviation");
            }

            this.Abbreviation = abbreviation;
            this.Name = name;
            this.modifiers = new List<Modifier>();
            this.modifierSources = new List<ModifierSource>();
        }

        /// <summary>
        /// The abbreviated name of the score.
        /// </summary>
        public string Abbreviation 
        { 
            get;
            private set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// This has no flavor text and is fully covered by existing mechanics.
        /// </exception>
        /// <seealso cref="HasDescription"/>
        public string Description
        {
            get
            {
                if (!HasDescription)
                {
                    throw new InvalidOperationException("Modifier source has no description");
                }

                return string.Format(description, descriptionScores.ConvertAll<object>(ModifierSourceHelper.Converter).ToArray());
            }
        }

        /// <summary>
        /// Does this feature have additional information that cannot be modelled
        /// by the library and so should be included on a character sheet, for 
        /// example.
        /// </summary>
        /// <seealso cref="Description"/>
        public bool HasDescription
        {
            get
            {
                return !string.IsNullOrEmpty(description);
            }
        }

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Modifiers this source creates.
        /// </summary>
        public IList<Modifier> Modifiers
        {
            get
            {
                return modifiers.AsReadOnly();
            }
        }

        /// <summary>
        /// The <see cref="ModifierSource"/>s used by this ModifierSource, such as 
        /// attack and damage bonuses for attack powers.
        /// </summary>
        public IList<ModifierSource> ModifierSources
        {
            get
            {
                return modifierSources.AsReadOnly();
            }
        }

        /// <summary>
        /// Are these the same modifier source?
        /// </summary>
        /// <param name="other">
        /// The <see cref="ModifierSource"/> to check.
        /// </param>
        /// <returns>
        /// True if the objects are equal, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// other cannot be null.
        /// </exception>
        public override bool Equals(object other)
        {
            if (other is ModifierSource)
            {
                return Equals((ModifierSource)other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Are these the same modifier source (by name and abbreviation).
        /// </summary>
        /// <param name="other">
        /// The <see cref="ModifierSource"/> to check.
        /// </param>
        /// <returns>
        /// True if the objects are equal, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// other cannot be null.
        /// </exception>
        public bool Equals(ModifierSource other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            return Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)
                && Abbreviation.Equals(other.Abbreviation, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get the <see cref="ModifierSource"/> to <see cref="ModifierSource"/> dependencies.
        /// </summary>
        /// <remarks>
        /// The base implementation calls AddModifiers, passing in CharacterUpdateStage.DependencyMapping,
        /// and uses the returned modifiers to construct the dependencies.
        /// <para/>
        /// ModifierSources requiring more complex implements should override AddDepdendencies. There is no 
        /// requirement to call AddModifier, just a convenience.
        /// </remarks>
        /// <param name="character">
        /// The character to generate scores for. Overriding methods should must not modify the passed in character.
        /// </param>
        /// <returns>
        /// A list of <see cref="ModifierSource"/> to ModifierSource dependencies.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// character cannot be null.
        /// </exception>
        public List<KeyValuePair<ModifierSource, ModifierSource>> GetDependencies(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            List<KeyValuePair<ModifierSource, ModifierSource>> result;

            result = new List<KeyValuePair<ModifierSource,ModifierSource>>();

            AddDependencies((x, y) => result.Add(new KeyValuePair<ModifierSource, ModifierSource>(x, y)), 
                character);

            return result;
        }

        /// <summary>
        /// Provide an appropriate hash code.
        /// </summary>
        /// <returns>
        /// A hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + Abbreviation + "|" + Name).GetHashCode();
        }

        /// <summary>
        /// Update the list of modifiers.
        /// </summary>
        /// <param name="character">
        /// The character to update the modifier for.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// character cannot be null.
        /// </exception>
        internal void Update(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("Character");
            }

            modifiers.Clear();
            AddModifiers(CharacterUpdateStage.UpdatingScores, x => modifiers.Add(x), character);
        }

        /// <summary>
        /// Provide human readable description of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Add dependencies.
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
        /// <see cref="AddModifiers"/>
        protected virtual void AddDependencies(Action<ModifierSource, ModifierSource> addDependency,
            Character character)
        {
            if (addDependency == null)
            {
                throw new ArgumentNullException("dependencies");
            }
            if (character == null)
            {
                throw new ArgumentNullException("Character");
            }

            List<Modifier> modifiers;

            // Add dependencies for each ModifierSource
            foreach (ModifierSource modifierSource in ModifierSources)
            {
                addDependency(modifierSource, this);
            }

            modifiers = new List<Modifier>();
            AddModifiers(CharacterUpdateStage.DependencyMapping, x => modifiers.Add(x), character);
            modifiers.ForEach(modifier => addDependency(modifier.Source, modifier.ModifiedScore));
        }

        /// <summary>
        /// Override this method to add modifiers.
        /// </summary>
        /// <remarks>
        /// This normally called twice when a character is updated. The first time 'stage' will be
        /// CharacterUpdateStage.DependencyMapping and the result used to construct a graph of 
        /// dependencies between ModifierSources. The second timem, when 'stage' will be 
        /// CharacterUpdateStage.UpdatingScores and the modifiers returned will be added to the
        /// specified ModifierSources.
        /// </remarks>
        /// <param name="stage">
        /// The stage during character update this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The character to add modifiers for. This should not be modified directly.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Neither modifiers nor character can be null.
        /// </exception>
        /// <see cref="AddDependencies"/>
        protected virtual void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            if (addModifier == null)
            {
                throw new ArgumentNullException("addModifier");
            }
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            // Do nothing
        }

        /// <summary>
        /// Add a <see cref="ModifierSource"/> for this power.
        /// </summary>
        /// <param name="modifierSource">
        /// The <see cref="ModifierSource"/> to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// modifierSource cannot be null.
        /// </exception>
        protected void AddModifierSource(ModifierSource modifierSource)
        {
            if (modifierSource == null)
            {
                throw new ArgumentNullException("score");
            }

            modifierSources.Add(modifierSource);
        }

        /// <summary>
        /// Add <see cref="ModifierSource"/>s this power.
        /// </summary>
        /// <param name="modifierSources">
        /// The <see cref="ModifierSource"/>s to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Neither modifierSources nor any modifier Source in it can be null.
        /// </exception>
        protected void AddModifierSources(IList<ModifierSource> modifierSources)
        {
            if (modifierSources == null)
            {
                throw new ArgumentNullException("modifierSources");
            }
            if (modifierSources.Contains(null))
            {
                throw new ArgumentNullException("modifierSources");
            }

            this.modifierSources.AddRange(modifierSources);
        }

        /// <summary>
        /// Set the description.
        /// </summary>
        /// <param name="description">
        /// The new description. Pass null or empty to remove the description.
        /// </param>
        /// <param name="scores">
        /// Optional <see cref="Score"/>s to substitute into the description.
        /// </param>
        protected void SetDescription(string description, params Score[] scores)
        {
            this.description = description;
            descriptionScores = new List<ModifierSource>();
            descriptionScores.AddRange(scores);

            // Yes, this causes a slight leak. 
            AddModifierSources(new List<ModifierSource>(scores));
        }
    }
}
