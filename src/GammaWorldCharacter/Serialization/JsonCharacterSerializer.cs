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
                characterJsonData.Levels.Add(level);
            }

            // Serialize gear
            characterJsonData.MainHand = character.GetHeldItem<Item>(Hand.Main);
            characterJsonData.OffHand = character.GetHeldItem<Item>(Hand.Off);
            foreach (Slot slot in Enum.GetValues(typeof (Slot)))
            {
                if (character.GetEquippedItem<Item>(slot) != null)
                {
                    characterJsonData.EquippedGear[slot] = character.GetEquippedItem<Item>(slot);
                }
            }
            foreach (Item item in character.Gear)
            {
                characterJsonData.OtherGear.Add(item);
            }
            
            // Include the ItemConverter here as there is no way to specify the
            // converter for collections as attributes.
            return JsonConvert.SerializeObject(characterJsonData, Formatting.Indented, 
                new ItemConverter(), new LevelConverter());
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

            // Include the ItemConverter here as there is no way to specify the
            // converter for collections as attributes.
            characterJsonData = JsonConvert.DeserializeObject<CharacterJsonData>(json, 
                new ItemConverter(), new LevelConverter());

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
                result.AddLevels(characterJsonData.Levels.OrderBy(x => x.Number).ToArray());
            }

            // Deserialize gear
            result.SetHeldItem(Hand.Main, characterJsonData.MainHand);
            result.SetHeldItem(Hand.Off, characterJsonData.OffHand);
            foreach (Item item in characterJsonData.EquippedGear.Values)
            {
                result.SetEquippedItem(item);
            }
            result.Gear.AddRange(characterJsonData.OtherGear);

            return result;
        }

        /*
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
                    'enum': [ 'Acrobatics',
                        'Athletics',
                        'Conspiracy',
                        'Insight',
                        'Interaction',
                        'Nature',
                        'Mechanics',
                        'Perception',
                        'Science',
                        'Stealth' ],
                    'required': true
                },
                'abilityScores': {
                    'type': 'object',
                    'required': true,
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
                },
                'mainHand': {
                    'extends': {'$ref': 'item'}
                },
                'offHand': {
                    'extends': [ 
                        {'$ref': 'item'}, 
                        {'$ref': 'weapon'},
                        {'$ref': 'ranged_weapon'},
                        {'$ref': 'armor'}
                    ],
                },
                'equippedGear': {
                    'type': 'array',
                    'required': true,
                    'extends': [ 
                        'body': {
                            'type':  {'$ref': 'armor'}
                        },
                    ],
                },
                'otherGear': {
                    'type': 'array',
                    'required': true,
                    'extends': [ 
                        {'$ref': 'item'}, 
                        {'$ref': 'weapon'},
                        {'$ref': 'ranged_weapon'},
                        {'$ref': 'armor'}
                    ],
                },
            }
        },
        'item': {
            'id': 'item',
            'type': 'object',
            'properties': {
                '$type': {
                    'type': 'string',
                    'value': 'item'
                },
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
            'id': 'weapon',
            'extends': { '$ref': 'item' },
            'type': 'object',
            'properties': {
                'weight': {
                    'type': 'string',
                    'enum': ['Heavy', 'Light'],
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
            'extends': { '$ref': 'weapon' },
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
            'extends': { '$ref': 'item' },
            'properties': {
                'type': {
                    'type': 'string',
                    'enum': ['Heavy', 'Light'],
                    'required': true
                },
            }
        }
    }
}";
            }
        }
        */
    }
}
