using System;
using System.Runtime.Serialization;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter
{
    /// <summary>
    /// A prerequisite is missing from a <see cref="Power"/> or similar.
    /// </summary>
    /// <seealso cref="ModifierSource"/>.
    public class UnmetPrerequisiteException : Exception
    {
        private ModifierSource modifierSource;
        private string requirement;

        // Keys used for serialization
        private const string requirementKey = "requirement";
        private const string modifierSourceKey = "modifierSource";

        /// <summary>
        /// Create a <see cref="UnmetPrerequisiteException"/>.
        /// </summary>
        /// <remarks>
        /// This is provided solely for serialization.
        /// </remarks>
        protected UnmetPrerequisiteException() 
        { 
            // Do nothing
        }

        /// <summary>
        /// Create a <see cref="UnmetPrerequisiteException"/>.
        /// </summary>
        /// <param name="requirement">
        /// The requirement that was not satisfied, e.g. "level 11+", "Charisma 15+", "Two-Weapon Fighting feat", "Fighter class", etc.
        /// This should identify the requirement, e.g. "Prime Shot", and what it is, e.g. "class feature".
        /// </param>
        /// <param name="modifierSource">
        /// The <see cref="ModifierSource"/> whose prerequisites were not met.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public UnmetPrerequisiteException(string requirement, ModifierSource modifierSource) 
            : base(string.Empty) 
        {
            if (string.IsNullOrEmpty(requirement))
            {
                throw new ArgumentNullException("requirement");
            }
            if (modifierSource == null)
            {
                throw new ArgumentNullException("source");
            }

            this.modifierSource = modifierSource;
            this.requirement = requirement;
        }

        /// <summary>
        /// The exception message.
        /// </summary>
        public override string  Message
        {
            get 
            {
                return string.Format("{0} '{1}' requires '{2}'", modifierSource.GetType().Name, 
                    modifierSource.Name, requirement);
            }
        }

        /// <summary>
        /// The <see cref="ModifierSource"/> whose prerequisites were not met.
        /// </summary>
        public ModifierSource ModifierSource
        {
            get
            {
                return modifierSource;
            }
        }

        /// <summary>
        /// The requirement that was not met.
        /// </summary>
        public string Requirement
        {
            get
            {
                return requirement;
            }
        }

        /// <summary>
        /// Get the exception details for serialization.
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> to store details in about the exception.
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> containing metadata about the serialization.
        /// </param>
        public override void  GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(requirementKey, requirement);
            info.AddValue(modifierSourceKey, modifierSource);
        }

        /// <summary>
        /// Create a <see cref="UnmetPrerequisiteException"/>.
        /// </summary>
        /// <param name="info">
        /// <see cref="SerializationInfo"/> to load details about the exception from.
        /// </param>
        /// <param name="context">
        /// Metadata about the serialization.
        /// </param>
        protected UnmetPrerequisiteException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        {
            requirement = info.GetString(requirementKey);
            modifierSource = (ModifierSource) info.GetValue(modifierSourceKey, typeof(ModifierSource));
        }
    }
}
