using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Levels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// Convert a <see cref="Level"/> to or from JSON.
    /// </summary>
    public class LevelConverter : JsonConverter
    {
        /// <summary>
        /// Level property name.
        /// </summary>
        internal static readonly string LevelPropertyName = "level";
        /// <summary>
        /// Critical hit benefit origin property name.
        /// </summary>
        internal static readonly string CriticalHitBenefitOriginPropertyName = "criticalHitBenefitOrigin";
        /// <summary>
        /// Utility power origin property name.
        /// </summary>
        internal static readonly string UtilityPowerOriginPropertyName = "utilityPowerOrigin";

        /// <summary>
        /// Can this serialize and deserialize an object of the given type?
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException("objectType");
            }

            return typeof(Level).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Serialize a <see cref="Level"/> to JSON.
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

            Dictionary<Type, Action<JsonWriter, Level>> conversion;
            Action<JsonWriter, Level> levelSerializer;
            Level level;

            level = value as Level;
            if (level == null)
            {
                throw new ArgumentException("value must be an Item");
            }

            conversion = new Dictionary<Type, Action<JsonWriter, Level>>();
            conversion[typeof(Level03)] = WriteLevel03;
            conversion[typeof(Level02)] = WriteLevel02;

            if (conversion.TryGetValue(value.GetType(), out levelSerializer))
            {
                writer.WriteStartObject();
                levelSerializer(writer, level);
                writer.WriteEndObject();
            }
            else
            {
                throw new InvalidSerializationException(
                    string.Format("Unknown or invalid level type '{0}' for level '{1}'", value.GetType().Name, level.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="level"></param>
        private void WriteLevel02(JsonWriter writer, Level level)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (level == null)
            {
                throw new ArgumentNullException("level");
            }

            Level02 level02;

            level02 = level as Level02;
            if (level02 == null)
            {
                throw new ArgumentException("level must be of type Level02", "level");
            }

            writer.WritePropertyName(LevelPropertyName);
            writer.WriteValue(level02.Number);
            writer.WritePropertyName(CriticalHitBenefitOriginPropertyName);
            writer.WriteValue(level02.CriticalHitBenefitOrigin.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="level"></param>
        private void WriteLevel03(JsonWriter writer, Level level)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (level == null)
            {
                throw new ArgumentNullException("level");
            }

            Level03 level03;

            level03 = level as Level03;
            if (level03 == null)
            {
                throw new ArgumentException("level must be of type Level03", "level");
            }

            writer.WritePropertyName(LevelPropertyName);
            writer.WriteValue(level03.Number);
            writer.WritePropertyName(UtilityPowerOriginPropertyName);
            writer.WriteValue(level03.UtilityPowerOrigin.ToString());
        }

        /// <summary>
        /// Deserialize a <see cref="Level"/> from JSON.
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

            Dictionary<int, Func<JObject, Level>> conversion;
            Func<JObject, Level> levelDeserializer;
            Level result;
            JObject jObject;
            JToken levelProperty;
            int level;

            // Order is important below to ensure subclasses are handled first.
            conversion = new Dictionary<int, Func<JObject, Level>>();
            conversion[3] = ReadLevel03;
            conversion[2] = ReadLevel02;

            // Using a JObject is slightly slower than iterating through the tokens
            // but it is less code, allows the properties to be in any order and
            // does not exclude additional properties in the JSON.
            if (reader.TokenType == JsonToken.StartObject)
            {
                jObject = JObject.Load(reader);
            }
            else if (reader.TokenType == JsonToken.Null)
            {
                jObject = null;
            }
            else
            {
                throw new InvalidSerializationException("Invalid level JSON");
            }

            // Deserialize the item
            if (jObject == null)
            {
                result = null;
            }
            else
            {
                levelProperty = jObject[LevelPropertyName];

                if (levelProperty == null)
                {
                    throw new InvalidSerializationException(
                        string.Format("Missing property '{0}' in level JSON: {1}", LevelPropertyName, jObject));
                }
                else if(!int.TryParse(levelProperty.Value<string>(), out level))
                {
                    throw new InvalidSerializationException(
                        string.Format("Non-integral level '{0}' in: {1}", levelProperty.Value<string>(), jObject));
                }
                else if (conversion.TryGetValue(level, out levelDeserializer))
                {
                    result = levelDeserializer(jObject);
                }
                else
                {
                    throw new InvalidSerializationException(
                        string.Format("Unknown or invalid level '{0}' in: {1}", levelProperty.Value<string>(), jObject));
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Level ReadLevel02(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }
            foreach (string propertyName in
                new[] { LevelPropertyName, CriticalHitBenefitOriginPropertyName })
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new InvalidSerializationException(
                        string.Format("Property '{0}' missing in level 2 JSON '{1}'", propertyName, jObject));
                }
            }

            OriginChoice criticalHitBenefitOrigin;

            if (!Enum.TryParse(jObject[CriticalHitBenefitOriginPropertyName].Value<string>(), true, out criticalHitBenefitOrigin))
            {
                throw new InvalidSerializationException(
                        string.Format("Property '{0}' has invalid value in level 2 JSON '{1}'", CriticalHitBenefitOriginPropertyName, jObject));
            }

            return new Level02(criticalHitBenefitOrigin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Level ReadLevel03(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }
            foreach (string propertyName in
                new[] { LevelPropertyName, UtilityPowerOriginPropertyName })
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new InvalidSerializationException(
                        string.Format("Property '{0}' missing in level 3 JSON '{1}'", propertyName, jObject));
                }
            }

            OriginChoice utilityPowerOrigin;

            if (!Enum.TryParse(jObject[UtilityPowerOriginPropertyName].Value<string>(), true, out utilityPowerOrigin))
            {
                throw new InvalidSerializationException(
                        string.Format("Property '{0}' has invalid value in level 3 JSON '{1}'", UtilityPowerOriginPropertyName, jObject));
            }

            return new Level03(utilityPowerOrigin);
        }
    }
}
