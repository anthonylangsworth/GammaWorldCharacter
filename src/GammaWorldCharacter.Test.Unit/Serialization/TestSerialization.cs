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

            characterSerializer = new JsonCharacterSerializer();
            string json = characterSerializer.Serialize(Level01Characters.Clip);

            JsonSchema jsonSchema;

            jsonSchema = JsonSchema.Parse(characterSerializer.Schema);
            JObject jObject;

            IList<string> messages;
            
            jObject = JObject.Parse(json);
            jObject.IsValid(jsonSchema, out messages);

            Assert.That(messages, Is.EquivalentTo(new string[0]));
        }
    }
}
