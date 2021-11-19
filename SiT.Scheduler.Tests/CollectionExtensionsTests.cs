namespace SiT.Scheduler.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SiT.Scheduler.Utilities;
    using TryAtSoftware.Randomizer.Core.Primitives;
    using Xunit;

    public class CollectionExtensionsTests
    {
        [Fact]
        public void OrEmptyIfNullShouldReturnTheSameCollectionIfItIsNotNull()
        {
            var collectionRandomizer = new CollectionRandomizer<string>(new StringRandomizer());
            var collection = collectionRandomizer.PrepareRandomValue();

            var result = collection.OrEmptyIfNull();
            Assert.NotNull(result);
            Assert.Same(collection, result);
        }

        [Fact]
        public void OrEmptyIfNullShouldReturnEmptyCollectionIfNullIsPassed()
        {
            var result = ((IEnumerable<object>)null).OrEmptyIfNull();
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void IgnoreNullValuesShouldThrowIfThePassedCollectionIsNull() => Assert.Throws<ArgumentNullException>(() => ((IEnumerable<object>)null).IgnoreNullValues());

        [Fact]
        public void IgnoreNullValuesShouldReturnCollectionWithNonNullableElements()
        {
            var dirtyCollection = new object[] { null, new(), new(), null, new(), null };
            var cleanCollection = dirtyCollection.IgnoreNullValues().ToList();
            Assert.NotNull(cleanCollection);
            Assert.Equal(3, cleanCollection.Count);

            foreach (var element in cleanCollection)
                Assert.NotNull(element);
        }

        [Fact]
        public void IgnoreDefaultValuesShouldThrowIfThePassedCollectionIsNull() => Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>)null).IgnoreDefaultValues());

        [Fact]
        public void IgnoreDefaultValuesShouldReturnCollectionWithElementsNotEqualToDefault()
        {
            var dirtyCollection = new[] { 0, 1, 2, 0, 3, 4, 0, 0, 5, 0 };
            var cleanCollection = dirtyCollection.IgnoreDefaultValues().ToList();
            Assert.NotNull(cleanCollection);
            Assert.Equal(5, cleanCollection.Count);

            foreach (var element in cleanCollection)
                Assert.NotEqual(default, element);
        }
    }
}