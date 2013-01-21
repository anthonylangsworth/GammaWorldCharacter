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
    /// Stores details of <see cref="RangedWeapon"/> for 
    /// serialization and deserialization.
    /// </summary>
    [JsonObject()]
    public class RangedWeaponJsonData : WeaponJsonData
    {
        /// <summary>
        /// The weapon's weight.
        /// </summary>
        [JsonProperty("rangedType", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RangedType RangedType;

        /// <summary>
        /// Construct a <see cref="RangedWeapon"/>.
        /// </summary>
        /// <returns></returns>
        public override Item ToItem()
        {
            return new RangedWeapon(RangedType, Handedness, Weight);
        }
    }
}
