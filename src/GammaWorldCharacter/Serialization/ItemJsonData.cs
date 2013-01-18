using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using Newtonsoft.Json;

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
        public string Slot;
    }
}
