using DataStructuresToolkit;
using DataStructuresToolKit;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class MyStackTests
    {
        [Test]
        public void Push_IncreasesCount_And_PeekReturnsLastPushed()
        {
            // Arrange
            var s = new MyStack<int>();

            // Act
            s.Push(1);
            s.Push(2);

            // Assert
            Assert.That(s.Count, Is.EqualTo(2)); // Push increases Count
            Assert.That(s.Peek(), Is.EqualTo(2)); // Peek returns last pushed
        }

        [Test]
        public void Pop_ReturnsLastPushed_And_DecrementsCount()
        {
            // Arrange
            var s = new MyStack<string>();
            s.Push("a");
            s.Push("b");

            // Act
            var popped = s.Pop();

            // Assert
            Assert.That(popped, Is.EqualTo("b")); // Pop returns last pushed
            Assert.That(s.Count, Is.EqualTo(1));  // Pop decrements Count
            Assert.That(s.Peek(), Is.EqualTo("a"));
        }

        [Test]
        public void Pop_OnEmpty_Throws_InvalidOperationException()
        {
            // Arrange
            var s = new MyStack<double>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => s.Pop());
        }

        [Test]
        public void Peek_OnEmpty_Throws_InvalidOperationException()
        {
            // Arrange
            var s = new MyStack<object?>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => s.Peek());
        }

        // Optional: growth under repeated Push (resizing path)
        [TestCase(1000)]
        [TestCase(10000)]
        public void Push_Many_TriggersResize_And_MaintainsLifo(int n)
        {
            // Arrange
            var s = new MyStack<int>();

            // Act
            for (int i = 0; i < n; i++) s.Push(i);

            // Assert
            Assert.That(s.Count, Is.EqualTo(n));
            Assert.That(s.Peek(), Is.EqualTo(n - 1));
            Assert.That(s.Pop(), Is.EqualTo(n - 1));
            Assert.That(s.Peek(), Is.EqualTo(n - 2));
        }
    }
}
