using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GammaWorldCharacter.Gear;
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

            characterJsonData = new CharacterJsonData();
            characterJsonData.Name = character.Name;
            characterJsonData.PlayerName = character.PlayerName;
            foreach (ScoreType abilityScore in ScoreTypeHelper.AbilityScores)
            {
                characterJsonData.AbilityScores[abilityScore] = 
                    character[abilityScore].Total;
            }
            characterJsonData.PrimaryOrigin = character.PrimaryOrigin.GetType();
            characterJsonData.SecondaryOrigin = character.SecondaryOrigin.GetType();
            characterJsonData.TrainedSkill = character.TrainedSkill;

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

            return JsonConvert.SerializeObject(characterJsonData, new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto
                });
        }

        /// <summary>
        /// Create a <see cref="Character"/> from serialized JSON.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Character Deserialize(string json)
        {
            if(string.IsNullOrEmpty("json"))
            {
                throw new ArgumentNullException("json");
            }

            CharacterJsonData characterJsonData;
            Character result;

            characterJsonData = JsonConvert.DeserializeObject<CharacterJsonData>(json);

            Origin primaryOrigin;
            Origin secondaryOrigin;
            IEnumerable<int> abilityScores;

            // Load the origins
            primaryOrigin = 
                characterJsonData.PrimaryOrigin.GetConstructor(new Type[0]) != null?
                    characterJsonData.PrimaryOrigin.GetConstructor(new Type[0]).Invoke(new object[0]) as Origin :
                    null;
            if (primaryOrigin == null)
            {
                throw new ArgumentException("Primary origin does not exist or is not an origin.");
            }
            secondaryOrigin =
                characterJsonData.SecondaryOrigin.GetConstructor(new Type[0]) != null ?
                    characterJsonData.SecondaryOrigin.GetConstructor(new Type[0]).Invoke(new object[0]) as Origin :
                    null;
            if (secondaryOrigin == null)
            {
                throw new ArgumentException("Secondary origin does not exist or is not an origin.");
            }

            // Cull out origin provided ability scores
            abilityScores = characterJsonData.AbilityScores.Where(x =>
                    x.Key != primaryOrigin.AbilityScore && x.Key != secondaryOrigin.AbilityScore).Select(x => x.Value);

            result = new Character(abilityScores, primaryOrigin, secondaryOrigin, characterJsonData.TrainedSkill);
            result.Name = characterJsonData.Name;
            result.PlayerName = characterJsonData.PlayerName;

            // Serialize gear
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
