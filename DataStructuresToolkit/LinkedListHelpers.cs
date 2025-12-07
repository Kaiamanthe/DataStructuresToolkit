using System;
using System.Collections.Generic;

namespace DataStructuresToolkit
{
    /*
    What did you learn about linked lists by implementing them yourself instead of relying on built-in containers?
        While I was making the singly and doubly linked list, it became apparent that they are nothing like arrays
    and Lists<T>. Arrays are at O(1) and simpler to understand. While testing it became obvious that accessing
    randomly comes with its own downfall.  A higher cost for inserts and deletes near the front. A singly linked
    list, it loses the index-based navigation, only to gain O(1) insertion at the head with the change of just one
    pointer. Array indexing and continual memory have a lot of hidden costs behind the scene.

    How did linked lists compare to structures like List<T> and built-in LinkedList<T>?
        C#’s built-in LinkedList<T> operations feels more convenient, and polished. It automatically tracks head
    and tail ref and offers clean APIs for adding and removing nodes. A custom list requires more manual pointer
    management, and it’s stressful to do updates on next and prev as it is risky every time to risk breaking the
    entire structure of the project. There are plenty of built in features that removes a lot of the risk of
    staying at the same complexity of O(1) insertion and removal when there’s already a node ref.

    In what situations would you choose a custom linked list over built-in structures?
        Custom lists are wheat when learning how pointers and red interact with memory. The main cause of orphaned nodes,
    boundary handling, and traversal issues are much more visible when manually wiring next and prev. What was
    really helpful is learning how a doubly linked list works with structures like LRU caches or undo stack that
    relies on O(1) removal when a  node is known. In a real world scenario I would use List<T> or LinkedList<T>
    since they are optimized, much safer than managing every point manually, and it’s well tested.
     */

    /// <summary>
    /// Generic singly linked list supporting insertion at head,s
    /// traversal, and membership testing.
    /// </summary>
    /// <typeparam name="T">Element type stored in the list.</typeparam>
    /// <remarks>
    /// Complexity (n = number of elements):
    /// <list type="bullet">
    /// <item><description><see cref="AddFirst"/>: <c>O(1)</c> time, <c>O(1)</c> extra space.</description></item>
    /// <item><description><see cref="Traverse"/>: <c>O(n)</c> time.</description></item>
    /// <item><description><see cref="Contains"/>: worst-case <c>O(n)</c> time.</description></item>
    /// </list>
    /// </remarks>
    public class SinglyLinkedList<T>
    {
        private ListNode<T> _head;
        private int _count;

        /// <summary>
        /// Gets number of elements currently stored in list.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Inserts a new val at start of list.
        /// </summary>
        /// <param name="value">The val to insert.</param>
        /// <remarks>
        /// Time: <c>O(1)</c>. Extra space: <c>O(1)</c>.
        /// Only head pointer and one <see cref="ListNode{T}.Next"/> pointer are updated.
        /// </remarks>
        public void AddFirst(T value)
        {
            var newNode = new ListNode<T>(value)
            {
                Next = _head
            };

            _head = newNode;
            _count++;
        }

        /// <summary>
        /// Visits each element from head to tail in list order.
        /// </summary>
        /// <param name="visit">Callback invoked once per element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="visit"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Time: <c>O(n)</c>, where n is the number of elements.
        /// </remarks>
        public void Traverse(Action<T> visit)
        {
            if (visit == null) throw new ArgumentNullException(nameof(visit));

            var current = _head;
            while (current != null)
            {
                visit(current.Value);
                current = current.Next;
            }
        }

        /// <summary>
        /// Determines if list contains a specified val.
        /// </summary>
        /// <param name="value">The val to search for.</param>
        /// <returns>
        /// <c>true</c> if an equal val is found; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Time: worst-case <c>O(n)</c> (val at tail or missing).
        /// Extra space: <c>O(1)</c>.
        /// </remarks>
        public bool Contains(T value)
        {
            var comparer = EqualityComparer<T>.Default;
            var current = _head;

            while (current != null)
            {
                if (comparer.Equals(current.Value, value))
                    return true;

                current = current.Next;
            }

            return false;
        }
    }

    /// <summary>
    /// Generic doubly linked list that tracks both head and tail
    /// for efficient insertion at either end and removal by val.
    /// </summary>
    /// <typeparam name="T">Element type stored in the list.</typeparam>
    /// <remarks>
    /// Complexity (n = number of elements):
    /// <list type="bullet">
    /// <item><description><see cref="AddFirst"/> / <see cref="AddLast"/>: <c>O(1)</c> time.</description></item>
    /// <item><description><see cref="TraverseFor"/> / <see cref="TraverseBack"/>: <c>O(n)</c> time.</description></item>
    /// <item><description><see cref="Remove"/>: search cost <c>O(n)</c>, pointer rewiring <c>O(1)</c>.</description></item>
    /// </list>
    /// </remarks>
    public class DoublyLinkedList<T>
    {
        private DoublyListNode<T> _head;
        private DoublyListNode<T> _tail;
        private int _count;

        /// <summary>
        /// Gets number of elements currently stored in list.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Inserts a new val at head of list.
        /// </summary>
        /// <param name="value">The val to insert.</param>
        /// <remarks>
        /// Time: <c>O(1)</c>. Extra space: <c>O(1)</c>.
        /// </remarks>
        public void AddFirst(T value)
        {
            var newNode = new DoublyListNode<T>(value);

            if (_head == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head.Prev = newNode;
                _head = newNode;
            }

            _count++;
        }

        /// <summary>
        /// Inserts a new val at tail of list.
        /// </summary>
        /// <param name="value">The val to append.</param>
        /// <remarks>
        /// Time: <c>O(1)</c>. Extra space: <c>O(1)</c>.
        /// </remarks>
        public void AddLast(T value)
        {
            var newNode = new DoublyListNode<T>(value);

            if (_tail == null)
            {
                _head = _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                newNode.Prev = _tail;
                _tail = newNode;
            }

            _count++;
        }

        /// <summary>
        /// Traverses list from head to tail.
        /// </summary>
        /// <param name="visit">Callback invoked once per element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="visit"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Time: <c>O(n)</c>.
        /// </remarks>
        public void TraverseFor(Action<T> visit)
        {
            if (visit == null) throw new ArgumentNullException(nameof(visit));

            var current = _head;
            while (current != null)
            {
                visit(current.Value);
                current = current.Next;
            }
        }

        /// <summary>
        /// Traverses list from tail back to head.
        /// </summary>
        /// <param name="visit">Callback invoked once per element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="visit"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Time: <c>O(n)</c>.
        /// </remarks>
        public void TraverseBack(Action<T> visit)
        {
            if (visit == null) throw new ArgumentNullException(nameof(visit));

            var current = _tail;
            while (current != null)
            {
                visit(current.Value);
                current = current.Prev;
            }
        }

        /// <summary>
        /// Removes the first node whose value is equal to the specified value.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>
        /// <c>true</c> if a matching node was found and removed;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Time: worst-case <c>O(n)</c> to locate the value, then <c>O(1)</c> to
        /// rewire neighbors. Extra space: <c>O(1)</c>.
        /// </remarks>
        public bool Remove(T value)
        {
            var comparer = EqualityComparer<T>.Default;
            var current = _head;

            // Search for node with matching value.
            while (current != null && !comparer.Equals(current.Value, value))
            {
                current = current.Next;
            }

            if (current == null)
                return false; // not found

            // Rewire previous neighbor (or update head).
            if (current.Prev != null)
            {
                current.Prev.Next = current.Next;
            }
            else
            {
                _head = current.Next;
            }

            // Rewire next neighbor (or update tail).
            if (current.Next != null)
            {
                current.Next.Prev = current.Prev;
            }
            else
            {
                _tail = current.Prev;
            }

            _count--;
            return true;
        }
    }

}
