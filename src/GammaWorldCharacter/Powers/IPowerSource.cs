using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// An object that contibutes powers to the character.
    /// </summary>
    public interface IPowerSource
    {
        /// <summary>
        /// Powers.
        /// </summary>
        IEnumerable<Power> Powers
        {
            get;
        }
    }
}
