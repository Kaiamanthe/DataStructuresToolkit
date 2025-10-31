using System;
using System.Collections.Generic;
using System.IO;

namespace DataStructuresToolkit
{
    /// <summary>
    /// Pure recursive algorithms
    /// </summary>
    public static class RecursionHelper
    {
        /// <summary>
        /// Compute n! using straight recursion.
        /// </summary>
        /// <param name="n">Non-negative integer.</param>
        /// <returns>n! as an int.</returns>
        /// <remarks>
        /// <b>Base case:</b> n == 0 → 1. <br/>
        /// <b>Recursive case:</b> n * Factorial(n - 1). <br/>
        /// <b>Time:</b> O(n). <b>Space:</b> O(n) from call depth.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="n"/> &lt; 0.</exception>
        public static int Factorial(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be non-negative.");
            if (n == 0) return 1;
            checked { return n * Factorial(n - 1); }
        }

        /// <summary>
        /// Return the nth Fibonacci number.
        /// </summary>
        /// <param name="n">Index (0-based). Must be ≥ 0.</param>
        /// <returns>F(n), where F(0)=0 and F(1)=1.</returns>
        /// <remarks>
        /// <b>Base case:</b> n == 0 → 0, n == 1 → 1. <br/>
        /// <b>Recursive case:</b> Fibonacci(n - 1) + Fibonacci(n - 2). <br/>
        /// <b>Time:</b> ~O(φⁿ). <b>Space:</b> O(n) from call depth.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="n"/> &lt; 0.</exception>
        public static int Fibonacci(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be non-negative.");
            if (n == 0) return 0;
            if (n == 1) return 1;
            checked { return Fibonacci(n - 1) + Fibonacci(n - 2); }
        }

        /// <summary>
        /// Sum arr[index..] pure recursion.
        /// </summary>
        /// <param name="arr">Array to sum.</param>
        /// <param name="index">Start index (0..Length).</param>
        /// <returns>Total of arr[index..end].</returns>
        /// <remarks>
        /// <b>Base case:</b> index == arr.Length → 0. <br/>
        /// <b>Recursive case:</b> arr[index] + SumArray(arr, index + 1). <br/>
        /// <b>Time:</b> O(n - index). <b>Space:</b> O(n - index) from call depth.
        /// </remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="arr"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> not in [0..arr.Length].</exception>
        public static int SumArray(int[] arr, int index)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (index < 0 || index > arr.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "index must be in [0, arr.Length].");
            if (index == arr.Length) return 0;
            checked { return arr[index] + SumArray(arr, index + 1); }
        }

        /// <summary>
        /// Check recursively if <paramref name="target"/> appears in arr[index..].
        /// </summary>
        /// <param name="arr">Array to scan.</param>
        /// <param name="index">Start index (0..Length).</param>
        /// <param name="target">Value to find.</param>
        /// <returns>true if found; false otherwise.</returns>
        /// <remarks>
        /// <b>Base case:</b> index == arr.Length → false. <br/>
        /// <b>Recursive case:</b> arr[index] == target ? true : Contains(arr, index + 1, target). <br/>
        /// <b>Time:</b> O(n - index). <b>Space:</b> O(n - index) from call depth.
        /// </remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="arr"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> not in [0..arr.Length].</exception>
        public static bool Contains(int[] arr, int index, int target)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (index < 0 || index > arr.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "index must be in [0, arr.Length].");

            if (index == arr.Length) return false;
            if (arr[index] == target) return true;
            return Contains(arr, index + 1, target);
        }

        // Problem-solving Recursion

        /// <summary>
        /// Palindrome check (case/punct ignored). Pure recursion.
        /// </summary>
        /// <param name="s">String to test.</param>
        /// <returns>true if s reads the same forward/backward; otherwise false.</returns>
        /// <remarks>
        /// <b>Base case:</b> left >= right → true. <br/>
        /// <b>Recursive case:</b> skip non-alnum at ends; else compare and move inward. <br/>
        /// <b>Time:</b> O(n). <b>Space:</b> O(n) from call depth.
        /// </remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="s"/> is null.</exception>
        public static bool IsPalindrome(string s) =>
            IsPalindromeCore(s ?? throw new ArgumentNullException(nameof(s)), 0, s.Length - 1);

        /// <summary>
        /// Power set generator (all subsets) using pure recursion + backtracking.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="items">Source items.</param>
        /// <returns>List of subsets (each subset is a List&lt;T&gt;).</returns>
        /// <remarks>
        /// <b>Base case:</b> idx == items.Length → add snapshot of current subset. <br/>
        /// <b>Recursive case:</b> branch exclude/include items[idx], then backtrack. <br/>
        /// <b>Time:</b> O(n·2ⁿ). <b>Space:</b> O(n) from call depth (+ output).
        /// </remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> is null.</exception>
        public static List<List<T>> PowerSet<T>(T[] items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            var result = new List<List<T>>();
            BuildPowerSet(items, 0, new List<T>(), result);
            return result;
        }

        // Structural Recursion

        /// <summary>
        /// Traverse a directory tree using pure recursion and report each path via <paramref name="onVisit"/>.
        /// </summary>
        /// <param name="path">Root path (must exist).</param>
        /// <param name="depthLimit">How deep to go: 0 = just root, 1 = root + children, etc.</param>
        /// <param name="onVisit">Callback per path visited.</param>
        /// <remarks>
        /// <b>Base case:</b> depthLeft &lt;= 0 → visit current and stop. <br/>
        /// <b>Recursive case:</b> visit each child via recursion (dirs descend; files just visit). <br/>
        /// <b>Time:</b> O(N) over visited entries. <b>Space:</b> O(H) for recursion height.
        /// </remarks>
        /// <exception cref="ArgumentNullException">If <paramref name="path"/> or <paramref name="onVisit"/> is null.</exception>
        /// <exception cref="DirectoryNotFoundException">If <paramref name="path"/> does not exist.</exception>
        public static void TraverseDirectory(string path, int depthLimit, Action<string> onVisit)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (onVisit == null) throw new ArgumentNullException(nameof(onVisit));
            if (!Directory.Exists(path)) throw new DirectoryNotFoundException(path);

            TraverseDirCore(path, depthLimit, onVisit);
        }

        // Private Helpers

        private static bool IsPalindromeCore(string s, int left, int right)
        {
            if (left >= right) return true;

            char a = char.ToLowerInvariant(s[left]);
            char b = char.ToLowerInvariant(s[right]);

            if (!char.IsLetterOrDigit(a)) return IsPalindromeCore(s, left + 1, right);
            if (!char.IsLetterOrDigit(b)) return IsPalindromeCore(s, left, right - 1);
            if (a != b) return false;

            return IsPalindromeCore(s, left + 1, right - 1);
        }

        private static void BuildPowerSet<T>(T[] items, int idx, List<T> current, List<List<T>> result)
        {
            if (idx == items.Length)
            {
                result.Add(new List<T>(current));
                return;
            }

            // exclude
            BuildPowerSet(items, idx + 1, current, result);

            // include
            current.Add(items[idx]);
            BuildPowerSet(items, idx + 1, current, result);
            current.RemoveAt(current.Count - 1); // backtrack
        }

        private static void TraverseDirCore(string path, int depthLeft, Action<string> onVisit)
        {
            onVisit(path);
            if (depthLeft <= 0) return;

            var entries = SafeGetEntries(path);
            IterateEntries(entries, 0, depthLeft - 1, onVisit);
        }

        private static string[] SafeGetEntries(string path)
        {
            try { return Directory.GetFileSystemEntries(path); }
            catch { return Array.Empty<string>(); }
        }

        private static void IterateEntries(string[] entries, int i, int depthLeft, Action<string> onVisit)
        {
            if (entries == null || i >= entries.Length) return;

            var entry = entries[i];

            if (Directory.Exists(entry))
            {
                // TraverseDirCore visit directory once.
                TraverseDirCore(entry, depthLeft, onVisit);
            }
            else
            {
                onVisit(entry);
            }

            IterateEntries(entries, i + 1, depthLeft, onVisit);
        }
    }
}
