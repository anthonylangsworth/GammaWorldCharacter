using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// An action.
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// This power either does not use an action or is part of another action.
        /// </summary>
        None,
        /// <summary>
        /// Can be used during the Character's turn without consuming a 
        /// minor, move or standard action.
        /// </summary>
        Free,
        /// <summary>
        /// Can be used outside the Character's turn after another Character's action.
        /// </summary>
        ImmediateReaction,
        /// <summary>
        /// Can be used outside the Character's turn and prevent or stop another Character's action.
        /// </summary>
        ImmediateInterrupt,
        /// <summary>
        /// A minor action.
        /// </summary>
        Minor,
        /// <summary>
        /// A move action.
        /// </summary>
        Move,
        /// <summary>
        /// A standard action.
        /// </summary>
        Standard
    }
}
