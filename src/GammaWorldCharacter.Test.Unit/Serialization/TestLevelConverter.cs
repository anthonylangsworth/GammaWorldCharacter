using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Levels;
using GammaWorldCharacter.Serialization;
using NUnit.Framework;
using Newtonsoft.Json;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    [TestFixture]
    public class TestLevelConverter : TestConverter<LevelConverter>
    {
        [Test]
        [TestCaseSource("TestSerializationSource")]
        public string TestSerialization(Level level)
        {
            JsonSerializer jsonSerializer;

            jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new LevelConverter());
            using (StringWriter stringWriter = new StringWriter())
            {
                jsonSerializer.Serialize(stringWriter, level);
                stringWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }            
        }

        public IEnumerable<TestCaseData> TestSerializationSource()
        {
            yield return new TestCaseData(new Level02(OriginChoice.Primary)).Returns("{\"level\":2,\"criticalHitBenefitOrigin\":\"Primary\"}");
            yield return new TestCaseData(new Level02(OriginChoice.Secondary)).Returns("{\"level\":2,\"criticalHitBenefitOrigin\":\"Secondary\"}");
            yield return new TestCaseData(new Level03(OriginChoice.Primary)).Returns("{\"level\":3,\"utilityPowerOrigin\":\"Primary\"}");
            yield return new TestCaseData(new Level03(OriginChoice.Secondary)).Returns("{\"level\":3,\"utilityPowerOrigin\":\"Secondary\"}");
            yield return new TestCaseData(null).Returns("null");
        }

        [Test]
        [TestCaseSource("TestDeserializationSource")]
        public Level TestDeserialization(string json)
        {
            JsonSerializer jsonSerializer;

            jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new LevelConverter());
            using (StringReader stringReader = new StringReader(json))
            {
                return (Level)jsonSerializer.Deserialize(stringReader, typeof(Level));
            }
        }

        public IEnumerable<TestCaseData> TestDeserializationSource()
        {
            yield return new TestCaseData(("{\"level\":2,\"criticalHitBenefitOrigin\":\"Primary\"}")).Returns(new Level02(OriginChoice.Primary));
            yield return new TestCaseData(("{\"level\":2,\"criticalHitBenefitOrigin\":\"Secondary\"}")).Returns(new Level02(OriginChoice.Secondary));
            yield return new TestCaseData(("{\"level\":3,\"utilityPowerOrigin\":\"Primary\"}")).Returns(new Level03(OriginChoice.Primary));
            yield return new TestCaseData(("{\"level\":3,\"utilityPowerOrigin\":\"Secondary\"}")).Returns(new Level03(OriginChoice.Secondary));
            yield return new TestCaseData(("{\"level\":null}")).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData(("{\"level\":\"one\"}")).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData(("{\"level\":1}")).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData(("{\"level\":2}")).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData(("{\"level\":2,\"criticalHitBenefitOrigin\":\"Tertiary\"}")).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData(("{\"level\":3}")).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData(("{\"level\":3,\"utilityPowerOrigin\":\"Tertiary\"}")).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData("null").Returns(null);
        }
    }
}
