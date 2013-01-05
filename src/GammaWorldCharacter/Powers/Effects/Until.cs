using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// When an effect will end.
    /// </summary>
    public enum Until
    {
        /// <summary>
        /// Start of the your next turn.
        /// </summary>
        StartOfYourNextTurn,
        /// <summary>
        /// End of the your next turn.
        /// </summary>
        EndOfYourNextTurn,
        /// <summary>
        /// End of the encounter.
        /// </summary>
        EndOfEncounter
    }
}
