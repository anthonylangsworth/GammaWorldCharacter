using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// A class that stores the details of a <see cref="Character"/> for 
    /// serialization and deserialization.
    /// </summary>
    public class CharacterJsonData
    {
        /// <summary>
        /// Create a new <see cref="CharacterJsonData"/>.
        /// </summary>
        public CharacterJsonData()
        {
            AbilityScores = new Dictionary<string, int>();
        }

        /// <summary>
        /// The character name.
        /// </summary>
        [JsonProperty("name")]
        public string Name;

        /// <summary>
        /// The name of the player that uses the character.
        /// </summary>
        [JsonProperty("playerName")]
        public string PlayerName;

        /// <summary>
        /// The ability scores (Strength, Constitution, Dexterity, Intelligence, Wisdom, Charisma).
        /// </summary>
        [JsonProperty("abilityScores")]
        public Dictionary<string, int> AbilityScores
        {
            get;
            private set;
        }

        /// <summary>
        /// The name of the primary origin <see cref="Type"/>.
        /// </summary>
        [JsonProperty("primaryOrigin")]
        public string PrimaryOrigin;

        /// <summary>
        /// The name of the secondary origin <see cref="Type"/>.
        /// </summary>
        [JsonProperty("secondaryOrigin")]
        public string SecondaryOrigin;

        // TODO: Gear (held items, equipped items, carried items), trained skill, levels
    }
}
