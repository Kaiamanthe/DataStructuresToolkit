using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert; // force NUnit Assert for this file

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class SortingSearchingHelpersTests
    {
        // LinearSearch test

        [Test]
        public void LinearSearch_FindsExistingValue_ReturnsCorrectIndex()
        {
            // Arrange
            int[] arr = { 9, 7, 5, 3, 1, 7, 0 };

            // Act
            int idx = Toolkit.SortingSearchingHelpers.LinearSearch(arr, 3);

            // Assert
            Assert.That(idx, Is.EqualTo(3), "Should return the index of the first matching element.");
        }

        [Test]
        public void LinearSearch_TargetNotPresent_ReturnsMinusOne()
        {
            // Arrange
            int[] arr = { 10, 20, 30 };

            // Act
            int idx = Toolkit.SortingSearchingHelpers.LinearSearch(arr, 25);

            // Assert
            Assert.That(idx, Is.EqualTo(-1), "Should be -1 when target is not present.");
        }

        // BinarySearch tests

        [Test]
        public void BinarySearch_FindsValueInSortedArray_ReturnsSomeValidIndex()
        {
            // Arrange
            int[] arr = { 1, 3, 3, 4, 8, 10, 12 };
            int target = 3;

            // Act
            int idx = Toolkit.SortingSearchingHelpers.BinarySearch(arr, target);

            // Assert
            Assert.That(idx, Is.InRange(0, arr.Length - 1), "Index should be within bounds.");
            Assert.That(arr[idx], Is.EqualTo(target), "The element at the returned index should equal the target.");
        }

        [Test]
        public void BinarySearch_TargetNotPresentInSortedArray_ReturnsMinusOne()
        {
            // Arrange
            int[] arr = { 2, 4, 6, 8 };

            // Act
            int idx = Toolkit.SortingSearchingHelpers.BinarySearch(arr, 5);

            // Assert
            Assert.That(idx, Is.EqualTo(-1), "Should be -1 when target does not exist.");
        }

        // BubbleSort tests

        [Test]
        public void BubbleSort_SortsUnsortedArray_WithDuplicates()
        {
            // Arrange
            int[] arr = { 5, 1, 5, 3, 2, 2 };
            int[] expected = arr.OrderBy(x => x).ToArray();

            // Act
            Toolkit.SortingSearchingHelpers.BubbleSort(arr);

            // Assert
            Assert.That(arr, Is.EqualTo(expected), "Array should be sorted ascending.");
        }

        [Test]
        public void BubbleSort_AlreadySorted_RemainsSorted()
        {
            // Arrange
            int[] arr = { 1, 2, 3, 4 };
            int[] before = (int[])arr.Clone();

            // Act
            Toolkit.SortingSearchingHelpers.BubbleSort(arr);

            // Assert
            Assert.That(arr, Is.EqualTo(before), "Already-sorted array should remain unchanged.");
        }


        // MergeSort tests

        [Test]
        public void MergeSort_SortsMixedNumbers_IncludingNegatives()
        {
            // Arrange
            int[] arr = { 0, -5, 3, -1, 2, 9, -2 };
            int[] expected = arr.OrderBy(x => x).ToArray();

            // Act
            Toolkit.SortingSearchingHelpers.MergeSort(arr);

            // Assert
            Assert.That(arr, Is.EqualTo(expected), "Array should be sorted ascending, including negatives.");
        }

        [Test]
        public void MergeSort_AlreadySorted_NoChange()
        {
            // Arrange
            int[] arr = { -2, -1, 0, 3, 5 };
            int[] before = (int[])arr.Clone();

            // Act
            Toolkit.SortingSearchingHelpers.MergeSort(arr);

            // Assert
            Assert.That(arr, Is.EqualTo(before), "Sorted input should remain the same.");
        }

        // MergeSortSplit tests

        [Test]
        public void MergeSortSplit_SortsFullRange()
        {
            // Arrange
            int[] arr = { 9, 4, 1, 6, 7, 3 };
            int[] expected = arr.OrderBy(x => x).ToArray();
            var temp = new int[arr.Length];

            // Act
            InvokePrivate("MergeSortSplit", new object[] { arr, temp, 0, arr.Length - 1 });

            // Assert
            Assert.That(arr, Is.EqualTo(expected), "Full-range MergeSortSplit should sort entire array.");
        }

        [Test]
        public void MergeSortSplit_SortsPartialRange_LeavesOutsideUntouched()
        {
            // Arrange
            // Sort only indices [2..5]
            int[] arr = { 99, 88, 5, 1, 3, 2, 77, 66 };
            int[] expected = { 99, 88, 1, 2, 3, 5, 77, 66 };
            var temp = new int[arr.Length];

            // Act
            InvokePrivate("MergeSortSplit", new object[] { arr, temp, 2, 5 });

            // Assert
            Assert.That(arr, Is.EqualTo(expected), "Only the specified subrange should be sorted.");
        }

        // Merge tests

        [Test]
        public void Merge_CombinesTwoSortedHalves_IntoSortedWhole()
        {
            // Arrange
            // arr[0..2] and arr[3..6] are individually sorted
            int[] arr = { 1, 4, 9, 0, 2, 7, 10 };
            int[] temp = new int[arr.Length];
            Array.Copy(new[] { 1, 4, 9 }, 0, arr, 0, 3);
            Array.Copy(new[] { 0, 2, 7, 10 }, 0, arr, 3, 4);

            // Act
            InvokePrivate("Merge", new object[] { arr, temp, 0, 2, 6 });

            // Assert
            int[] expected = { 0, 1, 2, 4, 7, 9, 10 };
            Assert.That(arr, Is.EqualTo(expected), "Merged array should be globally sorted.");
        }

        [Test]
        public void Merge_HandlesDuplicatesAcrossHalves_CorrectOrder()
        {
            // Arrange
            // Left: [1,1,2]  Right: [1,2,3]
            int[] arr = { 1, 1, 2, 1, 2, 3 };
            int[] temp = new int[arr.Length];

            // Act
            InvokePrivate("Merge", new object[] { arr, temp, 0, 2, 5 });

            // Assert
            int[] expected = { 1, 1, 1, 2, 2, 3 };
            Assert.That(arr, Is.EqualTo(expected), "Merged result should contain all elements in ascending order.");
        }

        // Reflection tests

        private static object InvokePrivate(string methodName, object[] args)
        {
            // Arrange
            var t = typeof(Toolkit.SortingSearchingHelpers);

            // Act
            var mi = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);

            // Assert
            Assert.That(mi, Is.Not.Null, $"Private method '{methodName}' not found via reflection.");

            return mi!.Invoke(null, args);
        }
    }
}
