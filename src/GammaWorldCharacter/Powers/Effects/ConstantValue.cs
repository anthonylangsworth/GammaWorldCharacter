using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Use a constant with <see cref="ICharacterScoreValue"/>.
    /// </summary>
    public class ConstantValue: ICharacterScoreValue, IEquatable<ConstantValue>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ConstantValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConstantValue) obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value;
        }
    }
}
