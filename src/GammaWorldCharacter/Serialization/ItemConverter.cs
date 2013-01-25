using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Gear.Weapons;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// Convert an <see cref="Item"/> to or from JSON, handling
    /// the subclasses appropriately.
    /// </summary>
    public class ItemConverter: JsonConverter
    {
        /// <summary>
        /// Type property name.
        /// </summary>
        internal static readonly string TypePropertyName = "type";
        /// <summary>
        /// Value property name.
        /// </summary>
        internal static readonly string ValuePropertyName = "value";
        /// <summary>
        /// Ranged type property name.
        /// </summary>
        internal static readonly string RangedTypePropertyName = "rangedType";
        /// <summary>
        /// Name property name.
        /// </summary>
        internal static readonly string NamePropertyName = "name";
        /// <summary>
        /// Slot property name.
        /// </summary>
        internal static readonly string SlotPropertyName = "slot";
        /// <summary>
        /// Weight property name.
        /// </summary>
        internal static readonly string WeightPropertyName = "weight";
        /// <summary>
        /// Handedness property name.
        /// </summary>
        internal static readonly string HandednessPropertyName = "handedness";

        /// <summary>
        /// Ranged Weapon
        /// </summary>
        internal static readonly string RangedWeaponType = "Ranged Weapon";
        /// <summary>
        /// Weapon (usually Melee)
        /// </summary>
        internal static readonly string MeleeWeaponType = "Melee Weapon";
        /// <summary>
        /// Heavy Armor
        /// </summary>
        internal static readonly string HeavyArmorType = "Heavy Armor";
        /// <summary>
        /// Light Armor
        /// </summary>
        internal static readonly string LightArmorType = "Light Armor";
        /// <summary>
        /// Shield
        /// </summary>
        internal static readonly string ShieldType = "Shield";
        /// <summary>
        /// Item
        /// </summary>
        internal static readonly string ItemType = "Item";

        /// <summary>
        /// Serialize an <see cref="Item"/> and its subclasses to JSON.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
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

            Dictionary<Type, Action<JsonWriter, Item>> conversion;
            Action<JsonWriter, Item> itemSerializer;
            Item item;

            item = value as Item;
            if (item == null)
            {
                throw new ArgumentException("value must be an Item");
            }

            conversion = new Dictionary<Type, Action<JsonWriter, Item>>();
            conversion[typeof(RangedWeapon)] = WriteRangedWeapon;
            conversion[typeof(MeleeWeapon)] = WriteMeleeWeapon;
            conversion[typeof(HeavyArmor)] = WriteHeavyArmor;
            conversion[typeof(LightArmor)] = WriteLightArmor;
            conversion[typeof(Shield)] = WriteShield;
            conversion[typeof(Item)] = WriteItem;

            if (conversion.TryGetValue(value.GetType(), out itemSerializer))
            {
                writer.WriteStartObject();
                itemSerializer(writer, item);
                writer.WriteEndObject();
            }
            else
            {
                throw new InvalidSerializationException(
                    string.Format("Unknown or invalid item type '{0}' for item '{1}'", value.GetType().Name, item));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void WriteRangedWeapon(JsonWriter writer, Item item)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            RangedWeapon rangedWeapon;

            rangedWeapon = item as RangedWeapon;
            if (rangedWeapon == null)
            {
                throw new ArgumentException("item must be a RangedWeapon");
            }

            writer.WritePropertyName(TypePropertyName);
            writer.WriteValue(RangedWeaponType);
            writer.WritePropertyName(RangedTypePropertyName);
            writer.WriteValue(rangedWeapon.Type.ToString());
            WriteWeaponCommonProperties(writer, rangedWeapon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void WriteMeleeWeapon(JsonWriter writer, Item item)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            MeleeWeapon meleeWeapon;

            meleeWeapon = item as MeleeWeapon;
            if (meleeWeapon == null)
            {
                throw new ArgumentException("item must be a Weapon");
            }

            writer.WritePropertyName(TypePropertyName);
            writer.WriteValue(MeleeWeaponType);
            WriteWeaponCommonProperties(writer, meleeWeapon);
        }

        private void WriteWeaponCommonProperties(JsonWriter writer, Weapon weapon)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (weapon == null)
            {
                throw new ArgumentNullException("weapon");
            }

            writer.WritePropertyName(WeightPropertyName);
            writer.WriteValue(weapon.Weight.ToString());
            writer.WritePropertyName(HandednessPropertyName);
            writer.WriteValue(weapon.Handedness);  // Serialize as a number (1 or 2)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void WriteHeavyArmor(JsonWriter writer, Item item)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (!(item is HeavyArmor))
            {
                throw new ArgumentException("item must be HeavyArmor");
            }

            writer.WritePropertyName(TypePropertyName);
            writer.WriteValue(HeavyArmorType); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void WriteLightArmor(JsonWriter writer, Item item)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (!(item is LightArmor))
            {
                throw new ArgumentException("item must be LightArmor");
            }

            writer.WritePropertyName(TypePropertyName);
            writer.WriteValue(LightArmorType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void WriteShield(JsonWriter writer, Item item)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (!(item is Shield))
            {
                throw new ArgumentException("item must be Shield");
            }

            writer.WritePropertyName(TypePropertyName);
            writer.WriteValue(ShieldType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void WriteItem(JsonWriter writer, Item item)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            writer.WritePropertyName(TypePropertyName);
            writer.WriteValue(ItemType);
            writer.WritePropertyName(NamePropertyName);
            writer.WriteValue(item.Name);
            if (item.Slot != Slot.None)
            {
                writer.WritePropertyName(SlotPropertyName);
                writer.WriteValue(item.Slot.ToString());
            }
        }

        /// <summary>
        /// Deserialize an <see cref="Item"/> and its subclasses from JSON.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <exception cref="ArgumentNullException"></exception>
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

            Dictionary<string, Func<JObject, Item>> conversion;
            Func<JObject, Item> itemDeserializer;
            Item result;
            JObject jObject;
            JToken typeProperty;

            // Order is important below to ensure subclasses are handled first.
            conversion = new Dictionary<string, Func<JObject, Item>>();
            conversion[RangedWeaponType] = ReadRangedWeapon;
            conversion[MeleeWeaponType] = ReadMeleeWeapon;
            conversion[HeavyArmorType] = ReadHeavyArmor;
            conversion[LightArmorType] = ReadLightArmor;
            conversion[ShieldType] = ReadShield;
            conversion[ItemType] = ReadItem;

            // Using a JObject is slightly slower than iterating through the tokens
            // but it is less code, allows the properties to be in any order and
            // does not exclude additional properties in the JSON.
            if(reader.TokenType == JsonToken.StartObject)
            {
                jObject = JObject.Load(reader);
            }
            else if (reader.TokenType == JsonToken.Null)
            {
                jObject = null;
            }
            else
            {
                throw new InvalidSerializationException("Invalid item JSON");
            }

            // Deserialize the item
            if (jObject == null)
            {
                result = null;
            }
            else
            {
                typeProperty = jObject[TypePropertyName];

                if (typeProperty == null)
                {
                    throw new InvalidSerializationException(
                        string.Format("Missing property '{0}' in item JSON: {1}", TypePropertyName, jObject));
                }
                else if (conversion.TryGetValue(typeProperty.Value<string>(), out itemDeserializer))
                {
                    result = itemDeserializer(jObject);
                }
                else
                {
                    throw new InvalidSerializationException(
                        string.Format("Unknown or invalid item type '{0}' in: {1}", typeProperty.Value<string>(), jObject));
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Item ReadRangedWeapon(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }
            foreach (string propertyName in
                new[] {RangedTypePropertyName, WeightPropertyName, HandednessPropertyName})
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new InvalidSerializationException(
                        string.Format("Property '{0}' missing in ranged weapon JSON '{1}'", propertyName, jObject));
                }
            }

            RangedType rangedWeaponType;
            WeaponWeight weaponWeight;
            WeaponHandedness weaponHandedness;

            if (!Enum.TryParse(jObject[RangedTypePropertyName].Value<string>(), true, out rangedWeaponType))
            {
                throw new InvalidSerializationException(
                        string.Format("Property '{0}' has invalid value in ranged weapon JSON '{1}'", RangedTypePropertyName, jObject));
            }
            weaponWeight = ReadWeaponWeight(jObject);
            weaponHandedness = ReadWeaponHandedness(jObject);

            return new RangedWeapon(rangedWeaponType, weaponHandedness, weaponWeight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Item ReadMeleeWeapon(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }
            foreach (string propertyName in
                new[] { WeightPropertyName, HandednessPropertyName })
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new InvalidSerializationException(
                        string.Format("Property '{0}' missing in melee weapon JSON '{1}'", propertyName, jObject));
                }
            }

            WeaponWeight weaponWeight;
            WeaponHandedness weaponHandedness;

            weaponWeight = ReadWeaponWeight(jObject);
            weaponHandedness = ReadWeaponHandedness(jObject);

            return new MeleeWeapon(weaponHandedness, weaponWeight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private WeaponWeight ReadWeaponWeight(JObject jObject)
        {
            WeaponWeight result;

            if (!Enum.TryParse(jObject[WeightPropertyName].Value<string>(), true, out result))
            {
                throw new InvalidSerializationException(
                        string.Format("Property '{0}' has invalid value in weapon JSON '{1}'", WeightPropertyName, jObject));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private WeaponHandedness ReadWeaponHandedness(JObject jObject)
        {
            int propertyValue;
            WeaponHandedness result;

            if (!int.TryParse(jObject[HandednessPropertyName].Value<string>(), out propertyValue))
            {
                throw new InvalidSerializationException(
                        string.Format("Property '{0}' is not an integer JSON '{1}'", HandednessPropertyName, jObject));
            }
            else if (!Enum.IsDefined(typeof(WeaponHandedness), propertyValue))
            {
                throw new InvalidSerializationException(
                        string.Format("Property '{0}' must be 1 or 2 in weapon JSON '{1}'", HandednessPropertyName, jObject));
            }
            else
            {
                result = (WeaponHandedness) propertyValue;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Item ReadHeavyArmor(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }

            return new HeavyArmor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Item ReadLightArmor(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }

            return new LightArmor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Item ReadShield(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }

            return new Shield();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        private Item ReadItem(JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }
            foreach (string propertyName in
                new[] {NamePropertyName})
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new InvalidSerializationException(
                        string.Format("Property '{0}' missing in item weapon JSON '{1}'", propertyName, jObject));
                }
            }

            string name;
            JToken slotProperty;
            Slot slot;

            name = jObject[NamePropertyName].Value<string>();

            slotProperty = jObject[SlotPropertyName];
            if (slotProperty != null)
            {
                if (!Enum.TryParse(slotProperty.Value<string>(), true, out slot))
                {
                    throw new InvalidSerializationException(
                        string.Format("Property '{0}' has invalid value in melee weapon JSON '{1}'",
                                      SlotPropertyName, jObject));
                }
            }
            else
            {
                slot = Slot.None;
            }

            return new Item(name, slot);
        }

        /// <summary>
        /// Convert <see cref="Item"/> and subclasses.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException("objectType");
            }

            return typeof(Item).IsAssignableFrom(objectType);
        }
    }
}
