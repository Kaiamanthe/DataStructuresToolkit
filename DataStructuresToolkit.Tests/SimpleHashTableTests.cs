using DataStructuresToolkit;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class SimpleHashTableTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_SizeLessThanOne_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int size = 0;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SimpleHashTable(size));
        }

        [Test]
        public void Insert_ThenContains_ReturnsTrueForExistingKey()
        {
            // Arrange
            var table = new SimpleHashTable(5);
            int key = 12;

            // Act
            table.Insert(key);

            // Assert
            ClassicAssert.IsTrue(table.Contains(key));
        }

        [Test]
        public void Contains_MissingKey_ReturnsFalse()
        {
            // Arrange
            var table = new SimpleHashTable(5);
            int existing = 12;
            int missing = 99;
            table.Insert(existing);

            // Act
            bool result = table.Contains(missing);

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Insert_DuplicateKey_TableStillContainsKey()
        {
            // Arrange
            var table = new SimpleHashTable(5);
            int key = 22;

            // Act
            table.Insert(key);
            table.Insert(key); // duplicate insert

            // Assert
            ClassicAssert.IsTrue(table.Contains(key));
        }

        [Test]
        public void Insert_NegativeKey_ContainsReturnsTrue()
        {
            // Arrange
            var table = new SimpleHashTable(5);
            int key = -7;

            // Act
            table.Insert(key);

            // Assert
            ClassicAssert.IsTrue(table.Contains(key));
        }

        [Test]
        public void PrintTable_EmptyTable_WritesExpectedHeader()
        {
            // Arrange
            var table = new SimpleHashTable(3);
            using var writer = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(writer);

            // Act
            table.PrintTable();
            Console.SetOut(originalOut);

            // Assert
            string output = writer.ToString();
            StringAssert.Contains("Hash Table Buckets:", output);
            StringAssert.Contains("[0]", output);
            StringAssert.Contains("[1]", output);
            StringAssert.Contains("[2]", output);
        }

        [Test]
        public void PrintTable_WithValues_ShowsChainedValues()
        {
            // Arrange
            var table = new SimpleHashTable(5);
            table.Insert(12); // 12 % 5 = 2
            table.Insert(22); // 22 % 5 = 2 (collision)
            using var writer = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(writer);

            // Act
            table.PrintTable();
            Console.SetOut(originalOut);

            // Assert
            string output = writer.ToString();
            StringAssert.Contains("[2]", output);
            StringAssert.Contains("12", output);
            StringAssert.Contains("22", output);
        }
    }
}
