using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// Convert an <see cref="Origin"/> to JSON and vice versa.
    /// </summary>
    public class OriginConverter: JsonConverter
    {
        /// <summary>
        /// Android
        /// </summary>
        internal static readonly string AndroidOriginName = "Android";
        /// <summary>
        /// Cockroach
        /// </summary>
        internal static readonly string CockroachOriginName = "Cockroach";
        /// <summary>
        /// Doppelganger
        /// </summary>
        internal static readonly string DoppelgangerOriginName = "Doppelganger";
        /// <summary>
        /// Electrokinetic
        /// </summary>
        internal static readonly string ElectrokineticOriginName = "Electrokinetic";
        /// <summary>
        /// Empath
        /// </summary>
        internal static readonly string EmpathOriginName = "Empath";
        /// <summary>
        /// Felinoid
        /// </summary>
        internal static readonly string FelinoidOriginName = "Felinoid";
        /// <summary>
        /// Hawkoid
        /// </summary>
        internal static readonly string HawkoidOriginName = "Hawkoid";
        /// <summary>
        /// Hypercognitive
        /// </summary>
        internal static readonly string HypercognitiveOriginName = "Hypercognitive";
        /// <summary>
        /// Giant.
        /// </summary>
        internal static readonly string GiantOriginName = "Giant";
        /// <summary>
        /// Gravity controller.
        /// </summary>
        internal static readonly string GravityControllerOriginName = "Gravity Controller";

        /// <summary>
        /// Can this converter convert an object of the given type?
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException("objectType");
            }

            return typeof(Origin).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Derialize to JSON.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="value"/> is not an <see cref="Origin"/> or not a supported origin.
        /// </exception>
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

            Origin origin;
            Dictionary<Type, string> conversion;
            string originName;
            
            origin = value as Origin;
            if (origin == null)
            {
                // May want to support writing null in the future
                throw new ArgumentException("value is not an Origin", "value");
            }

            // Should be moved into a separate static property
            conversion = new Dictionary<Type, string>();
            conversion[typeof(Android)] = AndroidOriginName;
            conversion[typeof(Cockroach)] = CockroachOriginName;
            conversion[typeof(Doppelganger)] = DoppelgangerOriginName;
            conversion[typeof(Electrokinetic)] = ElectrokineticOriginName;
            conversion[typeof(Empath)] = EmpathOriginName;
            conversion[typeof(Felinoid)] = FelinoidOriginName;
            conversion[typeof(Hawkoid)] = HawkoidOriginName;
            conversion[typeof(Hypercognitive)] = HypercognitiveOriginName;
            conversion[typeof(Giant)] = GiantOriginName;
            conversion[typeof(GravityController)] = GravityControllerOriginName;

            if (conversion.TryGetValue(origin.GetType(), out originName))
            {
                writer.WriteValue(originName);
            }
            else
            {
                throw new ArgumentException("Unknown origin", "value");
            }
        }

        /// <summary>
        /// Deserialize from JSON.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <exception cref="ArgumentNullException">
        /// Neither <paramref name="reader"/>, <paramref name="objectType"/> and <paramref name="serializer"/> can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Invalid or unknown origin.
        /// </exception>
        /// <returns>
        /// The deserialized <see cref="Origin"/>.
        /// </returns>
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

            string originName;
            Func<Origin> result;
            Dictionary<string, Func<Origin>> conversion;

            // Should be moved into a separate static property
            conversion = new Dictionary<string, Func<Origin>>();
            conversion[AndroidOriginName.ToLowerInvariant()] = () => new Android();
            conversion[CockroachOriginName.ToLowerInvariant()] = () => new Cockroach();
            conversion[DoppelgangerOriginName.ToLowerInvariant()] = () => new Doppelganger();
            conversion[ElectrokineticOriginName.ToLowerInvariant()] = () => new Electrokinetic();
            conversion[EmpathOriginName.ToLowerInvariant()] = () => new Empath();
            conversion[FelinoidOriginName.ToLowerInvariant()] = () => new Felinoid();
            conversion[HawkoidOriginName.ToLowerInvariant()] = () => new Hawkoid();
            conversion[HypercognitiveOriginName.ToLowerInvariant()] = () => new Hypercognitive();
            conversion[GiantOriginName.ToLowerInvariant()] = () => new Giant();
            conversion[GravityControllerOriginName.ToLowerInvariant()] = () => new GravityController();

            if (reader.TokenType == JsonToken.String)
            {
                originName = reader.Value.ToString();
            }
            else
            {
                throw new JsonSerializationException("Invalid origin serialization");
            }

            if (!conversion.TryGetValue(originName.ToLowerInvariant(), out result))
            {
                throw new ArgumentException("Unknown origin");
            }

            return result();
        }
    }
}
