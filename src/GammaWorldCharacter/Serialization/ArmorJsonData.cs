using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// A class that stores the details of <see cref="Armor"/> for 
    /// serialization and deserialization.
    /// </summary>
    [JsonObject()]
    public class ArmorJsonData: ItemJsonData
    {
        /// <summary>
        /// Whether the armor is heavy or light.
        /// </summary>
        [JsonProperty("bonus", Required = Required.Always)]
        public ArmorWeight Weight; 

        /// <summary>
        /// Construct a <see cref="Armor"/> from this object.
        /// </summary>
        /// <returns></returns>
        public override Item ToItem()
        {
            Item result = null;
            switch (Weight)
            {
                case ArmorWeight.Heavy:
                    result = new HeavyArmor();
                    break;
                case ArmorWeight.Light:
                    result = new LightArmor();
                    break;
                case ArmorWeight.Shield:
                    result = new Shield();
                    break;
                default:
                    throw new InvalidOperationException("Unknown weight.");
            }
            return result;
        }
    }
}
