using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Levels;
using Newtonsoft.Json;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// A serialized character's level.
    /// </summary>
    [JsonObject()]
    public abstract class LevelJsonData
    {
        /// <summary>
        /// The level number.
        /// </summary>
        [JsonProperty("number")]
        public int Number;

        /// <summary>
        /// Serialize the given <see cref="Level"/>.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="level"/> cannot be null.
        /// </exception>
        public static LevelJsonData FromLevel(Level level)
        {
            if (level == null)
            {
                throw new ArgumentNullException("level");
            }

            LevelJsonData result;

            result = null;
            if (level is Level02)
            {
                result = new Level02JsonData()
                    {
                        CriticalHitBenefit = ((Level02) level).CriticalHitBenefitOrigin
                    };
            }
            else if (level is Level03)
            {
                result = new Level03JsonData()
                {
                    UtilityPowerOrigin = ((Level03)level).UtilityPowerOrigin
                };
            }
            else
            {
                throw new ArgumentException("Unknown or invalid level", "level");
            }

            return result;
        }

        /// <summary>
        /// Deserialize this to a <see cref="Level"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Level ToLevel();
    }
}
