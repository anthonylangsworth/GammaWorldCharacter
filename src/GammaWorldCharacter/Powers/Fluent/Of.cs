using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Used in conjunction with <see cref="Where"/>, this specifies where 
    /// <see cref="Where.WithinSquares"/> and other methods are relative to.
    /// </summary>
    public enum Of
    {
        /// <summary>
        /// Relative to the target of the power.
        /// </summary>
        Target,
        /// <summary>
        /// Relative to the originator of the power.
        /// </summary>
        You
    }
}
