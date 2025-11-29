using System.Linq;
using NUnit.Framework;
using DataStructuresToolkit;

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class TreeToolkitTests
    {
        // Teaching tree builder + terse array literal helper
        private static Node TeachingTree() => TreeToolkit.BuildTeachingTree();
        private static int[] Seq(params int[] xs) => xs;

        // BuildTeachingTree

        [Test]
        public void BuildTeachingTree_ConstructsExpectedShape()
        {
            // Arrange
            // Teaching tree should look like:
            //        38
            //       /  \
            //     27    43
            //    /  \
            //   3    9

            // Act
            var root = TeachingTree();

            // Assert root
            Assert.That(root, Is.Not.Null);
            Assert.That(root.Value, Is.EqualTo(38));

            // Assert left subtree
            Assert.That(root.Left, Is.Not.Null);
            Assert.That(root.Left.Value, Is.EqualTo(27));

            Assert.That(root.Left.Left, Is.Not.Null);
            Assert.That(root.Left.Left.Value, Is.EqualTo(3));
            Assert.That(root.Left.Left.Left, Is.Null);
            Assert.That(root.Left.Left.Right, Is.Null);

            Assert.That(root.Left.Right, Is.Not.Null);
            Assert.That(root.Left.Right.Value, Is.EqualTo(9));
            Assert.That(root.Left.Right.Left, Is.Null);
            Assert.That(root.Left.Right.Right, Is.Null);

            // Assert right subtree
            Assert.That(root.Right, Is.Not.Null);
            Assert.That(root.Right.Value, Is.EqualTo(43));
            Assert.That(root.Right.Left, Is.Null);
            Assert.That(root.Right.Right, Is.Null);
        }

        // BST behavior (TreeToolkit)

        [Test]
        public void TreeToolkit_Insert_AllUniqueVal_ThenInorderIsSorted()
        {
            // Arrange
            var bst = new TreeToolkit();
            int[] vals = { 50, 30, 70, 20, 40, 60, 80 };
            var expected = vals.OrderBy(x => x).ToArray();

            // Act
            foreach (var v in vals)
                Assert.That(bst.Insert(v), Is.True, $"Insert failed for {v}");

            var inorder = bst.Inorder().ToArray();

            // Assert
            Assert.That(inorder, Is.EqualTo(expected));
        }

        [Test]
        public void TreeToolkit_Insert_Dup_ReturnsFalseAndDoesNotChangeOrder()
        {
            // Arrange
            var bst = new TreeToolkit();
            var expected = Seq(10);

            // Act
            var first = bst.Insert(10);
            var dup = bst.Insert(10);
            var inorder = bst.Inorder().ToArray();

            // Assert
            Assert.That(first, Is.True);
            Assert.That(dup, Is.False);
            Assert.That(inorder, Is.EqualTo(expected));
        }

        [Test]
        public void TreeToolkit_Contains_FindsPresent_AndRejectsMissing()
        {
            // Arrange
            var bst = new TreeToolkit();
            int[] vals = { 50, 30, 70, 20, 40, 60, 80 };
            foreach (var v in vals) bst.Insert(v);

            // Act
            bool has60 = bst.Contains(60);
            bool has25 = bst.Contains(25);

            // Assert
            Assert.That(has60, Is.True);
            Assert.That(has25, Is.False);
        }

        [Test]
        public void TreeToolkit_Contains_OnEmpty_IsFalse()
        {
            // Arrange
            var bst = new TreeToolkit();

            // Act
            bool found = bst.Contains(123);

            // Assert
            Assert.That(found, Is.False);
        }

        [Test]
        public void TreeToolkit_Height_EmptyIsMinusOne_SingleNodeIsZero()
        {
            // Arrange
            var bst = new TreeToolkit();

            // Act
            int hEmpty = bst.Height();
            bst.Insert(1);
            int hSingle = bst.Height();

            // Assert
            Assert.That(hEmpty, Is.EqualTo(-1));
            Assert.That(hSingle, Is.EqualTo(0));
        }

        [Test]
        public void TreeToolkit_Height_SkewedIsTallerThanBalancedOnSameVal()
        {
            // Arrange
            var skewed = new TreeToolkit();
            foreach (var v in new[] { 10, 20, 30, 40, 50 })
                skewed.Insert(v);

            var balanced = new TreeToolkit();
            foreach (var v in new[] { 30, 20, 40, 10, 50 })
                balanced.Insert(v);

            // Act
            int hSkewed = skewed.Height();
            int hBalanced = balanced.Height();

            // Assert
            Assert.That(hSkewed, Is.GreaterThan(hBalanced));
        }
    }
}
