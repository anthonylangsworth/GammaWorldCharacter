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
    /// Level 2.
    /// </summary>
    [JsonObject()]
    public class Level02JsonData : LevelJsonData
    {
        /// <summary>
        /// Create a new <see cref="Level02JsonData"/>.
        /// </summary>
        public Level02JsonData()
        {
            Number = 2;
        }

        /// <summary>
        /// The critical hit benefit gained at level 2.
        /// </summary>
        [JsonProperty("criticalHitBenefit", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OriginChoice CriticalHitBenefit;

        /// <summary>
        /// Deserialize this to a <see cref="Level"/>.
        /// </summary>
        /// <returns></returns>
        public override Level ToLevel()
        {
            return new Level02(CriticalHitBenefit);
        }
    }
}
