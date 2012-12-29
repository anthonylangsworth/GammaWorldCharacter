using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacterGenerator.Collections;
using NUnit.Framework;

namespace GammaWorldCharacterGenerator.Test.Unit.Collections
{
    [TestFixture]
    public class TestGraph
    {
        [Test]
        public void Test_Creation_Null()
        {
            Assert.That(() => new Graph<string>(null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("data"));
        }

        [Test]
        public void Test_Creation_Contains()
        {
            Assert.That(() => new Graph<string>(string.Empty).Contains(string.Empty),
                Is.True);
        }

        [Test]
        public void Test_AddChild_NullParent()
        {
            Assert.That(() => new Graph<string>(string.Empty).AddChild(null, "1"),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("parent"));
        }

        [Test]
        public void Test_AddChild_NullChild()
        {
            Assert.That(() => new Graph<string>(string.Empty).AddChild("1", null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("child"));
        }

        [Test]
        public void Test_AddChild_IsChild()
        {
            Graph<string> graph = new Graph<string>(string.Empty);
            graph.AddChild(string.Empty, "1");
            Assert.That(graph.IsChild(string.Empty, "1"),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("child"));
        }

        [Test]
        public void Test_AddChild_Contains()
        {
            Graph<string> graph = new Graph<string>(string.Empty);
            graph.AddChild(string.Empty, "1");
            Assert.That(graph.IsChild(string.Empty, "1"),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("child"));
        }

    }
}
