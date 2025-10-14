using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresToolkit
{
    /// <summary>
    /// Provides helper methods for demonstrating array, list, and string operations.
    /// </summary>
    /// <remarks>
    /// Each method here illustrates a different time complexity pattern (O(1), O(n), O(n²)).
    /// </remarks>
    public class ArrayStringListHelpers
    {
        /// <summary>
        /// Inserts a value into a fixed-size integer array at a specified index.
        /// </summary>
        /// <param name="arr">The integer array where the insertion will occur.</param>
        /// <param name="index">The index at which to insert the new value.</param>
        /// <param name="value">The integer value to insert.</param>
        /// <remarks>
        /// Time Complexity: O(n).  
        /// Elements must be shifted one position to the right starting from the end of the array 
        /// until the target index, making the operation linear with respect to array length.
        /// </remarks>
        public static void InsertIntoArray(int[] arr, int index, int value)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length == 0) throw new InvalidOperationException("Cannot insert into an empty array.");
            if (index < 0 || index >= arr.Length) throw new ArgumentOutOfRangeException(nameof(index));

            for (int i = arr.Length - 1; i > index; i--)
            {
                arr[i] = arr[i - 1];
            }

            arr[index] = value;
        }

        /// <summary>
        /// Deletes an element from a fixed-size array by shifting all subsequent elements left.
        /// </summary>
        /// <param name="arr">The integer array from which to delete an element.</param>
        /// <param name="index">The index of the element to remove.</param>
        /// <remarks>
        /// Time Complexity: O(n).  
        /// All elements after the specified index must be shifted one position left, resulting in linear time complexity.
        /// </remarks>
        public static void DeleteFromArray(int[] arr, int index)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            if (arr.Length == 0) throw new InvalidOperationException("Cannot delete from an empty array.");
            if (index < 0 || index >= arr.Length) throw new ArgumentOutOfRangeException(nameof(index));

            for (int i = index; i < arr.Length - 1; i++)
            {
                arr[i] = arr[i + 1];
            }

            arr[^1] = default;
        }

        /// <summary>
        /// Concatenates an array of strings using the <c>+=</c> operator in a loop.
        /// </summary>
        /// <param name="names">An array of strings to concatenate.</param>
        /// <returns>A single concatenated string containing all names.</returns>
        /// <remarks>
        /// Time Complexity: O(n²) in the worst case.  
        /// Each concatenation creates a new string, causing repeated copying of characters, 
        /// which increases runtime quadratically as the number of strings grows.
        /// </remarks>
        public static string ConcatenateNamesNaive(string[] names)
        {
            if (names == null) return string.Empty;

            string result = string.Empty;
            foreach (var name in names)
            {
                if (name == null) continue;
                result += name;
            }
            return result;
        }

        /// <summary>
        /// Concatenates an array of strings using a <see cref="StringBuilder"/> for efficiency.
        /// </summary>
        /// <param name="names">An array of strings to concatenate.</param>
        /// <returns>A single concatenated string containing all names.</returns>
        /// <remarks>
        /// Time Complexity: O(n).  
        /// Using a mutable <see cref="StringBuilder"/> avoids creating new string objects 
        /// for each concatenation, resulting in linear performance proportional to total characters appended.
        /// </remarks>
        public static string ConcatenateNamesBuilder(string[] names)
        {
            if (names == null) return string.Empty;

            var sb = new StringBuilder();
            foreach (var name in names)
            {
                if (name == null) continue;
                sb.Append(name);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Inserts a value into a <see cref="List{T}"/> of integers at a specific index.
        /// </summary>
        /// <param name="list">The list into which the value will be inserted.</param>
        /// <param name="index">The zero-based index at which to insert the value.</param>
        /// <param name="value">The integer value to insert into the list.</param>
        /// <remarks>
        /// Time Complexity: O(n) when inserting in the middle of the list.  
        /// Internally, the list shifts elements after the insertion index, requiring linear time.
        /// </remarks>
        public static void InsertIntoList(List<int> list, int index, int value)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (index < 0 || index > list.Count) throw new ArgumentOutOfRangeException(nameof(index));

            list.Insert(index, value);
        }
    }
}
