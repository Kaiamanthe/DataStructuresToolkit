using System;
using System.Collections.Generic;
using DataStructuresToolkit;

namespace ConsoleDevUI
{
    /// <summary>
    /// Centralized console output so Program.cs stays clean.
    /// </summary>
    public static class ConOutputHelper
    {
        /// <summary>Print a big header with an underline.</summary>
        /// <param name="title">Header text.</param>
        public static void Header(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine(new string('-', Math.Max(3, title.Length)));
        }

        /// <summary>Print a section header line with a fixed-width underline.</summary>
        /// <param name="title">Section title.</param>
        /// <param name="width">Underline width.</param>
        public static void SubHeader(string title, int width = 60)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine(new string('-', width));
        }

        /// <summary>Write a divider line of dashes.</summary>
        /// <param name="width">Number of dashes to print.</param>
        public static void Divider(int width = 60) => Console.WriteLine(new string('-', width));

        /// <summary>Print a simple Markdown table header.</summary>
        /// <param name="columns">Column labels.</param>
        public static void TableHeader(IReadOnlyList<string> columns)
        {
            Console.WriteLine($"| {string.Join(" | ", columns)} |");
            var under = new List<string>(columns.Count);
            foreach (var c in columns) under.Add(new string('-', Math.Max(3, c.Length)));
            Console.WriteLine($"| {string.Join(" | ", under)} |");
        }

        /// <summary>Print a single timing row for three sizes.</summary>
        /// <param name="name">Label.</param>
        /// <param name="t1">Small size ms.</param>
        /// <param name="t2">Medium size ms.</param>
        /// <param name="t3">Large size ms.</param>
        public static void StringRow(string name, long t1, long t2, long t3)
        {
            string c1 = t1 >= 0 ? $"{t1} ms" : "—";
            string c2 = t2 >= 0 ? $"{t2} ms" : "—";
            string c3 = t3 >= 0 ? $"{t3} ms" : "—";
            Console.WriteLine($"| {name,-6}| {c1,7} | {c2,8} | {c3,9} |");
        }

        /// <summary>Write one line.</summary>
        /// <param name="text">Text to print.</param>
        public static void Line(string text) => Console.WriteLine(text);

        /// <summary>Pretty-print an int array with a label.</summary>
        /// <param name="label">Prefix label.</param>
        /// <param name="a">Array content.</param>
        public static void Print(string label, int[] a)
        {
            Console.WriteLine($"{label}: [{string.Join(", ", a)}]");
            Divider();
        }

        /// <summary>Pretty print a string with a label.</summary>
        /// <param name="label">Prefix label.</param>
        /// <param name="s">String content.</param>
        public static void Print(string label, string s)
        {
            Console.WriteLine($"{label}: {s}");
            Divider();
        }

        /// <summary>
        /// Return the names array, adding commas after the first element.
        /// </summary>
        /// <param name="names">Names to join.</param>
        /// <returns>Shallow copy with commas prefixed after the first.</returns>
        public static string[] NamesWithCommas(string[] names)
        {
            if (names == null || names.Length == 0) return Array.Empty<string>();
            var result = new string[names.Length];
            for (int i = 0; i < names.Length; i++)
                result[i] = i == 0 ? names[i] : ", " + names[i];
            return result;
        }

        // Recursion Demo

        /// <summary>Run a batch of int→int calls and print the results.</summary>
        /// <param name="title">Title for the run.</param>
        /// <param name="inputs">Input values.</param>
        /// <param name="fn">Function under test.</param>
        /// <param name="format">Optional formatter: (n, value) → string.</param>
        public static void RunIntUnaryDemo(string title, int[] inputs, Func<int, int> fn, Func<int, int, string> format = null)
        {
            Line(title);
            foreach (var n in inputs)
            {
                try
                {
                    int val = fn(n);
                    Line(format != null ? format(n, val) : $"{n} -> {val}");
                }
                catch (Exception ex)
                {
                    var what = title?.Split(' ')[0] ?? "fn";
                    Line($"{what}({n}) -> ERROR: {ex.GetType().Name}: {ex.Message}");
                }
            }
            Line(string.Empty);
        }

        /// <summary>Run the factorial demo.</summary>
        /// <param name="inputs">n values.</param>
        public static void RunFactorialDemo(params int[] inputs) =>
            RunIntUnaryDemo("Factorial", inputs, RecursionHelper.Factorial, (n, v) => $"factorial({n}) = {v}");

        /// <summary>Run the Fibonacci demo.</summary>
        /// <param name="inputs">n values.</param>
        public static void RunFibonacciDemo(params int[] inputs) =>
            RunIntUnaryDemo("Fibonacci", inputs, RecursionHelper.Fibonacci, (n, v) => $"fib({n}) = {v}");

        /// <summary>Run SumArray tests with a few starting indices.</summary>
        /// <param name="name">Label for the array.</param>
        /// <param name="arr">Array to sum.</param>
        /// <param name="indices">Indices to test.</param>
        public static void RunSumArrayDemo(string name, int[] arr, int[] indices)
        {
            Line("SumArray Demo");
            Print(name, arr);

            foreach (var idx in indices)
            {
                try
                {
                    int val = RecursionHelper.SumArray(arr, idx);
                    Line($"SumArray({name}, index={idx}) = {val}");
                }
                catch (Exception ex)
                {
                    Line($"SumArray({name}, {idx}) -> ERROR: {ex.GetType().Name}: {ex.Message}");
                }
            }
            Line(string.Empty);
        }

        /// <summary>Run Contains tests with (index, target) pairs.</summary>
        /// <param name="name">Label for the array.</param>
        /// <param name="arr">Array to search.</param>
        /// <param name="queries">(start index, target) tuples.</param>
        public static void RunContainsDemo(string name, int[] arr, (int index, int target)[] queries)
        {
            Line("Contains Demo");
            Print(name, arr);

            foreach (var q in queries)
            {
                try
                {
                    bool found = RecursionHelper.Contains(arr, q.index, q.target);
                    Line($"Contains({name}, index={q.index}, target={q.target}) = {found}");
                }
                catch (Exception ex)
                {
                    Line($"Contains({name}, {q.index}, {q.target}) -> ERROR: {ex.GetType().Name}: {ex.Message}");
                }
            }
            Line(string.Empty);
        }

        /// <summary>Run a few palindrome checks (problem-solving recursion).</summary>
        /// <param name="inputs">Strings to test.</param>
        public static void RunPalindromeDemo(params string[] inputs)
        {
            Line("Palindrome Demo");
            foreach (var s in inputs)
            {
                try
                {
                    bool ok = RecursionHelper.IsPalindrome(s);
                    Line($"IsPalindrome(\"{s}\") = {ok}");
                }
                catch (Exception ex)
                {
                    Line($"IsPalindrome(\"{s}\") -> ERROR: {ex.GetType().Name}: {ex.Message}");
                }
            }
            Line(string.Empty);
        }

        /// <summary>Run the power set generator and print subsets (2^n growth — small n).</summary>
        /// <param name="items">Input items.</param>
        public static void RunPowerSetDemo(string[] items)
        {
            Line("PowerSet Demo");
            try
            {
                var ps = RecursionHelper.PowerSet(items);
                Line($"Input: [{string.Join(", ", items)}]");
                Line($"Subsets (count={ps.Count}):");
                for (int i = 0; i < ps.Count; i++)
                    Line($"  {i,2}: [{string.Join(", ", ps[i])}]");
            }
            catch (Exception ex)
            {
                Line($"PowerSet -> ERROR: {ex.GetType().Name}: {ex.Message}");
            }
            Line(string.Empty);
        }

        /// <summary>Run the structural recursion directory walk with a depth cap.</summary>
        /// <param name="path">Root path.</param>
        /// <param name="depth">Depth limit.</param>
        public static void RunDirTraverseDemo(string path, int depth)
        {
            Line($"TraverseDirectory Demo (depth={depth})");
            try
            {
                RecursionHelper.TraverseDirectory(path, depth, p => Line($"  {p}"));
            }
            catch (Exception ex)
            {
                Line($"TraverseDirectory(\"{path}\") -> ERROR: {ex.GetType().Name}: {ex.Message}");
            }
            Line(string.Empty);
        }
    }
}
