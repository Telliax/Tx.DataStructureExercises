using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.SortedList
{
    [TestFixture]
    class SimpleSortedListTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyList()
        {
            var list = CreateList();
            var expected = new SortedList<int, int>();
            CollectionAssert.AreEqual(expected.Values, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void CtorWithItems_CreatesFilledList()
        {
            var range = Enumerable.Range(0, 100).Select(i => i % 3 == 0 ? -i : i);
            var list = CreateList(range);
            var expected = new SortedList<int, int>(range.ToDictionary(i => i, i => i));
            CollectionAssert.AreEqual(expected.Values, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void CtorWithItems_ThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => CreateList(null));
        }

        [Test]
        public void Add_Test()
        {
            var list = CreateList();
            var expected = new SortedList<int, int>();
            foreach (var item in Enumerable.Range(0, 100).Select(i => i % 3 == 0 ? -i : i))
            {
                list.Add(item);
                expected.Add(item, item);
            }
            CollectionAssert.AreEqual(expected.Values, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void Remove_Test()
        {
            var range = Enumerable.Range(0, 100).Select(i => i % 3 == 0 ? -i : i);
            var list = CreateList(range);
            var expected = new SortedList<int, int>(range.ToDictionary(i => i, i => i));
            list.Remove(0);
            expected.Remove(0);
            list.Remove(99);
            expected.Remove(99);
            for (int i = 20; i < 30; i++)
            {
                list.Remove(i);
                expected.Remove(i);
            }
            CollectionAssert.AreEqual(expected.Values, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void RemoveAt_Test()
        {
            var range = Enumerable.Range(0, 100).Select(i => i % 3 == 0 ? -i : i);
            var list = CreateList(range);
            var expected = new SortedList<int, int>(range.ToDictionary(i => i, i => i));
            list.RemoveAt(99);
            expected.RemoveAt(99);
            list.RemoveAt(0);
            expected.RemoveAt(0);
            for (int i = 20; i < 60; i += 10)
            {
                list.RemoveAt(i);
                expected.RemoveAt(i);
            }
            CollectionAssert.AreEqual(expected.Values, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void Clear_Test()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            var expected = new SortedList<int, int>(range.ToDictionary(i => i, i => i));
            list.Clear();
            expected.Clear();
            CollectionAssert.AreEqual(expected.Values, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [TestCase(0)]
        [TestCase(99)]
        [TestCase(25)]
        public void Indexer_GetTest(int index)
        {
            var range = Enumerable.Range(0, 100).Reverse();
            var list = CreateList(range);
            var expected = new SortedList<int,int>(range.ToDictionary(i => i, i => i));
            Assert.AreEqual(expected[index], list[index]);
        }

        [TestCase]
        [TestCase(50)]
        [TestCase(2)]
        [TestCase(2, 50)]
        [TestCase(50, 55)]
        [TestCase(22, 50, 55)]
        [TestCase(2, 45, 50, 66, 77, 88)]
        [TestCase(2, 3, 4, 5, 6, 7, 8, 9, 50)]
        [TestCase(50, 51, 52, 53, 77, 88, 99, 100, 111)]
        [TestCase(1, 5, 22, 44, 50, 66, 77, 88, 99, 111, 222, 333, 444)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 50, 66, 77)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 66, 77)]
        public void BinarySearch_Test(params int[] testCase)
        {
            var expected = new List<int>(testCase);
            var actual = new SimpleSortedList<int>(testCase);
            var expectedIndex = expected.BinarySearch(50);
            var actualIndex = actual.BinarySearch(50);
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public void InvalidOperation_Tests()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-10));
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(1000));
            Assert.Throws<IndexOutOfRangeException>(() => list[-10].ToString());
            Assert.Throws<IndexOutOfRangeException>(() => list[1000].ToString());
        }

        protected virtual ISimpleSortedList<int> CreateList() => new SimpleSortedList<int>();
        protected virtual ISimpleSortedList<int> CreateList(IEnumerable<int> items) => new SimpleSortedList<int>(items);
    }
}
