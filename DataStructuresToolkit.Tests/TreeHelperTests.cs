using DataStructuresToolkit;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DataStructuresToolkit.Tests;

public class TreeHelperTests
{

    // Height tests

    [Test]
    public void Height_EmptyTree_ReturnsMinusOne()
    {
        // Arrange
        Node root = null;

        // Act
        int h = TreeHelper.Height(root);

        // Assert
        Assert.That(h, Is.EqualTo(-1));
    }

    [Test]
    public void Height_SingleNode_ReturnsZero()
    {
        // Arrange
        var root = new Node(10);

        // Act
        int h = TreeHelper.Height(root);

        // Assert
        Assert.That(h, Is.EqualTo(0));
    }

    [Test]
    public void Height_SimpleThreeLevelTree_ComputesCorrectHeight()
    {
        // Arrange
        //      2
        //     /
        //    1
        //   /
        //  0
        var n0 = new Node(0);
        var n1 = new Node(1, n0, null);
        var root = new Node(2, n1, null);

        // Act
        int h = TreeHelper.Height(root);

        // Assert
        // Edges convention: root->n1 (1), n1->n0 (2) => height = 2
        Assert.That(h, Is.EqualTo(2));
    }

    // Depth tests

    [Test]
    public void Depth_FindsExistingVal_ReturnsCorrectDepth()
    {
        // Arrange
        //      2
        //     / \
        //    1   3
        var n1 = new Node(1);
        var n3 = new Node(3);
        var root = new Node(2, n1, n3);

        // Act
        int depthRoot = TreeHelper.Depth(root, 2);
        int depthLeft = TreeHelper.Depth(root, 1);
        int depthRight = TreeHelper.Depth(root, 3);

        // Assert
        Assert.That(depthRoot, Is.EqualTo(0));
        Assert.That(depthLeft, Is.EqualTo(1));
        Assert.That(depthRight, Is.EqualTo(1));
    }

    [Test]
    public void Depth_NonExistingVal_ReturnsMinusOne()
    {
        // Arrange
        //      2
        //     / \
        //    1   3
        var n1 = new Node(1);
        var n3 = new Node(3);
        var root = new Node(2, n1, n3);

        // Act
        int depth = TreeHelper.Depth(root, 99);

        // Assert
        Assert.That(depth, Is.EqualTo(-1));
    }

    // Traversal tests

    [Test]
    public void Inorder_ThreeNodeBst_ReturnsSortedOrder()
    {
        // Arrange
        //      2
        //     / \
        //    1   3
        var n1 = new Node(1);
        var n3 = new Node(3);
        var root = new Node(2, n1, n3);

        // Act
        var result = TreeHelper.Inorder(root).ToArray();

        // Assert
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
    }

    [Test]
    public void Preorder_ThreeNodeBst_ReturnsRootLeftRight()
    {
        // Arrange
        //      2
        //     / \
        //    1   3
        var n1 = new Node(1);
        var n3 = new Node(3);
        var root = new Node(2, n1, n3);

        // Act
        var result = TreeHelper.Preorder(root).ToArray();

        // Assert
        CollectionAssert.AreEqual(new[] { 2, 1, 3 }, result);
    }

    [Test]
    public void Postorder_ThreeNodeBst_ReturnsLeftRightRoot()
    {
        // Arrange
        //      2
        //     / \
        //    1   3
        var n1 = new Node(1);
        var n3 = new Node(3);
        var root = new Node(2, n1, n3);

        // Act
        var result = TreeHelper.Postorder(root).ToArray();

        // Assert
        CollectionAssert.AreEqual(new[] { 1, 3, 2 }, result);
    }

    // Rotation tests

    [Test]
    public void RotateLeft_OnRightHeavyTwoNodeTree_RebalStruc()
    {
        // Arrange
        // Before:
        //   1
        //    \
        //     2
        var root = new Node(1, null, new Node(2));

        // Act
        var newRoot = TreeHelper.RotateLeft(root);

        // Assert
        // After:
        //   2
        //  /
        // 1
        Assert.That(newRoot.Value, Is.EqualTo(2));
        Assert.That(newRoot.Left, Is.Not.Null);
        Assert.That(newRoot.Left.Value, Is.EqualTo(1));
        Assert.That(newRoot.Right, Is.Null);
    }

    [Test]
    public void RotateRight_OnLeftHeavyTwoNodeTree_RebalStruc()
    {
        // Arrange
        // Before:
        //    2
        //   /
        //  1
        var root = new Node(2, new Node(1), null);

        // Act
        var newRoot = TreeHelper.RotateRight(root);

        // Assert
        // After:
        //  1
        //   \
        //    2
        Assert.That(newRoot.Value, Is.EqualTo(1));
        Assert.That(newRoot.Right, Is.Not.Null);
        Assert.That(newRoot.Right.Value, Is.EqualTo(2));
        Assert.That(newRoot.Left, Is.Null);
    }

    // Balance factor tests

    [Test]
    public void BalFactor_NullNode_ReturnsZero()
    {
        // Arrange
        // (null root)

        // Act
        int bf = TreeHelper.BalFactor(null);

        // Assert
        Assert.That(bf, Is.EqualTo(0));
    }

    [Test]
    public void BalFactor_LeafNode_ReturnsZero()
    {
        // Arrange
        var root = new Node(10);

        // Act
        int bf = TreeHelper.BalFactor(root);

        // Assert
        Assert.That(bf, Is.EqualTo(0));
    }

    [Test]
    public void BalFactor_LeftHeavyTree_IsPositive()
    {
        // Arrange
        //    2
        //   /
        //  1
        var left = new Node(1);
        var root = new Node(2, left, null);

        // Act
        int bf = TreeHelper.BalFactor(root);

        // Assert
        // Height(left) = 0, Height(right) = -1 => bf = 1
        Assert.That(bf, Is.EqualTo(1));
    }

    [Test]
    public void BalFactor_RightHeavyTree_IsNeg()
    {
        // Arrange
        //  2
        //   \
        //    3
        var right = new Node(3);
        var root = new Node(2, null, right);

        // Act
        int bf = TreeHelper.BalFactor(root);

        // Assert
        // Height(left) = -1, Height(right) = 0 => bf = -1
        Assert.That(bf, Is.EqualTo(-1));
    }
}
