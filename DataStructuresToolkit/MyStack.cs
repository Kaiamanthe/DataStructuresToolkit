using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresToolkit
{
    /// <summary>LIFO stack resizable array (doubling).</summary>
    public class MyStack<T>
    {
        private T[] _items;
        private int _count;
        private const int DefaultCapacity = 4;

        /// <remarks>O(1)</remarks>
        public int Count => _count;

        public MyStack(int capacity = DefaultCapacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _items = capacity == 0 ? Array.Empty<T>() : new T[capacity];
        }

        /// <remarks>Amortized O(1)</remarks>
        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                int newCap = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
                Array.Resize(ref _items, newCap);
            }
            _items[_count++] = item;
        }

        /// <remarks>O(1)</remarks>
        public T Pop()
        {
            if (_count == 0) throw new InvalidOperationException("Stack is empty.");
            var v = _items[--_count];
            _items[_count] = default!;
            return v;
        }

        /// <remarks>O(1)</remarks>
        public T Peek()
        {
            if (_count == 0) throw new InvalidOperationException("Stack is empty.");
            return _items[_count - 1];
        }
    }
}