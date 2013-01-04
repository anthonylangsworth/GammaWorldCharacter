using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects
{
    /// <summary>
    /// Part of a textual effect description.
    /// </summary>
    public class EffectSpan: IEquatable<EffectSpan>
    {
        /// <summary>
        /// Create a new <see cref="EffectSpan"/>.
        /// </summary>
        /// <param name="text">
        /// The text to display. This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="effectSpanType">
        /// The <see cref="EffectSpanType"/> of the text shown. By default, 
        /// it is EffectSpanType.None.
        /// </param>
        public EffectSpan(string text, EffectSpanType effectSpanType = EffectSpanType.None)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException("text");
            }

            this.Text = text;
            this.Type = effectSpanType;
        }

        /// <summary>
        /// The span's description or text.
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// What the text describes.
        /// </summary>
        public EffectSpanType Type
        {
            get;
            private set;
        }

        /// <summary>
        /// Are objects equal?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is EffectSpan)
            {
                return Equals((EffectSpan) obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Are objects equal?
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EffectSpan other)
        {
            if (other != null)
            {
                return Type == other.Type
                       && Text == other.Text;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A hash code for this object.
        /// </summary>
        /// <returns>
        /// A hash code.
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = 17;
            unchecked // Let it wrap
            {
                hashCode = hashCode * 23 + this.Type.GetHashCode();
                hashCode = hashCode * 23 + this.Text.GetHashCode();
            }
            return hashCode;
        }

        /// <summary>
        /// A human readable representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Type + ": " + Text;
        }
    }
}
