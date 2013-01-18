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
                characterJsonData.AbilityScores[ScoreTypeHelper.ToString(abilityScore).ToLower()] = 
                    character[abilityScore].Total;
            }
            characterJsonData.PrimaryOrigin = character.PrimaryOrigin.GetType().FullName;
            characterJsonData.SecondaryOrigin = character.SecondaryOrigin.GetType().FullName;

            return JsonConvert.SerializeObject(characterJsonData, Formatting.Indented);
        }

        /// <summary>
        /// The JSON Schema describing the serialized characters.
        /// </summary>
        public string Schema
        {
            get
            {
                // TODO: Cache this for efficiency
                //using (Stream stream = 
                //    Assembly.GetExecutingAssembly().GetManifestResourceStream(
                //        "GammaWorldCharacter.Serialization.CharacterSchema.json"))
                //using (StreamReader streamReader = new StreamReader(stream))
                //{
                //    return streamReader.ReadToEnd();
                //}

                return @"{
    '$schema': 'http://json-schema.org/draft-04/schema#',
    'description': 'Gamma World Character',
    'type': 'object',
    'properties': {
        'name': {
            'type': 'string'
        },
        'playerName': {
            'type': 'string'
        },
        'primaryOriginType': {
            'type': 'string'
        },
        'secondaryOriginType': {
            'type': 'string'
        },
        'abilityScores': {
            'type': 'object',
            'items': {
				'strength': {
                    'type': 'number',
                    'minimum': 3,
                    'maximum': 20
                },
				'constitution': {
                    'type': 'number',
                    'minimum': 3,
                    'maximum': 20
                },
				'dexterity': {
                    'type': 'number',
                    'minimum': 3,
                    'maximum': 20
                },
				'intelligence': {
                    'type': 'number',
                    'minimum': 3,
                    'maximum': 20
                },
				'wisdom': {
                    'type': 'number',
                    'minimum': 3,
                    'maximum': 20
                },
				'charisma': {
                    'type': 'number',
                    'minimum': 3,
                    'maximum': 20
                }
            },
            'minItems': 6,
            'maxItems': 6,
            'uniqueItems': true
        }
    }
}";
            }
        }
    }
}
