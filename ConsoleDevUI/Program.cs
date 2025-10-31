using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresToolkit;

namespace ConsoleDevUI
{
    class Program
    {
        static void Main()
        {
            var tester = new ComplexityTester();

            // Create and fill arrays for testing
            int[] arr1 = new int[1000];
            int[] arr2 = new int[10000];
            int[] arr3 = new int[100000];
            Fill(arr1); Fill(arr2); Fill(arr3);

            // Complexity Test
            ConOutputHelper.Header("Complexity Test");
            ConOutputHelper.TableHeader(new[] { "Method", "n=1,000", "n=10,000", "n=100,000" });

            long o1_t1 = ComplexityTester.Time(() => tester.RunConstantScenario(arr1));
            long o1_t2 = ComplexityTester.Time(() => tester.RunConstantScenario(arr2));
            long o1_t3 = ComplexityTester.Time(() => tester.RunConstantScenario(arr3));

            long on_t1 = ComplexityTester.Time(() => tester.RunLinearScenario(arr1));
            long on_t2 = ComplexityTester.Time(() => tester.RunLinearScenario(arr2));
            long on_t3 = ComplexityTester.Time(() => tester.RunLinearScenario(arr3));

            long on2_t1 = ComplexityTester.Time(() => tester.RunQuadraticScenario(arr1));
            long on2_t2 = ComplexityTester.Time(() => tester.RunQuadraticScenario(arr2));
            long on2_t3 = ComplexityTester.Time(() => tester.RunQuadraticScenario(arr3)); // ~19–20s at 100k

            ConOutputHelper.StringRow("O(1) ", o1_t1, o1_t2, o1_t3);
            ConOutputHelper.StringRow("O(n) ", on_t1, on_t2, on_t3);
            ConOutputHelper.StringRow("O(n²)", on2_t1, on2_t2, on2_t3);

            // ArrayStringListHelpers
            ConOutputHelper.SubHeader("ArrayStringListHelpers Demonstration");

            int[] demoArray = { 1, 2, 3, 4, 5 };
            ConOutputHelper.Print("Start (Array)", demoArray);
            ArrayStringListHelpers.InsertIntoArray(demoArray, index: 2, value: 99);
            ConOutputHelper.Print("After InsertIntoArray O(n) at index 2 (value 99)", demoArray);

            ArrayStringListHelpers.DeleteFromArray(demoArray, index: 3);
            ConOutputHelper.Print("After DeleteFromArray O(n) at index 3", demoArray);

            string[] names = { "bob", "lorrie", "rex", "evie", "charlie", "sindy" };
            var withCommas = ConOutputHelper.NamesWithCommas(names);

            var swNaive = Stopwatch.StartNew();
            string naive = ArrayStringListHelpers.ConcatenateNamesNaive(withCommas);
            swNaive.Stop();
            ConOutputHelper.Print("ConcatenateNamesNaive O(n²)", naive);
            ConOutputHelper.Line($"Time (Naive): {swNaive.ElapsedTicks} ticks ({swNaive.ElapsedMilliseconds} ms)");
            ConOutputHelper.Divider();

            var swBuilder = Stopwatch.StartNew();
            string built = ArrayStringListHelpers.ConcatenateNamesBuilder(withCommas);
            swBuilder.Stop();
            ConOutputHelper.Print("ConcatenateNamesBuilder O(n)", built);
            ConOutputHelper.Line($"Time (StringBuilder): {swBuilder.ElapsedTicks} ticks ({swBuilder.ElapsedMilliseconds} ms)");
            ConOutputHelper.Divider();

            var numList = new List<int> { 10, 20, 30, 40 };
            ConOutputHelper.Line($"Start (List): {string.Join(", ", numList)}");
            ArrayStringListHelpers.InsertIntoList(numList, index: 2, value: 999);
            ConOutputHelper.Line($"After InsertIntoList O(n) at index 2 (value 999): {string.Join(", ", numList)}");
            ConOutputHelper.Divider();

            // Stack & Queue
            ConOutputHelper.SubHeader("Stack and Queue Demo");
            var stack = new MyStack<string>();
            ConOutputHelper.Line("Adding items to Stack (LIFO):");
            stack.Push("First"); ConOutputHelper.Line("Pushed: First");
            stack.Push("Second"); ConOutputHelper.Line("Pushed: Second");
            stack.Push("Third"); ConOutputHelper.Line("Pushed: Third");

            ConOutputHelper.Line("\nStack contents (top first):");
            var tempStack = new List<string>();
            while (true)
            {
                try { var v = stack.Pop(); ConOutputHelper.Line(v); tempStack.Add(v); }
                catch (InvalidOperationException) { break; }
            }
            for (int i = tempStack.Count - 1; i >= 0; i--) stack.Push(tempStack[i]);

            ConOutputHelper.Line($"\nPeek (top): {stack.Peek()}");
            ConOutputHelper.Line($"Pop: {stack.Pop()}");
            ConOutputHelper.Line($"Pop: {stack.Pop()}");
            ConOutputHelper.Line($"Remaining top: {stack.Peek()}");
            ConOutputHelper.Line($"Count: {stack.Count}");
            ConOutputHelper.Divider();

            var queue = new MyQueue<string>();
            ConOutputHelper.Line("Adding items to Queue (FIFO):");
            queue.Enqueue("Job1"); ConOutputHelper.Line("Enqueued: Job1");
            queue.Enqueue("Job2"); ConOutputHelper.Line("Enqueued: Job2");
            queue.Enqueue("Job3"); ConOutputHelper.Line("Enqueued: Job3");

            ConOutputHelper.Line("\nQueue contents (front first):");
            int qn = queue.Count;
            var tempQ = new List<string>(qn);
            for (int i = 0; i < qn; i++) { var v = queue.Dequeue(); ConOutputHelper.Line(v); tempQ.Add(v); }
            foreach (var v in tempQ) queue.Enqueue(v);

            ConOutputHelper.Line($"\nPeek (front): {queue.Peek()}");
            ConOutputHelper.Line($"Dequeue: {queue.Dequeue()}");
            ConOutputHelper.Line($"Dequeue: {queue.Dequeue()}");
            ConOutputHelper.Line($"Next in line: {queue.Peek()}");
            ConOutputHelper.Line($"Count: {queue.Count}");
            ConOutputHelper.Divider();

            // Recursion
            ConOutputHelper.SubHeader("Recursion Demos");
            ConOutputHelper.RunFactorialDemo(0, 1, 5, 10);
            ConOutputHelper.RunFibonacciDemo(0, 1, 5, 10);

            var arr = new[] { 2, 4, 6, 8, 10 };
            ConOutputHelper.RunSumArrayDemo("arr", arr, new[] { 0, 2, 5 }); // 5 shows error path
            var queries = new (int index, int target)[] { (0, 8), (0, 7), (3, 10) };
            ConOutputHelper.RunContainsDemo("arr", arr, queries);

            // Problem-solving recursion
            ConOutputHelper.RunPalindromeDemo("racecar", "A man, a plan, a canal: Panama!", "not a palindrome");
            ConOutputHelper.RunPowerSetDemo(new[] { "a", "b", "c" });

            // Structural recursion
            ConOutputHelper.RunDirTraverseDemo(Environment.CurrentDirectory, depth: 1);
        }

        static void Fill(int[] a)
        {
            for (int i = 0; i < a.Length; i++) a[i] = i;
        }
    }
}
