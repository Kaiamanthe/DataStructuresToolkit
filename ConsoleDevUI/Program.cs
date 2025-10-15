using DataStructuresToolkit;
using DataStructuresToolKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Console application that first runs ComplexityTester benchmarks,
/// then demonstrates ArrayStringListHelpers' methods.
/// </summary>
/// <remarks>
/// Section 1 Prints a timing table for O(1), O(n), O(n²) scenarios.
/// Section 2 Shows array insert/delete, naive vs. StringBuilder concatenation,
/// and list insertion results directly.
/// </remarks>
class Program
{
    /// <summary>
    /// Entry point. Shows complexity timings first, then helper method results.
    /// </summary>
    static void Main()
    {
        // SECTION 1: COMPLEXITY TEST RESULTS

        var tester = new ComplexityTester();

        // Create arrays for testing

        int[] arr1 = new int[1000];
        int[] arr2 = new int[10000];
        int[] arr3 = new int[100000];

        // Fill arrays with values

        Fill(arr1);
        Fill(arr2);
        Fill(arr3);

        // Header for results

        Console.WriteLine("Complexity Test");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("| Method | n=1,000 | n=10,000 | n=100,000 |");
        Console.WriteLine("|--------|---------|----------|-----------|");

        // Time each method using the shared timer in class library

        long o1_t1 = ComplexityTester.Time(() => tester.RunConstantScenario(arr1));
        long o1_t2 = ComplexityTester.Time(() => tester.RunConstantScenario(arr2));
        long o1_t3 = ComplexityTester.Time(() => tester.RunConstantScenario(arr3));

        long on_t1 = ComplexityTester.Time(() => tester.RunLinearScenario(arr1));
        long on_t2 = ComplexityTester.Time(() => tester.RunLinearScenario(arr2));
        long on_t3 = ComplexityTester.Time(() => tester.RunLinearScenario(arr3));

        long on2_t1 = ComplexityTester.Time(() => tester.RunQuadraticScenario(arr1));
        long on2_t2 = ComplexityTester.Time(() => tester.RunQuadraticScenario(arr2));
        long on2_t3 = ComplexityTester.Time(() => tester.RunQuadraticScenario(arr3)); // ~19–20s at 100k

        // Print results

        StringRow("O(1) ", o1_t1, o1_t2, o1_t3);
        StringRow("O(n) ", on_t1, on_t2, on_t3);
        StringRow("O(n²)", on2_t1, on2_t2, on2_t3);

        // SECTION 2: ARRAYSTRINGLISTHELPERS DEMONSTRATION

        Console.WriteLine();
        Console.WriteLine("ArrayStringListHelpers Demonstration");
        Console.WriteLine(new string('-', 60));

        // Arrays: Insert (O(n))
        // Fixed-size array InsertIntoArray shifts elements right and overwrites last slot

        int[] demoArray = { 1, 2, 3, 4, 5 };
        Print("Start (Array)", demoArray);
        ArrayStringListHelpers.InsertIntoArray(demoArray, index: 2, value: 99);
        Print("After InsertIntoArray O(n) at index 2 (value 99)", demoArray);

        // Arrays: Delete (O(n))
        // DeleteFromArray shifts left and sets last slot to default(int)

        ArrayStringListHelpers.DeleteFromArray(demoArray, index: 3);
        Print("After DeleteFromArray O(n) at index 3", demoArray);

        //Strings: Naive += (O(n²)) vs StringBuilder (O(n))

        string[] names = { "bob", "lorrie", "rex", "evie", "charlie", "sindy" };

        // Naive concatenation

        Stopwatch swNaive = Stopwatch.StartNew();
        string naive = ArrayStringListHelpers.ConcatenateNamesNaive(namesWithCommas(names));
        swNaive.Stop();
        Print("ConcatenateNamesNaive O(n²)", naive);
        Console.WriteLine($"Time (Naive): {swNaive.ElapsedTicks} ticks ({swNaive.ElapsedMilliseconds} ms)");
        Console.WriteLine(new string('-', 60));

        // StringBuilder concatenation

        Stopwatch swBuilder = Stopwatch.StartNew();
        string built = ArrayStringListHelpers.ConcatenateNamesBuilder(namesWithCommas(names));
        swBuilder.Stop();
        Print("ConcatenateNamesBuilder O(n)", built);
        Console.WriteLine($"Time (StringBuilder): {swBuilder.ElapsedTicks} ticks ({swBuilder.ElapsedMilliseconds} ms)");
        Console.WriteLine(new string('-', 60));

        //List<T>: Insert (O(n))

        var numList = new List<int> { 10, 20, 30, 40 };
        Console.WriteLine($"Start (List): {string.Join(", ", numList)}");
        ArrayStringListHelpers.InsertIntoList(numList, index: 2, value: 999);
        Console.WriteLine($"After InsertIntoList O(n) at index 2 (value 999): {string.Join(", ", numList)}");
        Console.WriteLine(new string('-', 60));

        // SECTION 3: STACK AND QUEUE

        Console.WriteLine();
        Console.WriteLine("STACK AND QUEUE DEMONSTRATION");
        Console.WriteLine(new string('-', 60));

        // STACK (LIFO)
        var stack = new MyStack<string>();
        Console.WriteLine("Adding items to Stack (LIFO):");
        stack.Push("First"); Console.WriteLine("Pushed: First");
        stack.Push("Second"); Console.WriteLine("Pushed: Second");
        stack.Push("Third"); Console.WriteLine("Pushed: Third");

        // Show full stack (top first) without permanently mutating it
        Console.WriteLine("\nStack contents (top first):");
        var tempStack = new List<string>();
        while (true)
        {
            try
            {
                var v = stack.Pop();      // pop to read
                Console.WriteLine(v);     // prints top to bottom
                tempStack.Add(v);
            }
            catch (InvalidOperationException)
            {
                break; // empty
            }
        }
        // restore
        for (int i = tempStack.Count - 1; i >= 0; i--) stack.Push(tempStack[i]);

        Console.WriteLine($"\nPeek (top): {stack.Peek()}");
        Console.WriteLine($"Pop: {stack.Pop()}");
        Console.WriteLine($"Pop: {stack.Pop()}");
        Console.WriteLine($"Remaining top: {stack.Peek()}");
        Console.WriteLine($"Count: {stack.Count}");
        Console.WriteLine(new string('-', 60));

        // QUEUE (FIFO)
        var queue = new MyQueue<string>();
        Console.WriteLine("Adding items to Queue (FIFO):");
        queue.Enqueue("Job1"); Console.WriteLine("Enqueued: Job1");
        queue.Enqueue("Job2"); Console.WriteLine("Enqueued: Job2");
        queue.Enqueue("Job3"); Console.WriteLine("Enqueued: Job3");

        // Show full queue (front first) without losing items
        Console.WriteLine("\nQueue contents (front first):");
        int qn = queue.Count;
        var tempQ = new List<string>(qn);
        for (int i = 0; i < qn; i++)
        {
            var v = queue.Dequeue();
            Console.WriteLine(v);
            tempQ.Add(v);
        }
        foreach (var v in tempQ) queue.Enqueue(v); // restore

        Console.WriteLine($"\nPeek (front): {queue.Peek()}");
        Console.WriteLine($"Dequeue: {queue.Dequeue()}");
        Console.WriteLine($"Dequeue: {queue.Dequeue()}");
        Console.WriteLine($"Next in line: {queue.Peek()}");
        Console.WriteLine($"Count: {queue.Count}");
        Console.WriteLine(new string('-', 60));
    }


    // Helpers (for this Program)

    /// <summary>
    /// Fills an int array with sequential values 0..n-1 (O(n)).
    /// </summary>
    static void Fill(int[] a)
    {
        for (int i = 0; i < a.Length; i++) a[i] = i;
    }

    /// <summary>
    /// Formats and prints a timing table row (O(1)).
    /// </summary>
    static void StringRow(string name, long t1, long t2, long t3)
    {
        string c1 = t1 >= 0 ? $"{t1} ms" : "—";
        string c2 = t2 >= 0 ? $"{t2} ms" : "—";
        string c3 = t3 >= 0 ? $"{t3} ms" : "—";
        Console.WriteLine($"| {name,-6}| {c1,7} | {c2,8} | {c3,9} |");
    }

    /// <summary>
    /// Prints an array with a label.
    /// </summary>
    static void Print(string label, int[] a)
    {
        Console.WriteLine($"{label}: [{string.Join(", ", a)}]");
        Console.WriteLine(new string('-', 60));
    }

    /// <summary>
    /// Prints a string with a label.
    /// </summary>
    static void Print(string label, string s)
    {
        Console.WriteLine($"{label}: {s}");
        Console.WriteLine(new string('-', 60));
    }

    /// <summary>
    /// Helper: returns a new array where each name is prefixed by a comma except the first,
    /// so concatenation produces a comma-separated string in one pass.
    /// </summary>
    private static string[] namesWithCommas(string[] names)
    {
        if (names == null || names.Length == 0) return Array.Empty<string>();
        var result = new string[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            result[i] = (i == 0) ? names[i] : ", " + names[i];
        }
        return result;
    }

}
