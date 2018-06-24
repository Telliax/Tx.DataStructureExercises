using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.Collection
{
    [TestFixture]
    class SimpleCollectionTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyList()
        {
            var collection = CreateCollection();
            var expected = new List<int>();
            CollectionAssert.AreEqual(expected, collection);
            Assert.AreEqual(expected.Count, collection.Count);
        }

        [Test]
        public void CtorWithItems_CreatesFilledList()
        {
            var range = Enumerable.Range(0, 10);
            var collection = CreateCollection(range);
            var expected = new List<int>(range);
            CollectionAssert.AreEqual(expected, collection);
            Assert.AreEqual(expected.Count, collection.Count);
        }

        [Test]
        public void Add_Test()
        {
            var collection = CreateCollection();
            var expected = new List<int>();
            foreach (var item in Enumerable.Range(0, 100))
            {
                collection.Add(item);
                expected.Add(item);
            }
            CollectionAssert.AreEqual(expected, collection);
            Assert.AreEqual(expected.Count, collection.Count);
        }

        [Test]
        public void Remove_Test()
        {
            var range = Enumerable.Range(0, 100);
            var collection = CreateCollection(range);
            var expected = new List<int>(range);
            collection.Remove(0);
            expected.Remove(0);
            collection.Remove(99);
            expected.Remove(99);
            for (int i = 20; i < 30; i++)
            {
                collection.Remove(i);
                expected.Remove(i);
            }
            CollectionAssert.AreEqual(expected, collection);
            Assert.AreEqual(expected.Count, collection.Count);
        }

        [Test]
        public void Clear_Test()
        {
            var range = Enumerable.Range(0, 100);
            var collection = CreateCollection(range);
            var expected = new List<int>(range);
            collection.Clear();
            expected.Clear();
            CollectionAssert.AreEqual(expected, collection);
            Assert.AreEqual(expected.Count, collection.Count);
        }

        [Test]
        public void InvalidOperation_Tests()
        {
            Assert.Throws<ArgumentNullException>(() => CreateCollection(null));
        }

        protected virtual ISimpleCollection<int> CreateCollection() => new SimpleCollection<int>();
        protected virtual ISimpleCollection<int> CreateCollection(IEnumerable<int> items) => new SimpleCollection<int>(items);
    }
}
