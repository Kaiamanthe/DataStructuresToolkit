using System;
using System.Collections.Generic;


namespace DataStructuresToolkit
{
    /// <summary>
    /// Min heap based integer priority queue.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Internally uses <see cref="List{T}"/> of <see cref="int"/> where index 0
    /// is root of a binary heap. For any index <c>i</c>, the child live at
    /// <c>2 * i + 1</c> and <c>2 * i + 2</c>. The heap property ensures each
    /// parent is less than or equal to its children, so the smallest value is
    /// always at index 0.
    /// </para>
    /// </remarks>
    public class PriorityQueue
    {
        private readonly List<int> _heap = new List<int>();

        /// <summary>
        /// Gets the number of elements currently stored in priority queue.
        /// </summary>
        /// <remarks>Time: <c>O(1)</c>. Space: <c>O(1)</c>.</remarks>
        public int Count => _heap.Count;

        /// <summary>
        /// Inserts a new value into min-heap, restoring heap property
        /// using bubble-up.
        /// </summary>
        /// <param name="val">The value to insert.</param>
        /// <remarks>
        /// <b>Algorithm:</b> append the value to the end of the list, then
        /// repeatedly swap it with its parent while it is smaller than that
        /// parent. This is standard “bubble-up” or “sift-up” operation.
        /// <br/>
        /// <b>Complexity:</b> Time <c>O(log n)</c> in worst case height of
        /// heap extra space <c>O(1)</c>.
        /// </remarks>
        public void Enqueue(int val)
        {
            _heap.Add(val);
            BubbleUp(_heap.Count - 1);
        }

        /// <summary>
        /// Removes and returns the smallest value in priority queue.
        /// </summary>
        /// <returns>The minimum value currently stored.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if priority queue is empty.
        /// </exception>
        /// <remarks>
        /// <b>Algorithm:</b> save root value, move the last element to
        /// root, shrink the list, then restore heap property by repeatedly
        /// swapping new root downward with smaller child (bubble-down /
        /// heapify). <br/>
        /// <b>Complexity:</b> Time <c>O(log n)</c> in the worst case extra
        /// space <c>O(1)</c>.
        /// </remarks>
        public int Dequeue()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");

            int min = _heap[0];
            int lastIndex = _heap.Count - 1;

            // Move last element to root and shrink.
            int last = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);

            if (_heap.Count > 0)
            {
                _heap[0] = last;
                BubbleDown(0);
            }

            return min;
        }

        /// <summary>
        /// Returns smallest value without removing it.
        /// </summary>
        /// <returns>Current minimum value.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if priority queue is empty.
        /// </exception>
        public int Peek()
        {
            if (_heap.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");
            return _heap[0];
        }

        // Private heap helpers

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_heap[index] >= _heap[parent])
                    break;

                Swap(index, parent);
                index = parent;
            }
        }

        private void BubbleDown(int index)
        {
            int lastIndex = _heap.Count - 1;

            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int smallest = index;

                if (left <= lastIndex && _heap[left] < _heap[smallest])
                    smallest = left;

                if (right <= lastIndex && _heap[right] < _heap[smallest])
                    smallest = right;

                if (smallest == index)
                    break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int i, int j)
        {
            if (i == j) return;
            int tmp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = tmp;
        }
    }
}
