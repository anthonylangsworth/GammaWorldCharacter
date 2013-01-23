using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Gear.Weapons;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// Convert an <see cref="ItemJsonData"/> to or from JSON, handling
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
        /// Ranged Weapon
        /// </summary>
        public readonly string RangedWeaponType = "Ranged Weapon";
        /// <summary>
        /// Weapon (usually Melee)
        /// </summary>
        public readonly string WeaponType = "Weapon";
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
        /// Serialize an <see cref="ItemJsonData"/> and its subclasses to JSON.
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
            conversion[typeof(MeleeWeapon)] = WriteWeapon;
            conversion[typeof(Weapon)] = WriteWeapon;
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
            writer.WritePropertyName("rangedType");
            writer.WriteValue(JsonConvert.SerializeObject(rangedWeapon.Type, new StringEnumConverter()));
            writer.WritePropertyName("weight");
            writer.WriteValue(JsonConvert.SerializeObject(rangedWeapon.Weight, new StringEnumConverter()));
            writer.WritePropertyName("handedness");
            writer.WriteValue(JsonConvert.SerializeObject(rangedWeapon.Handedness, new StringEnumConverter()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void WriteWeapon(JsonWriter writer, Item item)
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
            writer.WriteValue(this.WeaponType);
            writer.WritePropertyName("weight");
            writer.WriteValue(JsonConvert.SerializeObject(weapon.Weight, new StringEnumConverter()));
            writer.WritePropertyName("handedness");
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
            writer.WritePropertyName("weight");
            writer.WriteValue(JsonConvert.SerializeObject(ArmorWeight.Heavy, new StringEnumConverter()));
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
            writer.WritePropertyName("weight");
            writer.WriteValue(JsonConvert.SerializeObject(ArmorWeight.Light, new StringEnumConverter()));
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
            writer.WritePropertyName("weight");
            writer.WriteValue(JsonConvert.SerializeObject(ArmorWeight.Shield, new StringEnumConverter()));
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
            writer.WriteValue(this.ItemType);
            writer.WritePropertyName("name");
            writer.WriteValue(item.Name);
            writer.WritePropertyName("slot");
            writer.WriteValue(JsonConvert.SerializeObject(item.Slot, new StringEnumConverter()));
        }

        /// <summary>
        /// Deserialize an <see cref="ItemJsonData"/> and its subclasses from JSON.
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

            Dictionary<string, Type> conversion;
            string typeName;
            Type type;
            ItemJsonData result;

            // Order is important below to ensure subclasses are handled first.
            conversion = new Dictionary<string, Type>();
            conversion[this.RangedWeaponType] = typeof(RangedWeapon);
            conversion[this.WeaponType] = typeof(Weapon);
            conversion[this.HeavyArmorType] = typeof(HeavyArmor);
            conversion[this.LightArmorType] = typeof(LightArmor);
            conversion[this.ShieldType] = typeof(Shield);
            conversion[this.ItemType] = typeof(Item);

            if (reader.TokenType == JsonToken.String)
            {
                typeName = (string) reader.Value;
            }
            else
            {
                throw new JsonSerializationException("Invalid item serialization");
            }

            if (conversion.TryGetValue(typeName, out type))
            {
                reader.Read(); // Value property
                result = (ItemJsonData)serializer.Deserialize(reader, type);
            }
            else
            {
                throw new ArgumentException("Unknown or invalid item");
            }

            return result;
        }

        /// <summary>
        /// Convert <see cref="ItemJsonData"/> and subclasses.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof (Item).IsAssignableFrom(objectType);
        }
    }
}
