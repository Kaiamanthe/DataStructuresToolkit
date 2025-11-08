using DataStructuresToolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Toolkit;

namespace ConsoleDevUI
{
    class Program
    {
        static void Main()
        {
            var tester = new ComplexityTester();

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

            ConOutputHelper.Header("Search & Sort Timing Benchmarks");
            var sizes = new[] { 100, 1_000, 10_000 };

            var bubbleTimes = new List<long>(sizes.Length);
            var mergeTimes = new List<long>(sizes.Length);
            var linearTimes = new List<long>(sizes.Length);
            var binaryTimes = new List<long>(sizes.Length);

            // Sort timings (Bubble vs Merge)
            foreach (var n in sizes)
            {
                var baseArr = SortingSearchingHelpers.RandomArray(n);
                var a1 = (int[])baseArr.Clone();
                var a2 = (int[])baseArr.Clone();

                long tBubble = SortingSearchingHelpers.TimeMs(() => SortingSearchingHelpers.BubbleSort(a1));
                long tMerge = SortingSearchingHelpers.TimeMs(() => SortingSearchingHelpers.MergeSort(a2));

                bubbleTimes.Add(tBubble);
                mergeTimes.Add(tMerge);
            }

            ConOutputHelper.PrintTimingTable(
                "Sorting Timings (Bubble vs Merge)",
                sizes,
                new[]
                {
                    ("Bubble (O(n²))",        bubbleTimes[0], bubbleTimes[1], bubbleTimes[2]),
                    ("Merge  (O(n log n))",   mergeTimes[0],  mergeTimes[1],  mergeTimes[2])
                }
            );

            // Search timings (Linear vs Binary)
            const int trials = 500;
            foreach (var n in sizes)
            {
                var baseArr = SortingSearchingHelpers.RandomArray(n);
                var sorted = SortingSearchingHelpers.SortedCopy(baseArr);

                int presentTarget = sorted[n / 2];
                int missingTarget = int.MaxValue;

                long tLinear = SortingSearchingHelpers.TimeMs(() =>
                {
                    for (int t = 0; t < trials; t++)
                    {
                        if ((t & 1) == 0)
                            SortingSearchingHelpers.LinearSearch(sorted, presentTarget);
                        else
                            SortingSearchingHelpers.LinearSearch(sorted, missingTarget);
                    }
                });

                long tBinary = SortingSearchingHelpers.TimeMs(() =>
                {
                    for (int t = 0; t < trials; t++)
                    {
                        if ((t & 1) == 0)
                            SortingSearchingHelpers.BinarySearch(sorted, presentTarget);
                        else
                            SortingSearchingHelpers.BinarySearch(sorted, missingTarget);
                    }
                });

                linearTimes.Add(tLinear);
                binaryTimes.Add(tBinary);
            }

            ConOutputHelper.PrintTimingTable(
                "Search Timings (Linear vs Binary) — aggregated over multiple trials",
                sizes,
                new[]
                {
                    ("Linear (O(n))",       linearTimes[0], linearTimes[1], linearTimes[2]),
                    ("Binary (O(log n))",   binaryTimes[0], binaryTimes[1], binaryTimes[2])
                }
            );


            // Trees & BST Toolkit
            ConOutputHelper.Header("Trees & BST Toolkit Tests");

            // Teaching tree
            ConOutputHelper.SubHeader("Teaching Tree Traversals + Metrics");
            var root = DataStructuresToolkit.TreeNode.BuildTeachingTree();

            string Join(System.Collections.Generic.IEnumerable<int> xs) => string.Join(", ", xs);

            ConOutputHelper.Line($"Inorder   : {Join(DataStructuresToolkit.TreeNode.Inorder(root))}");
            ConOutputHelper.Line($"Preorder  : {Join(DataStructuresToolkit.TreeNode.Preorder(root))}");
            ConOutputHelper.Line($"Postorder : {Join(DataStructuresToolkit.TreeNode.Postorder(root))}");

            int hTeach = DataStructuresToolkit.TreeNode.Height(root);
            ConOutputHelper.Line($"Height (edges) teaching tree: {hTeach}");
            ConOutputHelper.Line($"Depth of 38: {DataStructuresToolkit.TreeNode.Depth(root, 38)}");
            ConOutputHelper.Line($"Depth of 27: {DataStructuresToolkit.TreeNode.Depth(root, 27)}");
            ConOutputHelper.Line($"Depth of 9 : {DataStructuresToolkit.TreeNode.Depth(root, 9)}");
            ConOutputHelper.Divider();

            // BST insertion & search – classic sequence
            ConOutputHelper.SubHeader("BST Insert + Contains (classic sequence)");
            var bst = new DataStructuresToolkit.Bst();
            int[] seq = { 50, 30, 70, 20, 40, 60, 80 };
            foreach (var v in seq) bst.Insert(v);

            ConOutputHelper.Line($"BST inorder (sorted): {Join(bst.Inorder())}");
            ConOutputHelper.Line($"Contains 60? {bst.Contains(60)}");
            ConOutputHelper.Line($"Contains 25? {bst.Contains(25)}");
            ConOutputHelper.Line($"BST height (edges): {bst.Height()}");
            ConOutputHelper.Divider();

            // Skewed vs. balanced height comparison
            ConOutputHelper.SubHeader("Skewed vs. Balanced-ish Height Comparison");

            // Sorted insert
            var skewed = new DataStructuresToolkit.Bst();
            int[] sortedSeq = { 10, 20, 30, 40, 50 };
            foreach (var v in sortedSeq) skewed.Insert(v);

            // Balanced insertion order of the same values
            var balanced = new DataStructuresToolkit.Bst();
            int[] balancedOrder = { 30, 20, 40, 10, 50 };
            foreach (var v in balancedOrder) balanced.Insert(v);

            ConOutputHelper.Line($"Skewed inorder   : {Join(skewed.Inorder())}");
            ConOutputHelper.Line($"Balanced inorder : {Join(balanced.Inorder())}");
            ConOutputHelper.Line($"Skewed height (edges)   : {skewed.Height()}");
            ConOutputHelper.Line($"Balanced height (edges) : {balanced.Height()}");
            ConOutputHelper.Divider();

        }

        static void Fill(int[] a)
        {
            for (int i = 0; i < a.Length; i++) a[i] = i;
        }
    }
}
