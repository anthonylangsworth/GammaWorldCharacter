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
        public readonly string TypePropertyName = "type";
        /// <summary>
        /// Value property name.
        /// </summary>
        public readonly string ValuePropertyName = "value";
        /// <summary>
        /// Ranged type property name.
        /// </summary>
        public readonly string RangedTypePropertyName = "rangedType";
        /// <summary>
        /// Name property name.
        /// </summary>
        public readonly string NamePropertyName = "name";
        /// <summary>
        /// Slot property name.
        /// </summary>
        public readonly string SlotPropertyName = "slot";
        /// <summary>
        /// Weight property name.
        /// </summary>
        public readonly string WeightPropertyName = "weight";
        /// <summary>
        /// Handedness property name.
        /// </summary>
        public readonly string HandednessPropertyName = "handedness";

        /// <summary>
        /// Ranged Weapon
        /// </summary>
        public readonly string RangedWeaponType = "Ranged Weapon";
        /// <summary>
        /// Weapon (usually Melee)
        /// </summary>
        public readonly string MeleeWeaponType = "MeleeWeapon";
        /// <summary>
        /// Heavy Armor
        /// </summary>
        public readonly string HeavyArmorType = "Heavy Armor";
        /// <summary>
        /// Light Armor
        /// </summary>
        public readonly string LightArmorType = "Light Armor";
        /// <summary>
        /// Shield
        /// </summary>
        public readonly string ShieldType = "Shield";
        /// <summary>
        /// Item
        /// </summary>
        public readonly string ItemType = "Item";

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
                throw new ArgumentException("Unknown or invalid item", "value");
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

            writer.WritePropertyName(this.TypePropertyName);
            writer.WriteValue(this.RangedWeaponType);
            writer.WritePropertyName(this.RangedTypePropertyName);
            writer.WriteValue(JsonConvert.SerializeObject(rangedWeapon.Type, new StringEnumConverter()));
            writer.WritePropertyName(this.WeightPropertyName);
            writer.WriteValue(JsonConvert.SerializeObject(rangedWeapon.Weight, new StringEnumConverter()));
            writer.WritePropertyName(this.HandednessPropertyName);
            writer.WriteValue(JsonConvert.SerializeObject(rangedWeapon.Handedness, new StringEnumConverter()));
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

            Weapon weapon;

            weapon = item as Weapon;
            if (weapon == null)
            {
                throw new ArgumentException("item must be a Weapon");
            }

            writer.WritePropertyName(this.TypePropertyName);
            writer.WriteValue(this.MeleeWeaponType);
            writer.WritePropertyName(this.WeightPropertyName);
            writer.WriteValue(JsonConvert.SerializeObject(weapon.Weight, new StringEnumConverter()));
            writer.WritePropertyName(this.HandednessPropertyName);
            writer.WriteValue(JsonConvert.SerializeObject(weapon.Handedness, new StringEnumConverter()));
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

            writer.WritePropertyName(this.TypePropertyName);
            writer.WriteValue(this.HeavyArmorType); 
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

            writer.WritePropertyName(this.TypePropertyName);
            writer.WriteValue(this.LightArmorType);
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

            writer.WritePropertyName(this.TypePropertyName);
            writer.WriteValue(this.ShieldType);
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

            writer.WritePropertyName(this.TypePropertyName);
            writer.WriteValue(this.NamePropertyName);
            writer.WritePropertyName("name");
            writer.WriteValue(item.Name);
            writer.WritePropertyName("slot");
            writer.WriteValue(JsonConvert.SerializeObject(item.Slot, new StringEnumConverter()));
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
            //string typeName;
            Func<JObject, Item> itemDeserializer;
            Item result;
            JObject jObject;

            // Order is important below to ensure subclasses are handled first.
            conversion = new Dictionary<string, Func<JObject, Item>>();
            conversion[this.RangedWeaponType] = ReadRangedWeapon;
            conversion[this.MeleeWeaponType] = ReadMeleeWeapon;
            conversion[this.HeavyArmorType] = ReadHeavyArmor;
            conversion[this.LightArmorType] = ReadLightArmor;
            conversion[this.ShieldType] = ReadShield;
            conversion[this.ItemType] = ReadItem;

            // Using a JObject is slightly slower than iterating through the tokens
            // but it is less code, allows the properties to be in any order and
            // does not exclude additional properties in the JSON.
            jObject = JObject.Load(reader);

            // Ensure it is the start of a property
            //if (reader.TokenType != JsonToken.StartObject)
            //{
            //    throw new ArgumentException("Not an object");
            //}
            //reader.Read();
            //if (reader.TokenType != JsonToken.PropertyName
            //    || this.TypePropertyName.Equals(reader.Value))
            //{
            //    throw new ArgumentException(
            //        string.Format("First property must be {0}", this.TypePropertyName));    
            //}
            //reader.Read();

            //if (reader.TokenType == JsonToken.String)
            //{
            //    typeName = (string) reader.Value;
            //}
            //else
            //{
            //    throw new JsonSerializationException("Invalid item serialization");
            //}

            if (conversion.TryGetValue(jObject[this.TypePropertyName].Value<string>(), out itemDeserializer))
            {
                result = itemDeserializer(jObject);
            }
            else
            {
                throw new ArgumentException("Unknown or invalid item");
            }

            //reader.Read();
            //if (reader.TokenType != JsonToken.EndObject)
            //{
            //    throw new ArgumentException("Trailing properties or tokens");
            //}

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
                new[] {this.RangedTypePropertyName, this.WeightPropertyName, this.HandednessPropertyName})
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new ArgumentException(
                        string.Format("Property '{0}' missing", propertyName), "jObject");
                }
            }

            RangedType rangedWeaponType;
            WeaponWeight weaponWeight;
            WeaponHandedness weaponHandedness;

            rangedWeaponType = (RangedType) JsonConvert.DeserializeObject(
                jObject[this.RangedTypePropertyName].Value<string>(), typeof(RangedType), new StringEnumConverter());
            weaponWeight = (WeaponWeight)JsonConvert.DeserializeObject(
                jObject[this.WeightPropertyName].Value<string>(), typeof(WeaponWeight), new StringEnumConverter());
            weaponHandedness = (WeaponHandedness)JsonConvert.DeserializeObject(
                jObject[this.HandednessPropertyName].Value<string>(), typeof(WeaponHandedness), new StringEnumConverter());

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
                new[] { this.WeightPropertyName, this.HandednessPropertyName })
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new ArgumentException(
                        string.Format("Property '{0}' missing", propertyName), "jObject");
                }
            }

            WeaponWeight weaponWeight;
            WeaponHandedness weaponHandedness;

            weaponWeight = (WeaponWeight)JsonConvert.DeserializeObject(
                jObject[this.WeightPropertyName].Value<string>(), typeof(WeaponWeight), new StringEnumConverter());
            weaponHandedness = (WeaponHandedness)JsonConvert.DeserializeObject(
                jObject[this.HandednessPropertyName].Value<string>(), typeof(WeaponHandedness), new StringEnumConverter());

            return new MeleeWeapon(weaponHandedness, weaponWeight);
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
                new[] { this.NamePropertyName, this.SlotPropertyName })
            {
                if (jObject.Property(propertyName) == null)
                {
                    throw new ArgumentException(
                        string.Format("Property '{0}' missing", propertyName), "jObject");
                }
            }

            string name;
            Slot slot;

            name = jObject[this.NamePropertyName].Value<string>();
            slot = (Slot)JsonConvert.DeserializeObject(
                jObject[this.SlotPropertyName].Value<string>(), typeof(Slot), new StringEnumConverter());

            return new Item(name, slot);
        }

        /// <summary>
        /// Convert <see cref="Item"/> and subclasses.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Item).IsAssignableFrom(objectType);
        }
    }
}
