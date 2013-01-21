using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// Convert a <see cref="Type"/> to its full name and back again.
    /// </summary>
    public class TypeNameConverter : JsonConverter
    {
        /// <summary>
        /// Can it convert an object of the given <see cref="Type"/>?
        /// </summary>
        /// <param name="objectType">
        /// The <see cref="Type"/> of object to convert.
        /// </param>
        /// <returns>
        /// True if the object can be converted, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectType"/> cannot be null.
        /// </exception>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException("objectType");
            }

            return objectType == typeof (Type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (objectType == null)
            {
                throw new ArgumentNullException("objectType");
            }
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            Type result;

            result = null;
            if (reader.TokenType == JsonToken.String)
            {
                result =  Type.GetType(reader.Value.ToString());
            }
            else if (reader.TokenType != JsonToken.Null)
            {
                throw new JsonSerializationException("INvalid type name");
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (serializer == null)
            {
                throw new ArgumentNullException("serializer");
            }

            writer.WriteValue(((Type) value).FullName);
        }
    }
}
