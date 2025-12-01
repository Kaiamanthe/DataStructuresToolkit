using System;
using System.Collections.Generic;

namespace DataStructuresToolkit
{

    /// <summary>
    /// C# built-in associative containers (Dictionary and HashSet).
    /// </summary>
    public static class AssociativeHelpers
    {
        /// <summary>
        /// Run all associative container.
        /// </summary>
        public static void RunAllAsc()
        {
            RunDictionary();
            RunHashSet();
        }

        /// <summary>
        /// Demonstrate Dictionary&lt;string, string&gt; with a simple phone book.
        /// </summary>
        /// <remarks>
        /// Average-case: O(1) lookup/insert. Worst-case: O(n) under heavy collisions.
        /// </remarks>
        public static void RunDictionary()
        {
            Console.WriteLine("Dictionary (Phone Book):");

            var phoneBook = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["Alice"] = "555-0101",
                ["Bob"] = "555-0202",
                ["Carol"] = "555-0303"
            };

            Console.WriteLine($"ContainsKey(\"Alice\"): {phoneBook.ContainsKey("Alice")}");
            Console.WriteLine($"ContainsKey(\"David\"): {phoneBook.ContainsKey("David")}");

            if (phoneBook.TryGetValue("Alice", out var aliceNum))
                Console.WriteLine($"Lookup \"Alice\" => {aliceNum}");
            else
                Console.WriteLine("Lookup \"Alice\" => (not found)");

            if (phoneBook.TryGetValue("David", out var davidNum))
                Console.WriteLine($"Lookup \"David\" => {davidNum}");
            else
                Console.WriteLine("Lookup \"David\" => (not found)");

            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrate HashSet&lt;string&gt; preventing duplicates.
        /// </summary>
        /// <remarks>
        /// Average-case: O(1) add/contains. Worst-case: O(n) under heavy collisions.
        /// </remarks>
        public static void RunHashSet()
        {
            Console.WriteLine("HashSet (No Duplicates):");

            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            Console.WriteLine($"Add(\"apple\"):  {set.Add("apple")}");
            Console.WriteLine($"Add(\"apple\"):  {set.Add("apple")}  (duplicate prevented)");
            Console.WriteLine($"Add(\"banana\"): {set.Add("banana")}");

            Console.WriteLine($"Contains(\"apple\"): {set.Contains("apple")}");
            Console.WriteLine($"Contains(\"pear\"):  {set.Contains("pear")}");

            Console.WriteLine("Set contents: " + string.Join(", ", set));
            Console.WriteLine();
        }
    }
}
