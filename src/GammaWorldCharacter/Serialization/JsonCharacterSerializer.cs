using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Levels;
using GammaWorldCharacter.Origins;
using Newtonsoft.Json;
using System.Resources;
using Newtonsoft.Json.Schema;

namespace GammaWorldCharacter.Serialization
{
    /// <summary>
    /// A serializer
    /// </summary>
    public class JsonCharacterSerializer
    {
        /// <summary>
        /// Serialize the given <see cref="Character"/> to JSON.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to serialize. This cannot be null.
        /// </param>
        /// <returns>
        /// The character as JSON.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="character"/> cannot be null.
        /// </exception>
        public string Serialize(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            CharacterJsonData characterJsonData;

            // Serialize the base character
            characterJsonData = new CharacterJsonData
            {
                PrimaryOrigin = character.PrimaryOrigin,
                SecondaryOrigin = character.SecondaryOrigin,
                TrainedSkill = character.TrainedSkill,
                Name = character.Name,
                PlayerName = character.PlayerName
            };
            foreach (ScoreType abilityScore in ScoreTypeHelper.AbilityScores)
            {
                characterJsonData.AbilityScores[abilityScore] =
                    character[abilityScore].Total;
            }

            // Serialize levels
            foreach (Level level in character.Levels)
            {
                characterJsonData.Levels.Add(LevelJsonData.FromLevel(level));
            }

            // Serialize gear
            characterJsonData.MainHand = ItemJsonData.FromItem(character.GetHeldItem<Item>(Hand.Main));
            characterJsonData.OffHand = ItemJsonData.FromItem(character.GetHeldItem<Item>(Hand.Off));
            foreach (Slot slot in Enum.GetValues(typeof (Slot)))
            {
                if (character.GetEquippedItem<Item>(slot) != null)
                {
                    characterJsonData.EquippedGear[slot] = ItemJsonData.FromItem(character.GetEquippedItem<Item>(slot));
                }
            }
            foreach (Item item in character.Gear)
            {
                characterJsonData.OtherGear.Add(ItemJsonData.FromItem(item));
            }

            return JsonConvert.SerializeObject(characterJsonData, Formatting.Indented);
        }

        /// <summary>
        /// Create a <see cref="Character"/> from serialized JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Character Deserialize(string json)
        {
            if (string.IsNullOrEmpty("json"))
            {
                throw new ArgumentNullException("json");
            }

            CharacterJsonData characterJsonData;
            Character result;
            Origin primaryOrigin;
            Origin secondaryOrigin;
            IEnumerable<int> abilityScores;

            characterJsonData = JsonConvert.DeserializeObject<CharacterJsonData>(json);

            // Cull out origin provided ability scores
            primaryOrigin = characterJsonData.PrimaryOrigin;
            secondaryOrigin = characterJsonData.SecondaryOrigin;
            abilityScores = characterJsonData.AbilityScores.Where(x =>
                                                                  x.Key != primaryOrigin.AbilityScore &&
                                                                  x.Key != secondaryOrigin.AbilityScore)
                                             .Select(x => x.Value);

            // Create the new base character
            result = new Character(abilityScores, primaryOrigin, secondaryOrigin, characterJsonData.TrainedSkill)
                {
                    Name = characterJsonData.Name,
                    PlayerName = characterJsonData.PlayerName
                };

            // Deserialize levels
            if (characterJsonData.Levels.Any())
            {
                result.AddLevels(characterJsonData.Levels.OrderBy(x => x.Number).Select(x => x.ToLevel()).ToArray());
            }

            // Deserialize gear
            result.SetHeldItem(Hand.Main, characterJsonData.MainHand != null ? characterJsonData.MainHand.ToItem() : null);
            result.SetHeldItem(Hand.Off, characterJsonData.OffHand != null ? characterJsonData.OffHand.ToItem() : null);
            foreach (ItemJsonData itemJsonData in characterJsonData.EquippedGear.Values)
            {
                result.SetEquippedItem(itemJsonData.ToItem());
            }
            result.Gear.AddRange(characterJsonData.OtherGear.Select(x => x.ToItem()));

            return result;
        }
    }
}
