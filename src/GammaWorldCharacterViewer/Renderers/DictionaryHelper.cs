using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// Helper methods for dealing with dictionaries.
    /// </summary>
    internal static class DictionaryHelper
    {
        /// <summary>
        /// If the given <see cref="IDictionary"/> does not contain a resource of type <typeparamref name="T"/>
        /// <paramref name="key"/>, throw an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="dictionary">
        /// The <see cref="IDictionary"/> to look in. This cannot be null.
        /// </param>
        /// <param name="key">
        /// The key of the element to look for. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="dictionary"/> does not contain a <typeparamref name="T"/> called
        /// <paramref name="key"/>.
        /// </exception>
        internal static void Expect<T>(IDictionary dictionary, object key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (!dictionary.Contains(key))
            {
                throw new InvalidOperationException(string.Format("Element '{0}' not present", key));
            }
            if (!(dictionary[key] is T))
            {
                throw new InvalidOperationException(string.Format("Element '{0}' present but not of type '{1}'",
                    key, typeof(T).FullName));
            }
        }

    }
}
