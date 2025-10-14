using System;
using System.Diagnostics;

namespace DataStructuresToolkit
{
    /// <summary>
    /// Provides small, self-contained scenarios that demonstrate
    /// constant, linear, and quadratic time complexities.
    /// </summary>
    public class ComplexityTester
    {
        /// <summary>
        /// Returns the first element of the array.
        /// </summary>
        /// <param name="arr">The input array to read from.</param>
        /// <returns>The first element of the array, or 0 if empty.</returns>
        /// <remarks>
        /// <para><b>Complexity: O(1)</b></para>
        /// Accessing a single element by index is constant-time; the operation
        /// does not depend on the length of <paramref name="arr"/>.
        /// </remarks>
        public int RunConstantScenario(int[] arr)
        {
            return arr.Length > 0 ? arr[0] : 0;
        }

        /// <summary>
        /// Computes the sum of all elements.
        /// </summary>
        /// <param name="arr">The input array whose elements will be summed.</param>
        /// <returns>The total sum of all elements.</returns>
        /// <remarks>
        /// <para><b>Complexity: O(n)</b></para>
        /// The method iterates through each element exactly once; the number of
        /// operations grows linearly with the length of <paramref name="arr"/>.
        /// </remarks>
        public long RunLinearScenario(int[] arr)
        {
            long total = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                total += arr[i];
            }
            return total;
        }

        /// <summary>
        /// Touches every pair of elements in the array and returns a checksum.
        /// </summary>
        /// <param name="arr">The input array whose element pairs will be processed.</param>
        /// <returns>
        /// A checksum accumulated from visiting all (i, j) pairs (value is only to
        /// prevent optimization and prove the work happened).
        /// </returns>
        /// <remarks>
        /// <para><b>Complexity: O(n²)</b></para>
        /// Two nested loops over the same array visit every ordered pair (i, j).
        /// The total work scales with n × n.
        /// </remarks>
        public long RunQuadraticScenario(int[] arr)
        {
            long checksum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    checksum += (arr[i] + arr[j]) & 1;
                }
            }
            return checksum;
        }

        /// <summary>
        /// Measures the elapsed time (in milliseconds) to execute <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The code to run and time.</param>
        /// <returns>Elapsed time in milliseconds.</returns>
        public static long Time(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
