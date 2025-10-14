using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using DataStructuresToolkit;

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class ArrayStringListHelpers_Tests
    {
        // InsertIntoArray

        [Test]
        public void InsertIntoArray_Correctness_InsertsAndShiftsRight()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };

            ArrayStringListHelpers.InsertIntoArray(arr, index: 2, value: 99);

            Assert.That(arr, Is.EqualTo(new[] { 1, 2, 99, 3, 4 }),
                "Array should reflect right-shift and inserted value.");
        }

        [Test]
        public void InsertIntoArray_Performance_SmokeTiming()
        {
            foreach (int n in new[] { 1_000, 10_000, 50_000 })
            {
                int[] arr = Enumerable.Range(0, n).ToArray();
                int mid = n / 2;

                var sw = Stopwatch.StartNew();
                ArrayStringListHelpers.InsertIntoArray(arr, mid, 123);
                sw.Stop();

                TestContext.WriteLine($"InsertIntoArray n={n}: {sw.ElapsedTicks} ticks");

                Assert.That(arr[mid], Is.EqualTo(123), "Inserted value should be at the target index.");
                if (mid + 1 < arr.Length)
                    Assert.That(arr[mid + 1], Is.EqualTo(mid),
                        "Element after insert index should be the original element previously at index mid.");
            }
        }

        // DeleteFromArray

        [Test]
        public void DeleteFromArray_Correctness_ShiftsLeftAndClearsLast()
        {
            var arr = new[] { 1, 2, 99, 3, 4 };

            ArrayStringListHelpers.DeleteFromArray(arr, index: 3);

            Assert.That(arr, Is.EqualTo(new[] { 1, 2, 99, 4, 0 }),
                "Array should shift left and clear the last slot.");
        }

        [Test]
        public void DeleteFromArray_Performance_SmokeTiming()
        {
            foreach (int n in new[] { 1_000, 10_000, 50_000 })
            {
                int[] arr = Enumerable.Range(0, n).ToArray();
                int mid = n / 2;

                var sw = Stopwatch.StartNew();
                ArrayStringListHelpers.DeleteFromArray(arr, mid);
                sw.Stop();

                TestContext.WriteLine($"DeleteFromArray n={n}: {sw.ElapsedTicks} ticks");

                int expectedAtMid = (mid + 1 < n) ? mid + 1 : 0;
                Assert.That(arr[mid], Is.EqualTo(expectedAtMid),
                    "Array should shift elements left from the deletion point.");
                Assert.That(arr[^1], Is.EqualTo(0), "Last slot should be cleared (default).");
            }
        }

        // ConcatenateNamesNaive

        [Test]
        public void ConcatenateNamesNaive_Correctness_JoinsAllStrings()
        {
            string[] names = { "Alice", "Bob", "Charlie" };
            string result = ArrayStringListHelpers.ConcatenateNamesNaive(names);
            Assert.That(result, Is.EqualTo("AliceBobCharlie"));
        }

        [Test]
        public void ConcatenateNamesNaive_Performance_SmokeTiming()
        {
            foreach (int n in new[] { 1_000, 10_000, 50_000 })
            {
                string[] names = Enumerable.Repeat("x", n).ToArray();

                var sw = Stopwatch.StartNew();
                string result = ArrayStringListHelpers.ConcatenateNamesNaive(names);
                sw.Stop();

                TestContext.WriteLine($"ConcatenateNamesNaive n={n}: {sw.ElapsedTicks} ticks");
                Assert.That(result.Length, Is.EqualTo(n));
            }
        }

        // ConcatenateNamesBuilder

        [Test]
        public void ConcatenateNamesBuilder_Correctness_JoinsAllStrings()
        {
            string[] names = { "Alice", "Bob", "Charlie" };
            string result = ArrayStringListHelpers.ConcatenateNamesBuilder(names);
            Assert.That(result, Is.EqualTo("AliceBobCharlie"));
        }

        [Test]
        public void ConcatenateNamesBuilder_Performance_SmokeTiming()
        {
            foreach (int n in new[] { 1_000, 10_000, 50_000 })
            {
                string[] names = Enumerable.Repeat("x", n).ToArray();

                var sw = Stopwatch.StartNew();
                string result = ArrayStringListHelpers.ConcatenateNamesBuilder(names);
                sw.Stop();

                TestContext.WriteLine($"ConcatenateNamesBuilder n={n}: {sw.ElapsedTicks} ticks");
                Assert.That(result.Length, Is.EqualTo(n));
            }
        }

        [Test]
        public void Concatenate_Consistency_NaiveAndBuilderMatch()
        {
            string[] names = { "bob", "lorrie", "rex", "evie", "charlie", "sindy" };
            string naive = ArrayStringListHelpers.ConcatenateNamesNaive(names);
            string built = ArrayStringListHelpers.ConcatenateNamesBuilder(names);
            Assert.That(built, Is.EqualTo(naive));
        }

        // InsertIntoList

        [Test]
        public void InsertIntoList_Correctness_InsertsAtIndex()
        {
            var list = new List<int> { 10, 20, 30, 40 };

            ArrayStringListHelpers.InsertIntoList(list, index: 2, value: 999);

            Assert.That(list, Is.EqualTo(new[] { 10, 20, 999, 30, 40 }));
        }

        [Test]
        public void InsertIntoList_Performance_SmokeTiming()
        {
            foreach (int n in new[] { 1_000, 10_000, 50_000 })
            {
                var list = Enumerable.Range(0, n).ToList();
                int mid = n / 2;

                var sw = Stopwatch.StartNew();
                ArrayStringListHelpers.InsertIntoList(list, mid, 123);
                sw.Stop();

                TestContext.WriteLine($"InsertIntoList n={n}: {sw.ElapsedTicks} ticks");

                Assert.That(list[mid], Is.EqualTo(123));
                Assert.That(list.Count, Is.EqualTo(n + 1));
            }
        }
    }
}
