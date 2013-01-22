using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Levels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// Level 3.
    /// </summary>
    [JsonObject()]
    public class Level03JsonData : LevelJsonData
    {
        /// <summary>
        /// Create a new <see cref="Level02JsonData"/>.
        /// </summary>
        public Level03JsonData()
        {
            Number = 3;
        }

        /// <summary>
        /// The origin whose utility power was chosen at level3.
        /// </summary>
        [JsonProperty("utilityPowerOrigin")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OriginChoice UtilityPowerOrigin;

        /// <summary>
        /// Deserialize this to a <see cref="Level"/>.
        /// </summary>
        /// <returns></returns>
        public override Level ToLevel()
        {
            return new Level03(UtilityPowerOrigin);
        }
    }
}
