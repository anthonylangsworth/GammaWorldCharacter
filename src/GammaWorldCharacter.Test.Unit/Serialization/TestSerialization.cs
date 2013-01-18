using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Samples;
using GammaWorldCharacter.Serialization;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    [TestFixture]
    public class TestSerialization
    {
        [Test]
        public void TestJsonSerialization()
        {
            JsonCharacterSerializer characterSerializer;
            StringBuilder stringBuilder;
            IList<string> messages;
            JObject jObject;
            JsonSchema jsonSchema;
            string json;

            // TODO: Pass this in as a test
            Character character;
            character = Level01Characters.Clip;

            characterSerializer = new JsonCharacterSerializer();
            json = characterSerializer.Serialize(character);
            jObject = JObject.Parse(json);

            jsonSchema = JsonSchema.Parse(characterSerializer.Schema);
            
            if (!jObject.IsValid(jsonSchema, out messages))
            {
                stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("{0} Level {1} {2} {3}:\n\n",
                    character.Name, character.Level, 
                    character.PrimaryOrigin.Name, character.SecondaryOrigin.Name);
                stringBuilder.AppendFormat("{0}\n", NumberLines(json));
                if (messages.Any())
                {
                    stringBuilder.Append("\nErrors:\n");
                    foreach (string message in messages)
                    {
                        stringBuilder.Append(message);
                        stringBuilder.AppendLine();
                    }
                }

                Assert.Fail(stringBuilder.ToString());
            }
        }

        public string NumberLines(string text)
        {
            int i = 1;
            return string.Join("\n", text.Split('\n').Select(x => i++.ToString() + ' ' + x));
        }
    }
}
