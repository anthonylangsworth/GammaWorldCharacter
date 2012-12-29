using System;

namespace GammaWorldCharacter
{
    /// <summary>
    /// The source of a class's features and powers.
    /// </summary>
    public enum PowerSource
    {
        /// <summary>
        /// Only valid for certain powers.
        /// </summary>
        None,
        /// <summary>
        /// Bio.
        /// </summary>
        Bio,
        /// <summary>
        /// Dark.
        /// </summary>
        Dark,
        /// <summary>
        /// Psi.
        /// </summary>
        Psi,
        /// <summary>
        /// Area 52
        /// </summary>
        Area52,
        /// <summary>
        /// Ishtar
        /// </summary>
        Ishtar,
        /// <summary>
        /// Xi
        /// </summary>
        Xi,
        /// <summary>
        /// Not a cannon power source, this differentiates powers from items rather than the character so 
        /// players know which powers are limited by the character's item daily power use.
        /// </summary>
        Item
    }
}
