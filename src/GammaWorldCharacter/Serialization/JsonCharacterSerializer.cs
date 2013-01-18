using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using System.Resources;

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
                characterJsonData.AbilityScores[ScoreTypeHelper.ToString(abilityScore)] = 
                    character[abilityScore].Total;
            }
            characterJsonData.PrimaryOrigin = character.PrimaryOrigin.GetType().FullName;
            characterJsonData.SecondaryOrigin = character.SecondaryOrigin.GetType().FullName;

            return JsonConvert.SerializeObject(characterJsonData);
        }

        /// <summary>
        /// The JSON Schema describing the serialized characters.
        /// </summary>
        public string Schema
        {
            get
            {
                // TODO: Cache this for efficiency
                using (Stream stream = 
                    Assembly.GetExecutingAssembly().GetManifestResourceStream(
                        "GammaWorldCharacter.Serialization.CharacterSchema.json"))
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
