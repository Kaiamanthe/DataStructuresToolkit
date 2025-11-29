using DataStructuresToolkit;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Linq;

namespace DataStructuresToolkit.Tests;

public class AvlTreeTests
{

    [Test]
    public void NewTree_IsEmpty_HeightMinusOne_ContainsFalse()
    {
        // Arrange
        var avl = new AvlTree();

        // Act
        bool isEmpty = avl.IsEmpty;
        int height = avl.Height;
        bool contains = avl.Contains(42);
        var inorder = avl.Inorder().ToArray();

        // Assert
        Assert.That(isEmpty, Is.True);
        Assert.That(height, Is.EqualTo(-1));
        Assert.That(contains, Is.False);
        CollectionAssert.IsEmpty(inorder);
    }

    [Test]
    public void Insert_SingleVal_SetsHeightZero_AndContainsVal()
    {
        // Arrange
        var avl = new AvlTree();

        // Act
        bool inserted = avl.Insert(10);
        bool isEmpty = avl.IsEmpty;
        int height = avl.Height;
        bool contains = avl.Contains(10);
        var inorder = avl.Inorder().ToArray();

        // Assert
        Assert.That(inserted, Is.True);
        Assert.That(isEmpty, Is.False);
        Assert.That(height, Is.EqualTo(0));
        Assert.That(contains, Is.True);
        CollectionAssert.AreEqual(new[] { 10 }, inorder);
    }

    [Test]
    public void Insert_DupVal_ReturnsFalse_AndDoesNotAddExtraNode()
    {
        // Arrange
        var avl = new AvlTree();

        // Act
        bool firstInsert = avl.Insert(10);
        bool secondInsert = avl.Insert(10);
        var inorder = avl.Inorder().ToArray();

        // Assert
        Assert.That(firstInsert, Is.True);
        Assert.That(secondInsert, Is.False);
        CollectionAssert.AreEqual(new[] { 10 }, inorder);
    }

    [Test]
    public void Insert_10_20_30_PerformsRightRightRotation_AndBalancesTree()
    {
        // Arrange
        var avl = new AvlTree();

        // Act
        avl.Insert(10);
        avl.Insert(20);
        avl.Insert(30);

        int height = avl.Height;
        var root = avl.Root;
        var inorder = avl.Inorder().ToArray();

        // Assert
        Assert.That(avl.IsEmpty, Is.False);
        Assert.That(height, Is.EqualTo(1));

        Assert.That(root, Is.Not.Null);
        Assert.That(root.Value, Is.EqualTo(20));
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Value, Is.EqualTo(10));
        Assert.That(root.Right.Value, Is.EqualTo(30));

        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, inorder);
    }

    [Test]
    public void Insert_30_20_10_PerformsLeftLeftRotation_AndBalancesTree()
    {
        // Arrange
        var avl = new AvlTree();

        // Act
        avl.Insert(30);
        avl.Insert(20);
        avl.Insert(10);

        int height = avl.Height;
        var root = avl.Root;

        // Assert
        Assert.That(height, Is.EqualTo(1));

        Assert.That(root, Is.Not.Null);
        Assert.That(root.Value, Is.EqualTo(20));
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Value, Is.EqualTo(10));
        Assert.That(root.Right.Value, Is.EqualTo(30));
    }

    [Test]
    public void Insert_30_10_20_PerformsLeftRightRotation_AndBalancesTree()
    {
        // Arrange
        var avl = new AvlTree();

        // Act
        avl.Insert(30);
        avl.Insert(10);
        avl.Insert(20);

        int height = avl.Height;
        var root = avl.Root;

        // Assert
        Assert.That(height, Is.EqualTo(1));

        Assert.That(root, Is.Not.Null);
        Assert.That(root.Value, Is.EqualTo(20));
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Value, Is.EqualTo(10));
        Assert.That(root.Right.Value, Is.EqualTo(30));
    }

    [Test]
    public void Insert_10_30_20_PerformsRightLeftRotation_AndBalancesTree()
    {
        // Arrange
        var avl = new AvlTree();

        // Act
        avl.Insert(10);
        avl.Insert(30);
        avl.Insert(20);

        int height = avl.Height;
        var root = avl.Root;

        // Assert
        Assert.That(height, Is.EqualTo(1));

        Assert.That(root, Is.Not.Null);
        Assert.That(root.Value, Is.EqualTo(20));
        Assert.That(root.Left, Is.Not.Null);
        Assert.That(root.Right, Is.Not.Null);
        Assert.That(root.Left.Value, Is.EqualTo(10));
        Assert.That(root.Right.Value, Is.EqualTo(30));
    }

    [Test]
    public void Inorder_ReturnsSortedVal_ForUnsortedInsertSequence()
    {
        // Arrange
        var avl = new AvlTree();
        int[] values = { 30, 5, 40, 10, 20 };

        // Act
        foreach (var v in values)
            avl.Insert(v);

        var inorder = avl.Inorder().ToArray();
        var expected = values.OrderBy(x => x).ToArray();

        // Assert
        CollectionAssert.AreEqual(expected, inorder);
    }

    [Test]
    public void Contains_FindsExistingVal_AndNotMissingOnes()
    {
        // Arrange
        var avl = new AvlTree();
        int[] values = { 50, 20, 70, 10, 40, 60, 80 };

        // Act
        foreach (var v in values)
            avl.Insert(v);

        // Assert
        foreach (var v in values)
            Assert.That(avl.Contains(v), Is.True, $"Should contain {v}");

        Assert.That(avl.Contains(999), Is.False);
    }
}
