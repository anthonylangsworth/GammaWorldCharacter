using System;
using System.Collections.Generic;

namespace GammaWorldCharacter
{
    /// <summary>
    /// This is passed to <see cref="ModifierSource.AddModifiers"/> to inform the ModifierSource
    /// why it is being called.
    /// </summary>
    public enum CharacterUpdateStage
    {
        /// <summary>
        /// Determining dependency relationships between <see cref="ModifierSource"/>s.
        /// </summary>
        DependencyMapping,
        /// <summary>
        /// The character's are being updated.
        /// </summary>
        UpdatingScores
    }
}
