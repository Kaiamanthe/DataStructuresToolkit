using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/*
Why did I add a set feature to this project?
	The toolkit already shows a lot of data structures such as arrays, lists, stacks, queues, linked
lists, hash tables, BSTs, AVL trees, sorting and searching. Set features seems to be a good pick as several
demo works with groups of IDs or Values where uniqueness matters. Before adding the extensions parts of the
toolkit was relying on List<int> and manual duplicate checks, which worked but didn’t show the uniqueness of
the actual goal. HashSet<int> makes it obvious that the code supports the concepts that’s already shown in
hashing and associative arrays.

What did the benchmark reveal?
	When comparing List<int> against HashSet<int> contains match lessons shown in my timing tables for sorting
and searching. List.Contains is O(n), so the whole thing grows it slows down. With a large quantity of 100,00
and tons of look ups it's pretty obvious. Hashset.Contains, stays fast due to Hashing giving an O(1) average
look up. The speedup printed to the console right beside set operations really shows the difference is.

How does this help the final Capstone?
    What was added for this week helps the toolkit have one more real comparison of data structures and
clarifies when sets should be used instead of lists. The project also gave me experience to point to output
and show how choosing the proper structure for the case complexity will make the whole system more efficient.
*/

namespace DataStructuresToolkit
{
    /// <summary>
    /// Helper methods for sets operations.
    /// Demonstrates uniqueness, set operations, and benchmarking.
    /// </summary>
    public static class SetHelpers
    {
        /// <summary>
        /// Builds a <see cref="HashSet{T}"/> of unique IDs from a sequence that may contain duplicates.
        /// </summary>
        /// <param name="ids">A sequence of integer IDs, possibly containing duplicates.</param>
        /// <returns>
        /// A <see cref="HashSet{T}"/> containing only unique IDs from the input sequence.
        /// </returns>
        /// <remarks>
        /// Time complexity: O(n), where n is the number of IDs.
        /// Each insertion into a hash set is O(1) on average, so n insertions cost O(n).
        /// Space complexity: O(n), because the hash set stores up to n unique elements.
        /// </remarks>
        public static HashSet<int> BuildUniqueIdSet(IEnumerable<int> ids)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));

            return new HashSet<int>(ids);
        }

        /// <summary>
        /// Creates example sets and computes Intersection, Union, and Difference.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// <list type="bullet">
        /// <item><description>The original existing-item ID set.</description></item>
        /// <item><description>The original new-item ID set.</description></item>
        /// <item><description>The intersection of the two sets.</description></item>
        /// <item><description>The union of the two sets.</description></item>
        /// <item><description>The difference (existing minus new).</description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Core operations like <see cref="HashSet{T}.UnionWith"/>,
        /// <see cref="HashSet{T}.IntersectWith"/>, and <see cref="HashSet{T}.ExceptWith"/>
        /// run in O(n + m) time, where n and m are the sizes of the two sets.
        /// </remarks>
        public static (
            HashSet<int> existingItemIds,
            HashSet<int> newItemIds,
            HashSet<int> intersection,
            HashSet<int> union,
            HashSet<int> difference
        ) GetSetOpResults()
        {
            var existingItemIds = new HashSet<int> { 1, 2, 3, 4, 5 };
            var newItemIds = new HashSet<int> { 4, 5, 6, 7 };

            var intersection = new HashSet<int>(existingItemIds);
            intersection.IntersectWith(newItemIds);

            var union = new HashSet<int>(existingItemIds);
            union.UnionWith(newItemIds);

            var difference = new HashSet<int>(existingItemIds);
            difference.ExceptWith(newItemIds);

            return (existingItemIds, newItemIds, intersection, union, difference);
        }

        /// <summary>
        /// Benchmarks membership lookup time in <see cref="List{T}"/> versus <see cref="HashSet{T}"/>.
        /// returns the raw numbers.
        /// </summary>
        /// <param name="dataSize">Number of distinct integers to generate.</param>
        /// <param name="lookupCount">Number of membership tests to perform on each structure.</param>
        /// <returns>
        /// A tuple containing:
        /// <list type="bullet">
        /// <item><description>The data size used for the test.</description></item>
        /// <item><description>The number of lookups performed.</description></item>
        /// <item><description>The elapsed time (ms) for <see cref="List{T}.Contains(T)"/>.</description></item>
        /// <item><description>The elapsed time (ms) for <see cref="HashSet{T}.Contains(T)"/>.</description></item>
        /// <item><description>The number of lookup hits found in list.</description></item>
        /// <item><description>The number of lookup hits found in hash set.</description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// <see cref="List{T}.Contains(T)"/> runs in O(n) time because it scans each element.
        /// <see cref="HashSet{T}.Contains(T)"/> runs in average-case O(1) time due to hashing.
        /// This method uses <see cref="Stopwatch"/> for a simple benchmark and leaves output
        /// formatting to the caller.
        /// </remarks>
        public static (
            int dataSize,
            int lookupCount,
            long listMs,
            long setMs,
            int hitsList,
            int hitsSet
        ) BenchListVsHashSetContainsCore(int dataSize = 100_000, int lookupCount = 20_000)
        {
            var data = Enumerable.Range(0, dataSize).ToList();

            var list = new List<int>(data);
            var set = new HashSet<int>(data);

            var random = new Random(42);
            var lookupKeys = new int[lookupCount];
            for (int i = 0; i < lookupCount; i++)
            {
                lookupKeys[i] = random.Next(0, dataSize * 2);
            }

            var (hitsList, listMs) = TimeContainsCore(list, lookupKeys);
            var (hitsSet, setMs) = TimeContainsCore(set, lookupKeys);

            return (dataSize, lookupCount, listMs, setMs, hitsList, hitsSet);
        }

        /// <summary>
        /// Times how long it takes to call Contains on a collection for each key in <paramref name="lookupKeys"/>.
        /// </summary>
        /// <param name="collection">The collection to test (for example, List&lt;int&gt; or HashSet&lt;int&gt;).</param>
        /// <param name="lookupKeys">Keys to probe using the Contains method.</param>
        /// <returns>
        /// A tuple of hit count and elapsed milliseconds.
        /// </returns>
        /// <remarks>
        /// For a list, the total time complexity is O(n * m) where n is the collection size
        /// and m is the number of lookups. For a hash set, the complexity is O(m) on average.
        /// </remarks>
        private static (int hits, long elapsedMs) TimeContainsCore(
            ICollection<int> collection,
            IEnumerable<int> lookupKeys)
        {
            int hits = 0;
            var stopwatch = Stopwatch.StartNew();

            foreach (var key in lookupKeys)
            {
                if (collection.Contains(key))
                {
                    hits++;
                }
            }

            stopwatch.Stop();
            long elapsedMs = stopwatch.ElapsedMilliseconds;
            return (hits, elapsedMs);
        }
    }
}
