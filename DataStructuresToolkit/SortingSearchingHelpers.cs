using System;
using System.Diagnostics;

namespace Toolkit
{
    /// <summary>
    /// Search and sort algorithms w/simple timing.
    /// </summary>
    public static class SortingSearchingHelpers
    {
        /// <summary>
        /// Performs a linear search <paramref name="target"/> in <paramref name="arr"/>.
        /// </summary>
        /// <param name="arr">Array to scan (unsorted OK).</param>
        /// <param name="target">Value to find.</param>
        /// <returns>Index of the first occurrence of <paramref name="target"/>; -1 if not found.</returns>
        /// <remarks>
        /// Time: O(n) worst/average, O(1) best-case if at index 0. Space: O(1).
        /// </remarks>
        public static int LinearSearch(int[] arr, int target)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target) return i;
            }
            return -1;
        }

        /// <summary>
        /// Performs binary search for <paramref name="target"/> in sorted array.
        /// </summary>
        /// <param name="arr">Array sorted in non-decreasing order.</param>
        /// <param name="target">Value to find.</param>
        /// <returns>Index of <paramref name="target"/> if present; otherwise -1.</returns>
        /// <remarks>
        /// Time: O(log n) worst/average. Space: O(1).
        /// Requires input to be sorted, otherwise results are undefined.
        /// </remarks>
        public static int BinarySearch(int[] arr, int target)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            int lo = 0, hi = arr.Length - 1;
            while (lo <= hi)
            {
                int mid = lo + ((hi - lo) / 2);
                if (arr[mid] == target) return mid;
                if (arr[mid] < target) lo = mid + 1;
                else hi = mid - 1;
            }
            return -1;
        }

        /// <summary>
        /// In-place Bubble Sort (stable) on <paramref name="arr"/>.
        /// </summary>
        /// <param name="arr">Array to sort (modified in place).</param>
        /// <remarks>
        /// Time: O(n²) worst/average, O(n) best (already sorted with early exit). Space: O(1).
        /// not intended for large n.
        /// </remarks>
        public static void BubbleSort(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            int n = arr.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break; // already sorted
            }
        }

        /// <summary>
        /// In-place Merge Sort (top-down).
        /// </summary>
        /// <param name="arr">Array to sort (modified in place).</param>
        /// <remarks>
        /// Time: O(n log n) worst/average/best. Space: O(n) auxiliary.
        /// Efficient and stable, much faster than Bubble/Insertion/Selection for large n.
        /// </remarks>
        public static void MergeSort(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length < 2) return;
            var temp = new int[arr.Length];
            MergeSortSplit(arr, temp, 0, arr.Length - 1);
        }

        private static void MergeSortSplit(int[] arr, int[] temp, int lo, int hi)
        {
            if (lo >= hi) return;
            int mid = lo + ((hi - lo) / 2);
            MergeSortSplit(arr, temp, lo, mid);
            MergeSortSplit(arr, temp, mid + 1, hi);
            if (arr[mid] <= arr[mid + 1]) return; // already in order
            Merge(arr, temp, lo, mid, hi);
        }

        private static void Merge(int[] arr, int[] temp, int lo, int mid, int hi)
        {
            int i = lo, j = mid + 1, k = lo;
            while (i <= mid && j <= hi)
            {
                if (arr[i] <= arr[j]) temp[k++] = arr[i++];
                else temp[k++] = arr[j++];
            }
            while (i <= mid) temp[k++] = arr[i++];
            while (j <= hi) temp[k++] = arr[j++];
            for (int p = lo; p <= hi; p++) arr[p] = temp[p];
        }

        /// <summary>
        /// Create a new array of length <paramref name="n"/> filled with random integers.
        /// </summary>
        /// <param name="n">Length of the array.</param>
        /// <returns>Randomly filled array.</returns>
        /// <remarks>
        /// Not part of algorithm performance, helper for generating test data.
        /// </remarks>
        public static int[] RandomArray(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
            var a = new int[n];
            var rnd = new Random(123456); // fixed seed for reproducibility
            for (int i = 0; i < n; i++) a[i] = rnd.Next();
            return a;
        }

        /// <summary>
        /// Returns a sorted copy (ascending) of provided array.
        /// </summary>
        /// <param name="arr">Source array.</param>
        /// <returns>New sorted array.</returns>
        /// <remarks>
        /// Uses Array.Copy and Array.Sort for convenience, not part of the assignment algorithms.
        /// </remarks>
        public static int[] SortedCopy(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            var copy = new int[arr.Length];
            Array.Copy(arr, copy, arr.Length);
            Array.Sort(copy);
            return copy;
        }

        /// <summary>
        /// Measure the elapsed ms to execute an <see cref="Action"/>.
        /// </summary>
        /// <param name="action">Work to time.</param>
        /// <returns>Elapsed ms.</returns>
        /// <remarks>
        /// Uses <see cref="Stopwatch"/>, discard the result when comparing only relative speed.
        /// </remarks>
        public static long TimeMs(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
