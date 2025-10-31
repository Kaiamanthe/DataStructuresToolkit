using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using DataStructuresToolkit;

namespace DataStructuresToolkit.Tests
{
    /// <summary>
    /// Unit tests for RecursionHelper. Includes edge cases and a timeout guard
    /// so accidental infinite recursion fails fast instead of hanging.
    /// </summary>
    [TestFixture]
    [Timeout(10000)] // each test must complete within 10 seconds
    public class RecursionHelperTests
    {
        // Factorial

        [Test]
        public void Factorial_Zero_IsOne()
        {
            // Arrange
            int n = 0;

            // Act
            int result = RecursionHelper.Factorial(n);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Factorial_Positive_Small()
        {
            // Arrange
            int n1 = 1;
            int n2 = 5;

            // Act
            int r1 = RecursionHelper.Factorial(n1);
            int r2 = RecursionHelper.Factorial(n2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(r1, Is.EqualTo(1));
                Assert.That(r2, Is.EqualTo(120));
            });
        }

        [Test]
        public void Factorial_Negative_Throws()
        {
            // Arrange
            int n = -1;

            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => RecursionHelper.Factorial(n));
        }

        // Fibonacci (naive recursion)

        [Test]
        public void Fibonacci_Bases_ZeroAndOne()
        {
            // Arrange
            int n0 = 0;
            int n1 = 1;

            // Act
            int r0 = RecursionHelper.Fibonacci(n0);
            int r1 = RecursionHelper.Fibonacci(n1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(r0, Is.EqualTo(0));
                Assert.That(r1, Is.EqualTo(1));
            });
        }

        [Test]
        public void Fibonacci_KnownValues()
        {
            // Arrange
            int n5 = 5;
            int n10 = 10;

            // Act
            int r5 = RecursionHelper.Fibonacci(n5);
            int r10 = RecursionHelper.Fibonacci(n10);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(r5, Is.EqualTo(5));
                Assert.That(r10, Is.EqualTo(55));
            });
        }

        [Test]
        public void Fibonacci_Negative_Throws()
        {
            // Arrange
            int n = -3;

            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => RecursionHelper.Fibonacci(n));
        }

        // SumArray

        [Test]
        public void SumArray_EmptyArray_FromZero_IsZero()
        {
            // Arrange
            int[] arr = Array.Empty<int>();
            int index = 0;

            // Act
            int sum = RecursionHelper.SumArray(arr, index);

            // Assert
            Assert.That(sum, Is.EqualTo(0));
        }

        [Test]
        public void SumArray_NormalCases()
        {
            // Arrange
            int[] a = { 1, 2, 3 };

            // Act
            int r0 = RecursionHelper.SumArray(a, 0);
            int r2 = RecursionHelper.SumArray(a, 2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(r0, Is.EqualTo(6));
                Assert.That(r2, Is.EqualTo(3));
            });
        }

        [Test]
        public void SumArray_BadIndex_Throws()
        {
            // Arrange
            int[] a = { 1, 2, 3 };

            // Act + Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => RecursionHelper.SumArray(a, -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => RecursionHelper.SumArray(a, 4));
            });
        }

        // Contains

        [Test]
        public void Contains_FindsExisting_OrNot()
        {
            // Arrange
            int[] a = { 2, 4, 6, 8, 10 };

            // Act
            bool has4 = RecursionHelper.Contains(a, 0, 4);
            bool has7 = RecursionHelper.Contains(a, 0, 7);
            bool has10From3 = RecursionHelper.Contains(a, 3, 10);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(has4, Is.True);
                Assert.That(has7, Is.False);
                Assert.That(has10From3, Is.True);
            });
        }

        [Test]
        public void Contains_EmptyArray_IsFalse()
        {
            // Arrange
            int[] a = Array.Empty<int>();

            // Act
            bool found = RecursionHelper.Contains(a, 0, 42);

            // Assert
            Assert.That(found, Is.False);
        }

        [Test]
        public void Contains_BadIndex_Throws()
        {
            // Arrange
            int[] a = { 1, 2, 3 };

            // Act + Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => RecursionHelper.Contains(a, -1, 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => RecursionHelper.Contains(a, 4, 2));
            });
        }

        // IsPalindrome (problem-solving recursion)

        [Test]
        public void IsPalindrome_TrueCases_IgnoresCasePunct()
        {
            // Arrange
            string s1 = "racecar";
            string s2 = "A man, a plan, a canal: Panama!";
            string s3 = ""; // empty string

            // Act
            bool r1 = RecursionHelper.IsPalindrome(s1);
            bool r2 = RecursionHelper.IsPalindrome(s2);
            bool r3 = RecursionHelper.IsPalindrome(s3);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(r1, Is.True);
                Assert.That(r2, Is.True);
                Assert.That(r3, Is.True);
            });
        }

        [Test]
        public void IsPalindrome_FalseCases()
        {
            // Arrange
            string s1 = "hello";
            string s2 = "not a palindrome";

            // Act
            bool r1 = RecursionHelper.IsPalindrome(s1);
            bool r2 = RecursionHelper.IsPalindrome(s2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(r1, Is.False);
                Assert.That(r2, Is.False);
            });
        }

        [Test]
        public void IsPalindrome_Null_Throws()
        {
            // Arrange
            string s = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => RecursionHelper.IsPalindrome(s));
        }

        // PowerSet (problem-solving recursion)

        [Test]
        public void PowerSet_Empty_HasOneEmptySubset()
        {
            // Arrange
            string[] items = Array.Empty<string>();

            // Act
            var ps = RecursionHelper.PowerSet(items);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ps.Count, Is.EqualTo(1));
                Assert.That(ps[0], Is.Empty);
            });
        }

        [Test]
        public void PowerSet_TwoItems_HasFourSubsets_WithExpectedMembers()
        {
            // Arrange
            var items = new[] { "a", "b" };

            // Act
            var ps = RecursionHelper.PowerSet(items);
            var asSets = ps.Select(s => string.Join(",", s)).ToHashSet();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(ps.Count, Is.EqualTo(4));
                Assert.That(asSets.Contains(""), Is.True);          // []
                Assert.That(asSets.Contains("a"), Is.True);         // [a]
                Assert.That(asSets.Contains("b"), Is.True);         // [b]
                Assert.That(asSets.Contains("a,b") || asSets.Contains("b,a"), Is.True); // [a,b]
            });
        }

        [Test]
        public void PowerSet_ThreeItems_CountIsEight()
        {
            // Arrange
            var items = new[] { "a", "b", "c" };

            // Act
            var ps = RecursionHelper.PowerSet(items);

            // Assert
            Assert.That(ps.Count, Is.EqualTo(8));
        }

        [Test]
        public void PowerSet_Null_Throws()
        {
            // Arrange
            string[] items = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => RecursionHelper.PowerSet(items));
        }

        // TraverseDirectory (structural recursion)

        private string _rootDir;
        private string _subDir1;
        private string _subDir2;
        private string _file1;
        private string _file2;

        [SetUp]
        public void SetupTempTree()
        {
            _rootDir = Path.Combine(Path.GetTempPath(), "RecursionHelperTests_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_rootDir);

            _subDir1 = Path.Combine(_rootDir, "d1");
            _subDir2 = Path.Combine(_rootDir, "d2");
            Directory.CreateDirectory(_subDir1);
            Directory.CreateDirectory(_subDir2);

            _file1 = Path.Combine(_rootDir, "f1.txt");
            _file2 = Path.Combine(_subDir1, "f2.txt");
            File.WriteAllText(_file1, "root file");
            File.WriteAllText(_file2, "nested file");
        }

        [TearDown]
        public void CleanupTempTree()
        {
            // Cleanup (not part of AAA)
            try { if (Directory.Exists(_rootDir)) Directory.Delete(_rootDir, recursive: true); }
            catch { /* best effort cleanup */ }
        }

        [Test]
        public void TraverseDirectory_Depth0_OnlyRootVisited()
        {
            // Arrange
            var visited = new List<string>();

            // Act
            RecursionHelper.TraverseDirectory(_rootDir, depthLimit: 0, onVisit: p => visited.Add(p));

            // Assert
            Assert.That(visited, Has.Count.EqualTo(1));
            Assert.That(Path.GetFullPath(visited[0]), Is.EqualTo(Path.GetFullPath(_rootDir)));
        }

        [Test]
        public void TraverseDirectory_Depth1_RootAndImmediateChildren_NoDuplicates()
        {
            // Arrange
            var visited = new List<string>();
            var expected = new HashSet<string>(new[]
            {
                Path.GetFullPath(_rootDir),
                Path.GetFullPath(_subDir1),
                Path.GetFullPath(_subDir2),
                Path.GetFullPath(_file1),
            });

            // Act
            RecursionHelper.TraverseDirectory(_rootDir, depthLimit: 1, onVisit: p => visited.Add(p));
            var actual = visited.Select(Path.GetFullPath).ToList();

            // Assert
            Assert.Multiple(() =>
            {
                // No duplicates
                Assert.That(actual.Distinct().Count(), Is.EqualTo(actual.Count));

                // Contains all expected (order may vary)
                Assert.That(expected.IsSubsetOf(actual), Is.True);

                // Should NOT contain f2.txt (depth too shallow)
                Assert.That(actual.Any(p => p.EndsWith("f2.txt", StringComparison.OrdinalIgnoreCase)), Is.False);
            });
        }

        [Test]
        public void TraverseDirectory_NullArgs_Throw()
        {
            // Arrange
            Action<string> cb = _ => { };

            // Act + Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentNullException>(() => RecursionHelper.TraverseDirectory(null, 0, cb));
                Assert.Throws<ArgumentNullException>(() => RecursionHelper.TraverseDirectory(_rootDir, 0, null));
            });
        }
    }
}
