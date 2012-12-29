using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter.Collections;

namespace GammaWorldCharacter.Test.Unit.Collections
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class TestGraphNode
    {
        [Test]
        public void Test_Creation_NoChildren()
        {
            GraphNode<string> graphNode = new GraphNode<string>(string.Empty);
            Assert.That(graphNode.Children, Is.Empty);
        }

        [Test]
        public void Test_Creation_NullData()
        {
            Assert.That(() => new GraphNode<string>(null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("data"));
        }

        [Test]
        public void Test_Creation_NullComparer()
        {
            Assert.That(() => new GraphNode<string>(string.Empty, null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("comparer"));
        }

        [Test]
        public void Test_AddParent_NullParent()
        {
            Assert.That(() => new GraphNode<string>(string.Empty).AddParent(null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("graphNode"));
        }

        [Test]
        public void Test_AddChild_NullChild()
        {
            Assert.That(() => new GraphNode<string>(string.Empty).AddChild(null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("graphNode"));
        }

        [Test]
        public void Test_AddChild_Self()
        {
            GraphNode<string> graphNode = new GraphNode<string>(string.Empty);
            Assert.That(() => graphNode.AddChild(graphNode),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("graphNode"));
        }

        [Test]
        public void Test_AddChild_CircularReferenceOneLevel()
        {
            GraphNode<string> parent = new GraphNode<string>("parent");
            GraphNode<string> child = new GraphNode<string>("child");
            parent.AddChild(child);
            Assert.That(() => child.AddChild(parent),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("graphNode"));
        }

        [Test]
        public void Test_AddChild_CircularReferenceTwoLevels()
        {
            GraphNode<string> grandParent = new GraphNode<string>("grandParent");
            GraphNode<string> parent = new GraphNode<string>("parent");
            GraphNode<string> child = new GraphNode<string>("child");
            grandParent.AddChild(parent);
            parent.AddChild(child);
            Assert.That(() => child.AddChild(grandParent),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("graphNode"));
        }

        [Test]
        public void Test_IsChild_NoChild()
        {
            GraphNode<string> parentGraphNode = new GraphNode<string>("parent");
            GraphNode<string> randomGraphNode = new GraphNode<string>("random");

            Assert.That(!parentGraphNode.IsChild(randomGraphNode));
        }

        [Test]
        public void Test_IsChild_NullChild()
        {
            Assert.That(() => new GraphNode<string>(string.Empty).IsChild(null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("graphNode"));
        }

        [Test]
        public void Test_IsChild_Self()
        {
            GraphNode<string> graphNode = new GraphNode<string>(string.Empty);

            Assert.That(!graphNode.IsChild(graphNode));
        }

        [Test]
        public void Test_IsChild_ParentsChild()
        {
            GraphNode<string> parentGraphNode = new GraphNode<string>("parent");
            GraphNode<string> childGraphNode = new GraphNode<string>("child");
            parentGraphNode.AddChild(childGraphNode);

            Assert.That(parentGraphNode.IsChild(childGraphNode));
        }

        [Test]
        public void Test_IsDescendant_Self()
        {
            GraphNode<string> graphNode = new GraphNode<string>(string.Empty);

            Assert.That(!graphNode.IsDescedant(graphNode));
        }

        [Test]
        public void Test_IsDescendant_Child()
        {
            GraphNode<string> parentGraphNode = new GraphNode<string>("parent");
            GraphNode<string> childGraphNode = new GraphNode<string>("child");
            parentGraphNode.AddChild(childGraphNode);

            Assert.That(parentGraphNode.IsDescedant(childGraphNode));
        }

        [Test]
        public void Test_IsParent_Self()
        {
            GraphNode<string> graphNode = new GraphNode<string>(string.Empty);

            Assert.That(!graphNode.IsParent(graphNode));
        }

        [Test]
        public void Test_IsParent_NullParent()
        {
            Assert.That(() => new GraphNode<string>(string.Empty).IsParent(null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("graphNode"));
        }

        [Test]
        public void Test_IsParent_ChildsParent()
        {
            GraphNode<string> parentGraphNode = new GraphNode<string>("parent");
            GraphNode<string> childGraphNode = new GraphNode<string>("child");
            parentGraphNode.AddChild(childGraphNode);

            Assert.That(childGraphNode.IsParent(parentGraphNode));
        }

        [Test]
        public void Test_GetDepthFirstEnumerator()
        {
            GraphNode<string> rootGraphNode = new GraphNode<string>("root");
            GraphNode<string> leftParentGraphNode = new GraphNode<string>("leftParent");
            GraphNode<string> leftFirstChildGraphNode = new GraphNode<string>("leftFirstChild");
            GraphNode<string> leftSecondChildGraphNode = new GraphNode<string>("leftSecondChild");
            GraphNode<string> rightParentGraphNode = new GraphNode<string>("rightParent");
            GraphNode<string> rightChildGraphNode = new GraphNode<string>("rightChild");
            GraphNode<string> rightGrandChildGraphNode = new GraphNode<string>("rightGrandChild");

            rootGraphNode.AddChild(leftParentGraphNode);
            leftParentGraphNode.AddChild(leftFirstChildGraphNode);
            leftParentGraphNode.AddChild(leftSecondChildGraphNode);
            rootGraphNode.AddChild(rightParentGraphNode);
            rightParentGraphNode.AddChild(rightChildGraphNode);
            rightChildGraphNode.AddChild(rightGrandChildGraphNode);

            GraphNode<string>[] expected = new[]
            {
                rootGraphNode,
                leftParentGraphNode,
                leftFirstChildGraphNode,
                leftSecondChildGraphNode,
                rightParentGraphNode,
                rightChildGraphNode,
                rightGrandChildGraphNode
            };

            Assert.That(expected.Zip(rootGraphNode.GetDepthFirstEnumerator(), (x, y) => x.Equals(y)).All(x => x), Is.True);
        }
    }
}
