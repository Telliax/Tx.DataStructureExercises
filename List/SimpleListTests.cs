using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.List
{
    [TestFixture]
    class SimpleListTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyList()
        {
            var list = CreateList();
            var expected = new List<int>();
            CollectionAssert.AreEqual(expected, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void CtorWithItems_CreatesFilledList()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new List<int>(range);
            CollectionAssert.AreEqual(expected, list);
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
            var expected = new List<int>();
            foreach (var item in Enumerable.Range(0, 100))
            {
                list.Add(item);
                expected.Add(item);
            }
            CollectionAssert.AreEqual(expected, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void Remove_Test()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            var expected = new List<int>(range);
            list.Remove(0);
            expected.Remove(0);
            list.Remove(99);
            expected.Remove(99);
            for (int i = 20; i < 30; i++)
            {
                list.Remove(i);
                expected.Remove(i);
            }
            CollectionAssert.AreEqual(expected, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void Insert_Test()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            var expected = new List<int>(range);
            list.Insert(0, 1000);
            expected.Insert(0, 1000);
            list.Insert(100, 2000);
            expected.Insert(100, 2000);
            for (int i = 20; i < 60; i+=10)
            {
                list.Insert(i, i*1000);
                expected.Insert(i, i*1000);
            }
            CollectionAssert.AreEqual(expected, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void RemoveAt_Test()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            var expected = new List<int>(range);
            list.RemoveAt(99);
            expected.RemoveAt(99);
            list.RemoveAt(0);
            expected.RemoveAt(0);
            for (int i = 20; i < 60; i += 10)
            {
                list.RemoveAt(i);
                expected.RemoveAt(i);
            }
            CollectionAssert.AreEqual(expected, list);
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void Clear_Test()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            var expected = new List<int>(range);
            list.Clear();
            expected.Clear();
            CollectionAssert.AreEqual(expected, list);
            Assert.AreEqual(expected.Count, list.Count);
        }
        
        [TestCase(0)]
        [TestCase(99)]
        [TestCase(25)]
        public void Indexer_GetTest(int index)
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            var expected = new List<int>(range);
            Assert.AreEqual(expected[index], list[index]);
        }

        [Test]
        public void Indexer_SetTest()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            var expected = new List<int>(range);
            list[0] = 1000;
            expected[0] = 1000;
            list[99] = 2000;
            expected[99] = 2000;
            for (int i = 20; i < 60; i += 10)
            {
                list[i] = i * 1000;
                expected[i] = i * 1000;
            }
            CollectionAssert.AreEqual(expected, list);
        }

        [Test]
        public void InvalidOperation_Tests()
        {
            var range = Enumerable.Range(0, 100);
            var list = CreateList(range);
            Assert.Throws<IndexOutOfRangeException>(() => list.Insert(-10, 1));
            Assert.Throws<IndexOutOfRangeException>(() => list.Insert(1000, 1));
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-10));
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(1000));
            Assert.Throws<IndexOutOfRangeException>(() => list[-10].ToString());
            Assert.Throws<IndexOutOfRangeException>(() => list[1000].ToString());
        }


        protected virtual ISimpleList<int> CreateList() => new SimpleList<int>();
        protected virtual ISimpleList<int> CreateList(IEnumerable<int> items) => new SimpleList<int>(items);
    }
}
