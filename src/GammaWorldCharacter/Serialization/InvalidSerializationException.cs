using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// Thrown when an error occurred serializing or deserializing a <see cref="Character"/>.
    /// </summary>
    public class InvalidSerializationException: Exception
    {
        /// <summary>
        /// Create a new <see cref="InvalidSerializationException"/>.
        /// </summary>
        public InvalidSerializationException()
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="InvalidSerializationException"/>.
        /// </summary>
        /// <param name="message"></param>
        public InvalidSerializationException(string message)
            : base(message)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="InvalidSerializationException"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidSerializationException(string message, Exception innerException)
            : base(message, innerException)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="InvalidSerializationException"/>.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected InvalidSerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Do nothing
        }
    }
}
