﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Levels;
using GammaWorldCharacter.Origins;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// A class that stores the details of a <see cref="Character"/> for 
    /// serialization and deserialization.
    /// </summary>
    [JsonObject(ItemTypeNameHandling = TypeNameHandling.Auto)]
    public class CharacterJsonData
    {
        /// <summary>
        /// Create a new <see cref="CharacterJsonData"/>.
        /// </summary>
        public CharacterJsonData()
        {
            AbilityScores = new Dictionary<ScoreType, int>();
            EquippedGear = new Dictionary<Slot, Item>();
            OtherGear = new List<Item>();
            Levels = new List<Level>();
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
        public IDictionary<ScoreType, int> AbilityScores
        {
            get;
            private set;
        }

        /// <summary>
        /// The primary <see cref="Origin"/>.
        /// </summary>
        [JsonProperty("primaryOrigin", Required = Required.Always)]
        [JsonConverter(typeof(OriginConverter))]
        public Origin PrimaryOrigin;

        /// <summary>
        /// The secondary <see cref="Origin"/>.
        /// </summary>
        [JsonProperty("secondaryOrigin", Required = Required.Always)]
        [JsonConverter(typeof(OriginConverter))]
        public Origin SecondaryOrigin;

        /// <summary>
        /// The name of the secondary origin <see cref="Type"/>.
        /// </summary>
        [JsonProperty("trainedSkill", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ScoreType TrainedSkill;

        /// <summary>
        /// Item in the main hand.
        /// </summary>
        [JsonProperty("mainHand", Required = Required.AllowNull)]
        [JsonConverter(typeof(ItemConverter))]
        public Item MainHand;

        /// <summary>
        /// Item in the off hand.
        /// </summary>
        [JsonProperty("offHand", Required = Required.AllowNull)]
        [JsonConverter(typeof(ItemConverter))]
        public Item OffHand;

        /// <summary>
        /// Equipped items.
        /// </summary>
        [JsonProperty("equippedGear", Required = Required.Always)]
        public IDictionary<Slot, Item> EquippedGear
        {
            get;
            private set;
        }

        /// <summary>
        /// Other carried gear.
        /// </summary>
        [JsonProperty("otherGear", Required = Required.Always)]
        public IList<Item> OtherGear
        {
            get;
            private set;
        }

        /// <summary>
        /// The character's levels.
        /// </summary>
        [JsonProperty("levels")]
        public IList<Level> Levels
        {
            get;
            private set;
        }
    }
}
