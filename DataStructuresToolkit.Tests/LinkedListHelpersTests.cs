using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class LinkedListHelpersTests
    {
        private SinglyLinkedList<int> _singly;
        private DoublyLinkedList<string> _doubly;

        [SetUp]
        public void SetUp()
        {
            _singly = new SinglyLinkedList<int>();
            _doubly = new DoublyLinkedList<string>();
        }

        // SinglyLinkedList<T> test

        [Test]
        public void Singly_AddFirst_BuildsListInReverseInsertOrder()
        {
            // Arrange + Act
            _singly.AddFirst(10);
            _singly.AddFirst(20);
            _singly.AddFirst(30); // expected list: 30 -> 20 -> 10

            var values = new List<int>();
            _singly.Traverse(values.Add);

            // Assert
            Assert.That(_singly.Count, Is.EqualTo(3), "Count should match number of inserted elements.");
            CollectionAssert.AreEqual(
                new[] { 30, 20, 10 },
                values,
                "Singly list should traverse from most recently added at head to oldest at tail."
            );
        }

        [Test]
        public void Singly_Contains_FindsExistingValues_AndRejectsMissing()
        {
            // Arrange
            _singly.AddFirst(10);
            _singly.AddFirst(20);
            _singly.AddFirst(30); // 30, 20, 10

            // Act + Assert
            Assert.That(_singly.Contains(30), Is.True, "Head value should be found.");
            Assert.That(_singly.Contains(20), Is.True, "Middle value should be found.");
            Assert.That(_singly.Contains(10), Is.True, "Tail value should be found.");
            Assert.That(_singly.Contains(99), Is.False, "Missing value should not be found.");

            // Empty list case
            var empty = new SinglyLinkedList<int>();
            Assert.That(empty.Contains(1), Is.False, "Empty list should never contain any value.");
        }

        [Test]
        public void Singly_Traverse_ThrowsOnNullAction()
        {
            // Arrange
            _singly.AddFirst(1);

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => _singly.Traverse(null));
        }

        // DoublyLinkedList<T> tests

        [Test]
        public void Doubly_AddFirstAndAddLast_ForAndBackwardTraversalMatch()
        {
            // Arrange
            _doubly.AddFirst("world");
            _doubly.AddFirst("hello");  // hello, world
            _doubly.AddLast("again");   // hello, world, again

            var forward = new List<string>();
            var backward = new List<string>();

            // Act
            _doubly.TraverseFor(forward.Add);
            _doubly.TraverseBack(backward.Add);

            // Assert
            Assert.That(_doubly.Count, Is.EqualTo(3), "Count should reflect all inserted nodes.");

            CollectionAssert.AreEqual(
                new[] { "hello", "world", "again" },
                forward,
                "Forward traversal should go head -> tail."
            );

            CollectionAssert.AreEqual(
                new[] { "again", "world", "hello" },
                backward,
                "Backward traversal should go tail -> head."
            );
        }

        [Test]
        public void Doubly_Remove_MiddleNode_RewiresNeighborsAndUpdatesCount()
        {
            // Arrange
            _doubly.AddLast("a");
            _doubly.AddLast("b");
            _doubly.AddLast("c");   // a, b, c

            // Act
            bool removed = _doubly.Remove("b");

            var forward = new List<string>();
            var backward = new List<string>();
            _doubly.TraverseFor(forward.Add);
            _doubly.TraverseBack(backward.Add);

            // Assert
            Assert.That(removed, Is.True, "Existing middle node should be removed successfully.");
            Assert.That(_doubly.Count, Is.EqualTo(2), "Count should decrement after removal.");

            CollectionAssert.AreEqual(
                new[] { "a", "c" },
                forward,
                "Forward traversal after removal should skip the removed middle node."
            );

            CollectionAssert.AreEqual(
                new[] { "c", "a" },
                backward,
                "Backward traversal after removal should also skip the removed middle node."
            );
        }

        [Test]
        public void Doubly_Remove_HeadAndTail_CoversEdgeCases()
        {
            // Arrange
            _doubly.AddLast("head");
            _doubly.AddLast("middle");
            _doubly.AddLast("tail");   // head, middle, tail

            // Act: remove head
            bool removedHead = _doubly.Remove("head");

            // Act: remove tail
            bool removedTail = _doubly.Remove("tail");

            var forward = new List<string>();
            _doubly.TraverseFor(forward.Add);

            // Assert
            Assert.That(removedHead, Is.True, "Head node should be removable.");
            Assert.That(removedTail, Is.True, "Tail node should be removable.");
            Assert.That(_doubly.Count, Is.EqualTo(1), "Only the middle node should remain.");

            CollectionAssert.AreEqual(
                new[] { "middle" },
                forward,
                "After removing head and tail, only the middle element should remain."
            );
        }

        [Test]
        public void Doubly_Remove_NonExistingValue_ReturnsFalse_AndDoesNotChangeList()
        {
            // Arrange
            _doubly.AddLast("x");
            _doubly.AddLast("y");

            var before = new List<string>();
            _doubly.TraverseFor(before.Add);

            // Act
            bool removed = _doubly.Remove("missing");

            var after = new List<string>();
            _doubly.TraverseFor(after.Add);

            // Assert
            Assert.That(removed, Is.False, "Remove should return false for missing values.");
            Assert.That(_doubly.Count, Is.EqualTo(2), "Count should not change when remove fails.");

            CollectionAssert.AreEqual(
                before,
                after,
                "List contents should remain unchanged when removal fails."
            );
        }

        [Test]
        public void Doubly_TraverseFor_ThrowsOnNullAction()
        {
            _doubly.AddFirst("one");
            Assert.Throws<ArgumentNullException>(() => _doubly.TraverseFor(null));
        }

        [Test]
        public void Doubly_TraverseBack_ThrowsOnNullAction()
        {
            _doubly.AddFirst("one");
            Assert.Throws<ArgumentNullException>(() => _doubly.TraverseBack(null));
        }
    }
}
