using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Helper methods for <see cref="ModifierSource"/>s.
    /// </summary>
    public static class ModifierSourceHelper
    {
        /// <summary>
        /// Convert the <see cref="List{T}">List{ModifierSource}</see> to an array for use in string.Format
        /// </summary>
        public static readonly Converter<ModifierSource, object> Converter
            = x => x is Score ? ((Score)x).Total.ToString() : x.ToString();
    }
}
