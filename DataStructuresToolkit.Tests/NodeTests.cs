using DataStructuresToolkit;
using NUnit.Framework;

namespace DataStructuresToolkit.Tests
{
    public class NodeTests
    {


        [Test]
        public void DefaultCon_InitChildrenToNull_AndValueToDefault()
        {
            // Act
            var node = new Node();

            // Assert
            Assert.That(node.Value, Is.EqualTo(0), "Default int value should be 0.");
            Assert.That(node.Left, Is.Null, "Left child should default to null.");
            Assert.That(node.Right, Is.Null, "Right child should default to null.");
        }

        [Test]
        public void ValCon_SetsValue_ChildrenDefaultToNull()
        {
            // Arrange
            int value = 42;

            // Act
            var node = new Node(value);

            // Assert
            Assert.That(node.Value, Is.EqualTo(value));
            Assert.That(node.Left, Is.Null);
            Assert.That(node.Right, Is.Null);
        }

        [Test]
        public void FullCon_SetsValueAndChildren()
        {
            // Arrange
            var left = new Node(1);
            var right = new Node(2);

            // Act
            var root = new Node(10, left, right);

            // Assert
            Assert.That(root.Value, Is.EqualTo(10));
            Assert.That(root.Left, Is.SameAs(left));
            Assert.That(root.Right, Is.SameAs(right));
        }

        [Test]
        public void Prop_AreMutable_AfterCon()
        {
            // Arrange
            var node = new Node(5);

            // Act
            node.Value = 7;
            node.Left = new Node(3);
            node.Right = new Node(9);

            // Assert
            Assert.That(node.Value, Is.EqualTo(7));
            Assert.That(node.Left, Is.Not.Null);
            Assert.That(node.Left.Value, Is.EqualTo(3));
            Assert.That(node.Right, Is.Not.Null);
            Assert.That(node.Right.Value, Is.EqualTo(9));
        }

        [Test]
        public void CanBuildSimpleThreeNodeChain_WithCon()
        {
            // Arrange & Act
            var leftChild = new Node(1);
            var rightChild = new Node(3);
            var root = new Node(2, leftChild, rightChild);

            // Assert (basic BST shape check)
            Assert.That(root.Value, Is.EqualTo(2));
            Assert.That(root.Left, Is.SameAs(leftChild));
            Assert.That(root.Right, Is.SameAs(rightChild));
            Assert.That(root.Left.Value, Is.LessThan(root.Value));
            Assert.That(root.Right.Value, Is.GreaterThan(root.Value));
        }
    }
}
