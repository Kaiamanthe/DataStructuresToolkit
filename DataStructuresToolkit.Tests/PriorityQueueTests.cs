using System;
using NUnit.Framework;
using DataStructuresToolkit;

namespace DataStructuresToolkit.Tests;

[TestFixture]
public class PriorityQueueTests
{
    private PriorityQueue _pq;

    [SetUp]
    public void Setup()
    {
        // Create a fresh priority queue instance before each test.
        _pq = new PriorityQueue();
    }

    [Test]
    public void NewQueue_HasCountZero()
    {

        // Act
        int count = _pq.Count;

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void Enqueue_SingleElement_IncreasesCountAndPeekReturnsThatElement()
    {
        // Act
        _pq.Enqueue(42);
        int count = _pq.Count;
        int top = _pq.Peek();

        // Assert
        Assert.That(count, Is.EqualTo(1));
        Assert.That(top, Is.EqualTo(42));
    }

    [Test]
    public void Enqueue_Multiple_UnorderedVal_DequeueReturnsSortedAsc()
    {
        // Arrange
        int[] values = { 5, 1, 9, 3, 7 };

        foreach (var v in values)
        {
            _pq.Enqueue(v);
        }

        Array.Sort(values);

        // Act & Assert
        // Dequeue should yield values in ascending order.
        foreach (var expected in values)
        {
            // Act
            int actual = _pq.Dequeue();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        // Assert (post-condition)
        Assert.That(_pq.Count, Is.EqualTo(0));
    }

    [Test]
    public void Enqueue_AllowsDup_DequeuesAllInNonDecreasingOrder()
    {
        // Arrange
        int[] values = { 5, 1, 3, 1, 5 };

        foreach (var v in values)
        {
            _pq.Enqueue(v);
        }

        Array.Sort(values);

        // Act & Assert
        foreach (var expected in values)
        {
            // Act
            int actual = _pq.Dequeue();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        // Assert (post-condition)
        Assert.That(_pq.Count, Is.EqualTo(0));
    }

    [Test]
    public void Enqueue_MixedPosAndNegVal_MinIsReturnedFirst()
    {
        // Arrange
        int[] values = { 0, -10, 5, -3, 2 };
        foreach (var v in values)
        {
            _pq.Enqueue(v);
        }

        // First min should be -10.
        // Arrange first assertion is already done.
        // Act
        int peek1 = _pq.Peek();
        int d1 = _pq.Dequeue();
        int d2 = _pq.Dequeue();
        int d3 = _pq.Dequeue();
        int d4 = _pq.Dequeue();
        int d5 = _pq.Dequeue();

        // Assert
        Assert.That(peek1, Is.EqualTo(-10));
        Assert.That(d1, Is.EqualTo(-10));
        Assert.That(d2, Is.EqualTo(-3));
        Assert.That(d3, Is.EqualTo(0));
        Assert.That(d4, Is.EqualTo(2));
        Assert.That(d5, Is.EqualTo(5));
        Assert.That(_pq.Count, Is.EqualTo(0));
    }

    [Test]
    public void Peek_DoesNotRemoveElement_AndDoesNotChangeCount()
    {
        // Arrange
        _pq.Enqueue(4);
        _pq.Enqueue(2);
        _pq.Enqueue(10);
        int beforeCount = _pq.Count;

        // Act
        int peeked = _pq.Peek();
        int afterPeekCount = _pq.Count;

        // Assert
        Assert.That(peeked, Is.EqualTo(2), "Peek should return current minimum.");
        Assert.That(afterPeekCount, Is.EqualTo(beforeCount), "Peek should not remove element.");

        // Act (next step)
        int dequeued = _pq.Dequeue();

        // Assert (next step)
        Assert.That(dequeued, Is.EqualTo(peeked), "First Dequeue should match previously peeked value.");
    }

    [Test]
    public void Dequeue_RemovesElementsUntilEmpty_ThenCountIsZero()
    {
        // Arrange
        _pq.Enqueue(3);
        _pq.Enqueue(1);
        _pq.Enqueue(2);

        // Act
        int first = _pq.Dequeue();
        int second = _pq.Dequeue();
        int third = _pq.Dequeue();
        int finalCount = _pq.Count;

        // Assert
        Assert.That(first, Is.EqualTo(1));
        Assert.That(second, Is.EqualTo(2));
        Assert.That(third, Is.EqualTo(3));
        Assert.That(finalCount, Is.EqualTo(0));
    }

    [Test]
    public void Interleaved_EnqueueDequeue_AlwaysMaintainsMinAtFront()
    {
        // Arrange
        _pq.Enqueue(5);
        _pq.Enqueue(3);
        _pq.Enqueue(8);

        // Act
        int first = _pq.Dequeue(); // min of {5,3,8}

        _pq.Enqueue(1);
        _pq.Enqueue(4);

        int peekAfterMore = _pq.Peek();
        int d2 = _pq.Dequeue();
        int d3 = _pq.Dequeue();
        int d4 = _pq.Dequeue();
        int d5 = _pq.Dequeue();
        int finalCount = _pq.Count;

        // Assert
        Assert.That(first, Is.EqualTo(3));
        Assert.That(peekAfterMore, Is.EqualTo(1));
        Assert.That(d2, Is.EqualTo(1));
        Assert.That(d3, Is.EqualTo(4));
        Assert.That(d4, Is.EqualTo(5));
        Assert.That(d5, Is.EqualTo(8));
        Assert.That(finalCount, Is.EqualTo(0));
    }

    [Test]
    public void Dequeue_OnEmptyQueue_ThrowsInvalidOperationException()
    {

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _pq.Dequeue());
    }

    [Test]
    public void Peek_OnEmptyQueue_ThrowsInvalidOperationException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _pq.Peek());
    }
}
