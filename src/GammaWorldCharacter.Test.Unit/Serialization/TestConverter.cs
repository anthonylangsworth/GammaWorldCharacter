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
    /// <typeparam name="T"></typeparam>
    public class TestConverter<T>
        where T:JsonConverter, new()
    {
        [Test]
        public void TestCanConvert_NullObject()
        {
            Assert.That(() => new T().CanConvert(null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("objectType"));
        }

        [Test]
        public void TestWriteJson_NullWriter()
        {
            Assert.That(() => new T().WriteJson(null, new object(), new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("writer"));
        }

        [Test]
        public void TestWriteJson_NullObject()
        {
            Assert.That(() => new T().WriteJson(new JsonTextWriter(new StringWriter()), null, new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("value"));
        }

        [Test]
        public void TestWriteJson_NullSerializer()
        {
            Assert.That(() => new T().WriteJson(new JsonTextWriter(new StringWriter()), new object(), null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("serializer"));
        }

        [Test]
        public void TestReadJson_NullWriter()
        {
            Assert.That(() => new T().ReadJson(null, typeof(object), null, new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("reader"));
        }

        [Test]
        public void TestReadJson_NullObjectType()
        {
            Assert.That(() => new T().ReadJson(new JsonTextReader(new StringReader(string.Empty)), null, new object(), new JsonSerializer()),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("objectType"));
        }

        [Test]
        public void TestReadJson_NullSerializer()
        {
            Assert.That(() => new T().ReadJson(new JsonTextReader(new StringReader(string.Empty)), typeof(object), new object(), null),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("serializer"));
        }
    }
}
