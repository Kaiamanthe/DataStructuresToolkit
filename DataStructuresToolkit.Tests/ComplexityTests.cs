using System.Diagnostics;
using NUnit.Framework;
using DataStructuresToolkit;
// I had to force it to use NUnit
using Assert = NUnit.Framework.Assert;

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class ComplexityTester_Tests
    {
        private ComplexityTester _tester;

        [SetUp]
        public void Setup()
        {
            _tester = new ComplexityTester();
        }

        // O(1) – RunConstantScenario

        [Test]
        public void RunConstantScenario_Times()
        {
            int[] sizes = { 1_000, 10_000, 100_000 };

            foreach (int n in sizes)
            {
                // Arrange
                var arr = new int[n];
                Fill(arr);
                _tester.RunConstantScenario(arr); // warm-up (JIT)

                // Act
                long ms = ComplexityTester.Time(() => _tester.RunConstantScenario(arr));
                var result = _tester.RunConstantScenario(arr);

                // Assert
                TestContext.WriteLine($"O(1),  n={n}: {ms} ms");
                Assert.That(result, Is.EqualTo(0)); // first element is 0 after Fill
            }
        }

        // O(n) – RunLinearScenario

        [Test]
        public void RunLinearScenario_Times()
        {
            int[] sizes = { 1_000, 10_000, 100_000 };

            foreach (int n in sizes)
            {
                // Arrange
                var arr = new int[n];
                Fill(arr);
                _tester.RunLinearScenario(arr); // warm-up (JIT)
                long expected = ((long)n * (n - 1)) / 2; // sum of 0..(n-1)

                // Act
                long ms = ComplexityTester.Time(() => _tester.RunLinearScenario(arr));
                var result = _tester.RunLinearScenario(arr);

                // Assert
                TestContext.WriteLine($"O(n),  n={n}: {ms} ms");
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        // O(n²) – RunQuadraticScenario

        [Test]
        public void RunQuadraticScenario_Times()
        {
            // This is going to take a while
            int[] sizes = { 1_000, 10_000, 100_000 };

            foreach (int n in sizes)
            {
                // Arrange
                var arr = new int[n];
                Fill(arr);
                _tester.RunQuadraticScenario(arr);

                // Act
                long ms = ComplexityTester.Time(() => _tester.RunQuadraticScenario(arr));
                var checksum = _tester.RunQuadraticScenario(arr);

                // Assert
                TestContext.WriteLine($"O(n²), n={n}: {ms} ms");
                Assert.That(checksum, Is.GreaterThanOrEqualTo(0));
            }
        }

        // Test helper
        private static void Fill(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = i;
        }
    }
}
