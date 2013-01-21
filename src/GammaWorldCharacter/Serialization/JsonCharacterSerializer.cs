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

            IEnumerable<int> abilityScores =
                characterJsonData.AbilityScores.Where(x =>
                    x.Key != primaryOrigin.AbilityScore && x.Key != secondaryOrigin.AbilityScore).Select(x => x.Value);

            result = new Character(abilityScores, primaryOrigin, secondaryOrigin, characterJsonData.TrainedSkill);
            result.Name = characterJsonData.Name;
            result.PlayerName = characterJsonData.PlayerName;

            // Serialize gear
            result.SetHeldItem(Hand.Main, characterJsonData.MainHand.ToItem());
            result.SetHeldItem(Hand.Off, characterJsonData.OffHand.ToItem());
            foreach (ItemJsonData itemJsonData in characterJsonData.EquippedGear.Values)
            {
                result.SetEquippedItem(itemJsonData.ToItem());
            }
            result.Gear.AddRange(characterJsonData.OtherGear.Select(x => x.ToItem()));

            return result;
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
    }
}
