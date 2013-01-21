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
    /// A class that stores the details of an <see cref="Item"/> for 
    /// serialization and deserialization.
    /// </summary>
    [JsonObject()]
    public class ItemJsonData
    {
        /// <summary>
        /// The item's name.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name;

        /// <summary>
        /// The item's slot.
        /// </summary>
        [JsonProperty("slot", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Slot Slot;

        /// <summary>
        /// Create a new <see cref="ItemJsonData"/>.
        /// </summary>
        /// <param name="item">
        /// The <see cref="Item"/> to initialize the object from. This
        /// cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="item"/> cannot be null.
        /// </exception>
        public static ItemJsonData FromItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // To make this more generic, it could use a "chain
            // of responsibility" pattern, i.e. have a 
            // List<Tuple<Predicate<Type>, Func<Jtem, ItemJsonData>>>
            // and select the serialization appropriately.

            ItemJsonData result;
            Weapon weapon;
            RangedWeapon rangedWeapon;

            result = null;
            if (item.GetType() == typeof (Weapon)
                || item.GetType() == typeof(MeleeWeapon))
            {
                weapon = (Weapon) item;
                result = new WeaponJsonData()
                    {
                        Weight = weapon.Weight,
                        Handedness = weapon.Handedness
                    };
            }
            else if (item.GetType() == typeof(RangedWeapon))
            {
                rangedWeapon = (RangedWeapon)item;
                result = new RangedWeaponJsonData()
                {
                    Weight = rangedWeapon.Weight,
                    Handedness = rangedWeapon.Handedness,
                    RangedType = rangedWeapon.Type
                };
            }
            else if (item.GetType() == typeof(HeavyArmor))
            {
                result = new ArmorJsonData()
                {
                    Weight = ArmorWeight.Heavy
                };
            }
            else if(item.GetType() == typeof(LightArmor))
            {
                result = new ArmorJsonData()
                {
                    Weight = ArmorWeight.Light
                };
            }
            else if(item.GetType() == typeof(Shield))
            {
                result = new ArmorJsonData()
                {
                    Weight = ArmorWeight.Shield
                };
            }
            else if (item.GetType() != typeof (Item))
            {
                throw new ArgumentException("Unknown Item subclass", "item");
            }
            else
            {
                result = new ItemJsonData();
            }

            // Common fields
            result.Name = item.Name;
            result.Slot = item.Slot;

            return result;
        }

        /// <summary>
        /// Create an <see cref="Item"/> from this object.
        /// </summary>
        /// <remarks>
        /// Overriders should not call the base class implementation.
        /// </remarks>
        /// <returns>
        /// </returns>
        public virtual Item ToItem()
        {
            return new Item(Name, Slot);
        }
    }
}
