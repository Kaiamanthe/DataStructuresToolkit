using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresToolkit
{
    /// <summary>FIFO queue with circular array (doubles when full).</summary>
    public class MyQueue<T>
    {
        private T[] _items;
        private int _head, _tail, _count;
        private const int DefaultCapacity = 4;

        /// <remarks>O(1)</remarks>
        public int Count => _count;

        public MyQueue(int capacity = DefaultCapacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _items = capacity == 0 ? Array.Empty<T>() : new T[capacity];
        }

        /// <remarks>Amortized O(1)</remarks>
        public void Enqueue(T item)
        {
            if (_count == _items.Length)
            {
                int newCap = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
                var newArr = new T[newCap];

                if (_count > 0)
                {
                    if (_head < _tail)
                        Array.Copy(_items, _head, newArr, 0, _count);
                    else
                    {
                        int right = _items.Length - _head;
                        Array.Copy(_items, _head, newArr, 0, right);
                        Array.Copy(_items, 0, newArr, right, _tail);
                    }
                }

                _items = newArr;
                _head = 0;
                _tail = _count;
            }

            _items[_tail] = item;
            _tail = (_tail + 1) % _items.Length;
            _count++;
        }

        /// <remarks>Amortized O(1)</remarks>
        public T Dequeue()
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty.");
            var v = _items[_head];
            _items[_head] = default!;
            _head = (_head + 1) % _items.Length;
            _count--;
            return v;
        }

        /// <remarks>O(1)</remarks>
        public T Peek()
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty.");
            return _items[_head];
        }
    }
}