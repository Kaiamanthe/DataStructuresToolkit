using System;
using System.Linq;
using NUnit.Framework;
using DataStructuresToolkit;

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class TreeToolkitTests
    {
        private static TreeNode T() => TreeNode.BuildTeachingTree(); // teaching tree
        private static int[] Seq(params int[] xs) => xs;              // terse array literal

        // TreeNode Traversals

        [Test]
        public void Inorder_TeachingTree_MatchesExpected()
        {
            // Arrange
            var root = T();
            var expected = Seq(3, 27, 9, 38, 43);

            // Act
            var actual = TreeNode.Inorder(root).ToArray();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Inorder_Empty_YieldsEmpty()
        {
            // Arrange
            TreeNode root = null;

            // Act
            var actual = TreeNode.Inorder(root).ToArray();

            // Assert
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public void Preorder_TeachingTree_MatchesExpected()
        {
            // Arrange
            var root = T();
            var expected = Seq(38, 27, 3, 9, 43);

            // Act
            var actual = TreeNode.Preorder(root).ToArray();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Preorder_Leaf_JustRoot()
        {
            // Arrange
            var root = new TreeNode(7);
            var expected = Seq(7);

            // Act
            var actual = TreeNode.Preorder(root).ToArray();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Postorder_TeachingTree_MatchesExpected()
        {
            // Arrange
            var root = T();
            var expected = Seq(3, 9, 27, 43, 38);

            // Act
            var actual = TreeNode.Postorder(root).ToArray();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Postorder_RightSkewed_VisitsLeftRightRootOrder()
        {
            // Arrange
            var root = new TreeNode(1, null,
                        new TreeNode(2, null,
                        new TreeNode(3)));
            var expected = Seq(3, 2, 1);

            // Act
            var actual = TreeNode.Postorder(root).ToArray();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        // TreeNode Metrics

        [Test]
        public void Height_TeachingTree_UsesEdgesConvention()
        {
            // Arrange
            var root = T();

            // Act
            int h = TreeNode.Height(root);

            // Assert
            // Teaching tree has 3 levels of nodes, height (edges) = 2
            Assert.That(h, Is.EqualTo(2));
        }

        [Test]
        public void Height_Empty_IsMinusOne()
        {
            // Arrange
            TreeNode root = null;

            // Act
            int h = TreeNode.Height(root);

            // Assert
            Assert.That(h, Is.EqualTo(-1));
        }

        [Test]
        public void Depth_FindsExistingValue()
        {
            // Arrange
            var root = T();

            // Act
            int dRoot = TreeNode.Depth(root, 38);
            int dGrandchild = TreeNode.Depth(root, 9);

            // Assert
            Assert.That(dRoot, Is.EqualTo(0));      // root
            Assert.That(dGrandchild, Is.EqualTo(2)); // grandchild
        }

        [Test]
        public void Depth_MissingValue_IsMinusOne()
        {
            // Arrange
            var root = T();

            // Act
            int d = TreeNode.Depth(root, 999);

            // Assert
            Assert.That(d, Is.EqualTo(-1));
        }

        // BST Behavior

        [Test]
        public void Bst_Insert_AllUniqueValues_ThenInorderIsSorted()
        {
            // Arrange
            var bst = new Bst();
            int[] vals = { 50, 30, 70, 20, 40, 60, 80 };
            var expected = vals.OrderBy(x => x).ToArray();

            // Act
            foreach (var v in vals) Assert.That(bst.Insert(v), Is.True, $"Insert failed for {v}");
            var inorder = bst.Inorder().ToArray();

            // Assert
            Assert.That(inorder, Is.EqualTo(expected));
        }

        [Test]
        public void Bst_Insert_Duplicate_ReturnsFalseAndDoesNotChangeOrder()
        {
            // Arrange
            var bst = new Bst();
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
        public void Bst_Contains_FindsPresent_AndRejectsMissing()
        {
            // Arrange
            var bst = new Bst();
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
        public void Bst_Contains_OnEmpty_IsFalse()
        {
            // Arrange
            var bst = new Bst();

            // Act
            bool found = bst.Contains(123);

            // Assert
            Assert.That(found, Is.False);
        }

        [Test]
        public void Bst_Height_EmptyIsMinusOne_SingleNodeIsZero()
        {
            // Arrange
            var bst = new Bst();

            // Act
            int hEmpty = bst.Height();
            bst.Insert(1);
            int hSingle = bst.Height();

            // Assert
            Assert.That(hEmpty, Is.EqualTo(-1));
            Assert.That(hSingle, Is.EqualTo(0));
        }

        [Test]
        public void Bst_Height_SkewedIsTallerThanBalancedOnSameValues()
        {
            // Arrange
            var skewed = new Bst();
            foreach (var v in new[] { 10, 20, 30, 40, 50 }) skewed.Insert(v);

            var balanced = new Bst();
            foreach (var v in new[] { 30, 20, 40, 10, 50 }) balanced.Insert(v);

            // Act
            int hSkewed = skewed.Height();
            int hBalanced = balanced.Height();

            // Assert
            Assert.That(hSkewed, Is.GreaterThan(hBalanced));
        }
    }
}
