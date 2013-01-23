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
        public readonly string AndroidOriginName = "Android";
        /// <summary>
        /// Cockroach
        /// </summary>
        public readonly string CockroachOriginName = "Cockroach";
        /// <summary>
        /// Doppelganger
        /// </summary>
        public readonly string DoppelgangerOriginName = "Doppelganger";
        /// <summary>
        /// Electrokinetic
        /// </summary>
        public readonly string ElectrokineticOriginName = "Electrokinetic";
        /// <summary>
        /// Empath
        /// </summary>
        public readonly string EmpathOriginName = "Empath";
        /// <summary>
        /// Felinoid
        /// </summary>
        public readonly string FelinoidOriginName = "Felinoid";
        /// <summary>
        /// Hawkoid
        /// </summary>
        public readonly string HawkoidOriginName = "Hawkoid";
        /// <summary>
        /// Hypercognitive
        /// </summary>
        public readonly string HypercognitiveOriginName = "Hypercognitive";
        /// <summary>
        /// Giant.
        /// </summary>
        public readonly string GiantOriginName = "Giant";
        /// <summary>
        /// Gravity controller.
        /// </summary>
        public readonly string GravityControllerOriginName = "Gravity Controller";

        /// <summary>
        /// Can this converter convert an object of the given type?
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (Origin);
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
                throw new ArgumentException("value is not an Origin", "value");
            }

            conversion = new Dictionary<Type, string>();
            conversion[typeof(Android)] = this.AndroidOriginName;
            conversion[typeof(Cockroach)] = this.CockroachOriginName;
            conversion[typeof(Doppelganger)] = this.DoppelgangerOriginName;
            conversion[typeof(Electrokinetic)] = this.ElectrokineticOriginName;
            conversion[typeof(Empath)] = this.EmpathOriginName;
            conversion[typeof(Felinoid)] = this.FelinoidOriginName;
            conversion[typeof(Hawkoid)] = this.HawkoidOriginName;
            conversion[typeof(Hypercognitive)] = this.HypercognitiveOriginName;
            conversion[typeof(Giant)] = this.GiantOriginName;
            conversion[typeof(GravityController)] = this.GravityControllerOriginName;

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

            conversion = new Dictionary<string, Func<Origin>>();
            conversion[this.AndroidOriginName.ToLowerInvariant()] = () => new Android();
            conversion[this.CockroachOriginName.ToLowerInvariant()] = () => new Cockroach();
            conversion[this.DoppelgangerOriginName.ToLowerInvariant()] = () => new Doppelganger();
            conversion[this.ElectrokineticOriginName.ToLowerInvariant()] = () => new Electrokinetic();
            conversion[this.EmpathOriginName.ToLowerInvariant()] = () => new Empath();
            conversion[this.FelinoidOriginName.ToLowerInvariant()] = () => new Felinoid();
            conversion[this.HawkoidOriginName.ToLowerInvariant()] = () => new Hawkoid();
            conversion[this.HypercognitiveOriginName.ToLowerInvariant()] = () => new Hypercognitive();
            conversion[this.GiantOriginName.ToLowerInvariant()] = () => new Giant();
            conversion[this.GravityControllerOriginName.ToLowerInvariant()] = () => new GravityController();

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
