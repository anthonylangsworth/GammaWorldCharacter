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
    /// Stores the details of <see cref="Weapon"/> for 
    /// serialization and deserialization.
    /// </summary>
    [JsonObject()]
    public class WeaponJsonData : ItemJsonData
    {
        /// <summary>
        /// The weapon's weight.
        /// </summary>
        [JsonProperty("weight", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponWeight Weight;

        /// <summary>
        /// Whether the weapon requires one or two hands.
        /// </summary>
        [JsonProperty("handedness", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponHandedness Handedness;

        /// <summary>
        /// Construct a <see cref="MeleeWeapon"/> from this object.
        /// </summary>
        /// <returns></returns>
        public override Item ToItem()
        {
            return new MeleeWeapon(Handedness, Weight);
        }
    }
}
