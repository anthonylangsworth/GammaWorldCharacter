using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Use a constant with <see cref="ICharacterScoreValue"/>.
    /// </summary>
    public class ConstantValue: ICharacterScoreValue
    {
        /// <summary>
        /// Create a new <see cref="ConstantValue"/>.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public ConstantValue(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Return the constant <see cref="Value"/>.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/>. Ignored.
        /// </param>
        /// <returns>
        /// The value of <see cref="Value"/>.
        /// </returns>
        public int GetValue(Character character)
        {
            return Value;
        }

        /// <summary>
        /// The constant.
        /// </summary>
        public int Value
        {
            get;
            private set;
        }
    }
}
