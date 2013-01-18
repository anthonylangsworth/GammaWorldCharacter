using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GammaWorldCharacter.Gear;
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
            EquippedGear = new Dictionary<string, ItemJsonData>();
            OtherGear = new List<ItemJsonData>();
        }

        /// <summary>
        /// The character name.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name;

        /// <summary>
        /// The name of the player that uses the character.
        /// </summary>
        [JsonProperty("playerName", Required = Required.AllowNull)]
        public string PlayerName;

        /// <summary>
        /// The ability scores (Strength, Constitution, Dexterity, Intelligence, Wisdom, Charisma).
        /// </summary>
        [JsonProperty("abilityScores", Required = Required.Always)]
        public Dictionary<string, int> AbilityScores
        {
            get;
            private set;
        }

        /// <summary>
        /// The name of the primary origin <see cref="Type"/>.
        /// </summary>
        [JsonProperty("primaryOriginType", Required = Required.Always)]
        public string PrimaryOrigin;

        /// <summary>
        /// The name of the secondary origin <see cref="Type"/>.
        /// </summary>
        [JsonProperty("secondaryOriginType", Required = Required.Always)]
        public string SecondaryOrigin;

        /// <summary>
        /// The name of the secondary origin <see cref="Type"/>.
        /// </summary>
        [JsonProperty("trainedSkill", Required = Required.Always)]
        public string TrainedSkill;

        /// <summary>
        /// Item in the main hand.
        /// </summary>
        [JsonProperty("mainHand", Required = Required.Always)]
        public ItemJsonData MainHand;

        /// <summary>
        /// Item in the off hand.
        /// </summary>
        [JsonProperty("offHand", Required = Required.Always)]
        public ItemJsonData OffHand;

        /// <summary>
        /// Equipped items.
        /// </summary>
        [JsonProperty("equippedGear", Required = Required.Always)]
        public Dictionary<string, ItemJsonData> EquippedGear
        {
            get;
            private set;
        }

        /// <summary>
        /// Other carried gear.
        /// </summary>
        [JsonProperty("otherGear", Required = Required.Always)]
        public IList<ItemJsonData> OtherGear
        {
            get;
            private set;
        }

        // TODO: Gear (held items, equipped items, carried items), levels
    }
}
