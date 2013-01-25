using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    /// <summary>
    /// Common tests for <see cref="JsonConverter"/>s.
    /// </summary>
    /// <typeparam name="TConverter">
    /// The converter type (inherits from <see cref="JsonConverter"/> and has a
    /// parameterless constructor).
    /// </typeparam>
    /// <typeparam name="TConvertedType">
    /// The type <typeparamref name="TConverter"/> converts.
    /// </typeparam>
    public abstract class TestConverter<TConverter, TConvertedType>
        where TConverter:JsonConverter, new()
    {
        [Test]
        [TestCaseSource("TestSerializationSource")]
        public string TestSerialization(TConvertedType objectToSerialize)
        {
            JsonSerializer jsonSerializer;

            jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new TConverter());
            using (StringWriter stringWriter = new StringWriter())
            {
                jsonSerializer.Serialize(stringWriter, objectToSerialize);
                stringWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public abstract IEnumerable<TestCaseData> TestSerializationSource();

        [Test]
        [TestCaseSource("TestDeserializationSource")]
        public TConvertedType TestDeserialization(string json)
        {
            JsonSerializer jsonSerializer;

            jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new TConverter());
            using (StringReader stringReader = new StringReader(json))
            {
                return (TConvertedType)jsonSerializer.Deserialize(stringReader, typeof(TConvertedType));
            }
        }

        public abstract IEnumerable<TestCaseData> TestDeserializationSource();

        [Test]
        public void TestCanConvert_NullObject()
        {
            Assert.That(() => new TConverter().CanConvert(null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("objectType"));
        }

        [Test]
        public void TestWriteJson_NullWriter()
        {
            Assert.That(() => new TConverter().WriteJson(null, new object(), new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("writer"));
        }

        [Test]
        public void TestWriteJson_NullObject()
        {
            Assert.That(() => new TConverter().WriteJson(new JsonTextWriter(new StringWriter()), null, new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("value"));
        }

        [Test]
        public void TestWriteJson_NullSerializer()
        {
            Assert.That(() => new TConverter().WriteJson(new JsonTextWriter(new StringWriter()), new object(), null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("serializer"));
        }

        [Test]
        public void TestReadJson_NullWriter()
        {
            Assert.That(() => new TConverter().ReadJson(null, typeof(object), null, new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("reader"));
        }

        [Test]
        public void TestReadJson_NullObjectType()
        {
            Assert.That(() => new TConverter().ReadJson(new JsonTextReader(new StringReader(string.Empty)), null, new object(), new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("objectType"));
        }

        [Test]
        public void TestReadJson_NullSerializer()
        {
            Assert.That(() => new TConverter().ReadJson(new JsonTextReader(new StringReader(string.Empty)), typeof(object), new object(), null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("serializer"));
        }
    }
}
