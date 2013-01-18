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
            characterJsonData.TrainedSkill = character.TrainedSkill.ToString();

            return JsonConvert.SerializeObject(characterJsonData, Formatting.Indented);
        }

        /// <summary>
        /// The JSON Schema describing the serialized characters.
        /// </summary>
        public string Schema
        {
            get
            {
                return @"{
    '$schema': 'http://json-schema.org/draft-04/schema#',
    'properties': {
        'gamma_world_character': {
            'id': 'gamma_world_character',
            'type': 'object',
            'properties': {
                'name': {
                    'type': 'string',
                    'required': true
                },
                'playerName': {
                    'type': 'string',
                    'required': true
                },
                'primaryOriginType': {
                    'type': 'string',
                    'required': true
                },
                'secondaryOriginType': {
                    'type': 'string',
                    'required': true
                },
                'trainedSkill': {
                    'type': 'string',
                    'required': true
                },
                'abilityScores': {
                    'type': 'object',
                    'items': {
				        'strength': {
                            'type': 'number',
                            'minimum': 3,
                            'maximum': 20,
                            'required': true
                        },
				        'constitution': {
                            'type': 'number',
                            'minimum': 3,
                            'maximum': 20,
                            'required': true
                        },
				        'dexterity': {
                            'type': 'number',
                            'minimum': 3,
                            'maximum': 20,
                            'required': true
                        },
				        'intelligence': {
                            'type': 'number',
                            'minimum': 3,
                            'maximum': 20,
                            'required': true
                        },
				        'wisdom': {
                            'type': 'number',
                            'minimum': 3,
                            'maximum': 20,
                            'required': true
                        },
				        'charisma': {
                            'type': 'number',
                            'minimum': 3,
                            'maximum': 20,
                            'required': true
                        }
                    },
                    'mainHand': {
                        'type': [ 
                            {'$ref': 'item'}, 
                            {'$ref': 'melee_weapon'},
                            {'$ref': 'ranged_weapon'},
                            {'$ref': 'armor'}
                        ]
                    },
                    'offHand': {
                        'type': [ 
                            {'$ref': 'item'}, 
                            {'$ref': 'melee_weapon'},
                            {'$ref': 'ranged_weapon'},
                            {'$ref': 'armor'}
                        ]
                    }
                },
            }
        },
        'item': {
            'id': 'item',
            'type': 'object',
            'properties': {
                'name': {
                    'type': 'string',
                    'required': true
                },
                'slot': {
                    'type': 'string',
                    'required': true
                }
            }
        },
        'melee_weapon': {
            'id': 'melee_weapon',
            'type': 'object',
            'properties': {
                'weight': {
                    'type': 'string',
                    'enum': ['heavy', 'light'],
                    'required': true
                },
                'hands': {
                    'type': 'number',
                    'minimum': 1,
                    'maximum': 2,
                    'required': true
                },
            }
        },
        'ranged_weapon': {
            'id': 'ranged_weapon',
            'type': 'object',
            'extends': { '$ref': 'melee_weapon' },
            'properties': {
                'type': {
                    'type': 'string',
                    'enum': ['gun', 'weapon'],
                    'required': true
                }
            }
        },
        'armor': {
            'id': 'armor',
            'type': 'object',
            'properties': {
                'weight': {
                    'type': 'string',
                    'enum': ['heavy', 'light', 'shield'],
                    'required': true
                },
            }
        }
    }
}";
            }
        }
    }
}
