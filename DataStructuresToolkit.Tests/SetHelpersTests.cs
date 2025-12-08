using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using DataStructuresToolkit;

namespace DataStructuresToolkit.Tests
{
    [TestFixture]
    public class SetHelpersTests
    {
        [Test]
        public void BuildUniqueIdSet_RemovesDup_AndKeepsUnique()
        {
            // Arrange
            var ids = new[] { 1, 2, 2, 3, 3, 3, 4 };

            // Act
            var result = SetHelpers.BuildUniqueIdSet(ids);

            // Assert
            Assert.That(result, Is.EquivalentTo(new[] { 1, 2, 3, 4 }));
            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test]
        public void BuildUniqueIdSet_EmpInput_ReturnsEmpSet()
        {
            // Arrange
            var ids = Array.Empty<int>();

            // Act
            var result = SetHelpers.BuildUniqueIdSet(ids);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BuildUniqueIdSet_NullInput_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> ids = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => SetHelpers.BuildUniqueIdSet(ids));
        }

        [Test]
        public void GetSetOpResults_ComputesExpectedIntersectionUnionAndDifference()
        {
            // Arrange
            // (method creates its own example sets)

            // Act
            var results = SetHelpers.GetSetOpResults();

            // Assert
            Assert.That(results.existingItemIds, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5 }));
            Assert.That(results.newItemIds, Is.EquivalentTo(new[] { 4, 5, 6, 7 }));
            Assert.That(results.intersection, Is.EquivalentTo(new[] { 4, 5 }));
            Assert.That(results.union, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7 }));
            Assert.That(results.difference, Is.EquivalentTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void BenchListVsHashSetContainsCore_ReturnsConsistentHitsAndTiming()
        {
            // Arrange
            int dataSize = 50_000;
            int lookupCount = 5_000;

            // Act
            var bench = SetHelpers.BenchListVsHashSetContainsCore(dataSize, lookupCount);

            // Assert
            Assert.That(bench.hitsList, Is.EqualTo(bench.hitsSet));
            Assert.That(bench.listMs, Is.GreaterThanOrEqualTo(0));
            Assert.That(bench.setMs, Is.GreaterThanOrEqualTo(0));
            Assert.That(bench.listMs, Is.GreaterThanOrEqualTo(bench.setMs));
        }
    }
}
