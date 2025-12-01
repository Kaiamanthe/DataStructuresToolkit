using System;
using System.Collections.Generic;

namespace DataStructuresToolkit
{
    /*
    Reflection
    What did you learn about hash tables by implementing your own instead of relying on built-in containers?
    
    While making the hash table, I understand how much behavior depends on the hash function and collision strategy.
    Chaining in a bucket array made collisions visible, and working through insert and lookup logic showed why
    average-case O(1) performance is not guaranteed without balanced buckets.

    How did the built-in Dictionary and HashSet compare to your custom implementation?
    Dictionary and HashSet felt more robust and convenient. It was able to manage resizing, performance optimization,
    and collision, while exposing a simple API for lookups and insert. A custom table needs more hands-on attention
    as it needs manual debugging bucket indices, preventing duplicates, and handling negative keys. Due to these reasons
    a high level container is more obviously needed.

    In what situations would you choose a custom table over built-in C# associative structures?
    When learning or demonstrating internal data structure, custom tables are the better option. The experience from messing
    with custom hashing behavior, or optimizing for niche patter where the default isn’t the best. Real applications, one should
    rely on Dictionary and HashSet for their generic, tested, and optimized for the majority of workloads.
    */

    /// <summary>
    /// Chaining-based hash table using an int array of buckets.
    /// </summary>
    public class SimpleHashTable
    {
        private readonly List<int>[] _buckets;

        /// <summary>
        /// Create new hash table with given number of buckets.
        /// </summary>
        /// <param name="size">Number of buckets.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="size"/> is less than 1.</exception>
        public SimpleHashTable(int size)
        {
            if (size < 1) throw new ArgumentOutOfRangeException(nameof(size), "Size must be >= 1.");

            _buckets = new List<int>[size];
            for (int i = 0; i < _buckets.Length; i++)
                _buckets[i] = new List<int>();
        }

        /// <summary>
        /// Insert key into table without overwriting existing key.
        /// </summary>
        /// <param name="key">Key to insert.</param>
        /// <remarks>
        /// Average-case: O(1) (small bucket). Worst-case: O(n) if many keys collide into one bucket.
        /// </remarks>
        public void Insert(int key)
        {
            int idx = IndexFor(key);
            var bucket = _buckets[idx];

            // Prevent duplicates
            if (!bucket.Contains(key))
                bucket.Add(key);
        }

        /// <summary>
        /// Check whether a key exists in table.
        /// </summary>
        /// <param name="key">Key to search for.</param>
        /// <returns>True if found, otherwise false.</returns>
        /// <remarks>
        /// Average-case: O(1). Worst-case: O(n) if all keys land in one bucket.
        /// </remarks>
        public bool Contains(int key)
        {
            int idx = IndexFor(key);
            return _buckets[idx].Contains(key);
        }

        /// <summary>
        /// Print contents of all buckets (shows collisions through chaining).
        /// </summary>
        /// <remarks>
        /// O(b + n) where b is bucket count and n is number of stored keys (total across buckets).
        /// </remarks>
        public void PrintTable()
        {
            Console.WriteLine("Hash Table Buckets:");
            for (int i = 0; i < _buckets.Length; i++)
            {
                var bucket = _buckets[i];
                string chain = bucket.Count == 0 ? "∅" : string.Join(" -> ", bucket);
                Console.WriteLine($"[{i}] {chain}");
            }
        }

        private int IndexFor(int key)
        {
            int idx = key % _buckets.Length;
            if (idx < 0) idx += _buckets.Length;
            return idx;
        }
    }
}
