using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Serialization;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    [TestFixture]
    public class TestJsonCharacterSerializer
    {
        [Test]
        public void TestSerialize_NullCharacter()
        {
            Assert.That(() => new JsonCharacterSerializer().Serialize(null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("character"));
        }

        [Test]
        public void TestDeserialize_NullJson()
        {
            Assert.That(() => new JsonCharacterSerializer().Deserialize(null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("json"));
        }

    }
}
