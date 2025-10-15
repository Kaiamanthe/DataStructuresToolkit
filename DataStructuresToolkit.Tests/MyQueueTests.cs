using System;
using DataStructuresToolkit;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MyQueueTests
    {
        [Test]
        public void Enqueue_IncreasesCount_And_PeekReturnsFirstEnqueued()
        {
            // Arrange
            var q = new MyQueue<int>();

            // Act
            q.Enqueue(10);
            q.Enqueue(20);

            // Assert
            Assert.That(q.Count, Is.EqualTo(2)); // Enqueue increases Count
            Assert.That(q.Peek(), Is.EqualTo(10)); // Peek returns first enqueued
        }

        [Test]
        public void Dequeue_ReturnsFirstEnqueued_And_DecrementsCount()
        {
            // Arrange
            var q = new MyQueue<string>();
            q.Enqueue("A");
            q.Enqueue("B");

            // Act
            var first = q.Dequeue();

            // Assert
            Assert.That(first, Is.EqualTo("A"));  // Dequeue returns first enqueued
            Assert.That(q.Count, Is.EqualTo(1));  // Dequeue decrements Count
            Assert.That(q.Peek(), Is.EqualTo("B"));
        }

        [Test]
        public void Dequeue_OnEmpty_Throws_InvalidOperationException()
        {
            // Arrange
            var q = new MyQueue<double>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => q.Dequeue());
        }

        [Test]
        public void Peek_OnEmpty_Throws_InvalidOperationException()
        {
            // Arrange
            var q = new MyQueue<object?>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => q.Peek());
        }

        [Test]
        public void Wraparound_EnqueueDequeue_Repeatedly_PreservesOrder()
        {
            // Arrange
            // Small capacity to force wrap quickly
            var q = new MyQueue<int>(3);

            // Act
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            var d1 = q.Dequeue(); // remove 1, head advances
            q.Enqueue(4);         // wraps tail to index 0
            var d2 = q.Dequeue(); // 2
            q.Enqueue(5);         // may wrap again based on layout

            // Assert
            Assert.That(d1, Is.EqualTo(1));
            Assert.That(d2, Is.EqualTo(2));
            Assert.That(q.Peek(), Is.EqualTo(3)); // front is still 3
            Assert.That(q.Count, Is.EqualTo(3));  // [3,4,5] in order

            // Drain to prove final ordering 3,4,5
            Assert.That(q.Dequeue(), Is.EqualTo(3));
            Assert.That(q.Dequeue(), Is.EqualTo(4));
            Assert.That(q.Dequeue(), Is.EqualTo(5));
        }

        [TestCase(1000)]
        [TestCase(10000)]
        public void Enqueue_Many_TriggersResize_And_MaintainsFifo(int n)
        {
            // Arrange
            var q = new MyQueue<int>();

            // Act
            for (int i = 0; i < n; i++) q.Enqueue(i);

            // Assert
            Assert.That(q.Count, Is.EqualTo(n));
            Assert.That(q.Peek(), Is.EqualTo(0));
            Assert.That(q.Dequeue(), Is.EqualTo(0));
            Assert.That(q.Peek(), Is.EqualTo(1));
        }
    }
}
