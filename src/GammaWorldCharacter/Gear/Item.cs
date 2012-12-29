using System;
using System.Collections.Generic;

namespace GammaWorldCharacter.Gear
{
    /// <summary>
    /// An item of equipment such as a lantern, leather armor or a +1 longsword.
    /// </summary>
    public class Item: ModifierSource, IEquatable<Item>
    {
        /// <summary>
        /// Create a new <see cref="Item"/>.
        /// </summary>
        /// <param name="name">
        /// The item's name.
        /// </param>
        /// <param name="slot">
        /// The <see cref="Slot"/> an item occupies.
        /// </param>
        public Item(string name, Slot slot)
            : base(name, name)
        {
            Slot = slot;
        }

        /// <summary>
        /// Are the two objects equal?
        /// </summary>
        /// <param name="other">
        /// The object to compare with this one.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="other"/> cannot be null.
        /// </exception>
        public override bool Equals(object other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            if (other is Item)
            {
                return Equals(other as Item);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Are the two items equal?
        /// </summary>
        /// <param name="other">
        /// The item to compare with this one.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="other"/> cannot be null.
        /// </exception>
        public bool Equals(Item other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            return Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase)
                && Slot == other.Slot;
        }

        /// <summary>
        /// Construct a unique hash code for the object.
        /// </summary>
        /// <returns>
        /// The object's hashcode.
        /// </returns>
        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + Name + "|" + Slot.ToString()).GetHashCode();
        }

        /// <summary>
        /// The slot an item occupies.
        /// </summary>
        public Slot Slot
        {
            get;
            private set;
        }
    }
}
