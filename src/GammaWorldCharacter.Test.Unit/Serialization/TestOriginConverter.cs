using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Serialization;
using GammaWorldCharacter.Test.Unit.Origins;
using NUnit.Framework;
using Newtonsoft.Json;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    [TestFixture]
    public class TestOriginConverter
    {
        [Test]
        [TestCaseSource("TestSerializationSource")]
        public string TestSerialization(Origin origin)
        {
            JsonSerializer jsonSerializer;

            jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new OriginConverter());
            using (StringWriter stringWriter = new StringWriter())
            {
                jsonSerializer.Serialize(stringWriter, origin);
                stringWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public IEnumerable<TestCaseData> TestSerializationSource()
        {
            yield return new TestCaseData(new Android()).Returns(QuoteString(OriginConverter.AndroidOriginName));
            yield return new TestCaseData(new Cockroach()).Returns(QuoteString(OriginConverter.CockroachOriginName));
            yield return new TestCaseData(new Doppelganger()).Returns(QuoteString(OriginConverter.DoppelgangerOriginName));
            yield return new TestCaseData(new Electrokinetic()).Returns(QuoteString(OriginConverter.ElectrokineticOriginName));
            yield return new TestCaseData(new Empath()).Returns(QuoteString(OriginConverter.EmpathOriginName));
            yield return new TestCaseData(new Felinoid()).Returns(QuoteString(OriginConverter.FelinoidOriginName));
            yield return new TestCaseData(new Giant()).Returns(QuoteString(OriginConverter.GiantOriginName));
            yield return new TestCaseData(new GravityController()).Returns(QuoteString(OriginConverter.GravityControllerOriginName));
            yield return new TestCaseData(new Hawkoid()).Returns(QuoteString(OriginConverter.HawkoidOriginName));
            yield return new TestCaseData(new Hypercognitive()).Returns(QuoteString(OriginConverter.HypercognitiveOriginName));
            yield return new TestCaseData(new NullOrigin()).Throws(typeof(ArgumentException));
        }

        [Test]
        [TestCaseSource("TestDeserializationSource")]
        public Origin TestDeserialization(string json)
        {
            JsonSerializer jsonSerializer;

            jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new OriginConverter());
            using (StringReader stringReader = new StringReader(json))
            {
                return (Origin) jsonSerializer.Deserialize(stringReader, typeof(Origin));
            }
        }

        public IEnumerable<TestCaseData> TestDeserializationSource()
        {
            yield return new TestCaseData(QuoteString(OriginConverter.AndroidOriginName)).Returns(new Android());
            yield return new TestCaseData(QuoteString(OriginConverter.CockroachOriginName)).Returns(new Cockroach());
            yield return new TestCaseData(QuoteString(OriginConverter.DoppelgangerOriginName)).Returns(new Doppelganger());
            yield return new TestCaseData(QuoteString(OriginConverter.ElectrokineticOriginName)).Returns(new Electrokinetic());
            yield return new TestCaseData(QuoteString(OriginConverter.EmpathOriginName)).Returns(new Empath());
            yield return new TestCaseData(QuoteString(OriginConverter.FelinoidOriginName)).Returns(new Felinoid());
            yield return new TestCaseData(QuoteString(OriginConverter.GiantOriginName)).Returns(new Giant());
            yield return new TestCaseData(QuoteString(OriginConverter.GravityControllerOriginName)).Returns(new GravityController());
            yield return new TestCaseData(QuoteString(OriginConverter.HawkoidOriginName)).Returns(new Hawkoid());
            yield return new TestCaseData(QuoteString(OriginConverter.HypercognitiveOriginName)).Returns(new Hypercognitive());
            yield return new TestCaseData(null).Throws(typeof(ArgumentNullException));
        }

        [Test]
        public void TestCanConvert_NullObject()
        {
            Assert.That(() => new OriginConverter().CanConvert(null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("objectType"));
        }

        [Test]
        public void TestWriteJson_NullWriter()
        {
            Assert.That(() => new OriginConverter().WriteJson(null, new object(), new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("writer"));
        }

        [Test]
        public void TestWriteJson_NullObject()
        {
            Assert.That(() => new OriginConverter().WriteJson(new JsonTextWriter(new StringWriter()), null, new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("value"));
        }

        [Test]
        public void TestWriteJson_NullSerializer()
        {
            Assert.That(() => new OriginConverter().WriteJson(new JsonTextWriter(new StringWriter()), new object(), null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("serializer"));
        }

        [Test]
        public void TestReadJson_NullWriter()
        {
            Assert.That(() => new OriginConverter().ReadJson(null, typeof(object), null, new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("reader"));
        }

        [Test]
        public void TestReadJson_NullObjectType()
        {
            Assert.That(() => new OriginConverter().ReadJson(new JsonTextReader(new StringReader(string.Empty)), null, new object(), new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("objectType"));
        }

        [Test]
        public void TestReadJson_NullSerializer()
        {
            Assert.That(() => new OriginConverter().ReadJson(new JsonTextReader(new StringReader(string.Empty)), typeof(object), new object(), null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("serializer"));
        }

        private static string QuoteString(string str)
        {
            return "\"" + str + "\"";
        }
    }
}
