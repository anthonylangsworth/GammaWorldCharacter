using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter
{
    /// <summary>
    /// An <see cref="EqualityComparer{T}"/> that uses identity (i.e. '==' or reference equality)
    /// rather than <see cref="IEquatable{T}"/> to determine equality.
    /// </summary>
    /// <typeparam name="T">
    /// The type of items being compared.
    /// </typeparam>
    internal class IdentityEqualityComparer<T>: EqualityComparer<T>
        where T: class
    {
        /// <summary>
        /// Create a new <see cref="IdentityEqualityComparer{T}"/>.
        /// </summary>
        public IdentityEqualityComparer()
        {
            // Do nothing
        }

        /// <summary>
        /// Are the two objects equal?
        /// </summary>
        /// <param name="x">
        /// The first object to compare.
        /// </param>
        /// <param name="y">
        /// The second object to compare.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        public override bool Equals(T x, T y)
        {
            return x == y;
        }

        /// <summary>
        /// Determine the hash for the given object.
        /// </summary>
        /// <param name="obj">
        /// The object to get the has for.
        /// </param>
        /// <returns>
        /// The hashcode of the given object.
        /// </returns>
        public override int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
