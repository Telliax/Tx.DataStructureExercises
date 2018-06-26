using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.Set
{
    [TestFixture]
    class SimpleHashSetTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyDict()
        {
            var dict = CreateSet();
            var expected = new HashSet<string>();
            CollectionAssert.AreEquivalent(expected, dict);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [Test]
        public void CtorWithItems_WhenNoDuplicates_CreatesFilledList()
        {
            var range = new[] { "1", "2", "3", "4", "5" };
            var set = CreateSet(range);
            var expected = new HashSet<string>(range);
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }


        [Test]
        public void CtorWithItems_WhenHasDuplicates_RemovesDuplicates()
        {
            var range = new[]{"1", "2", "3", "4", "2", "5"};
            var set = CreateSet(range);
            var expected = new HashSet<string>(range);
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }

        [Test]
        public void Add_WhenNoDuplicates_AllItemsAdded()
        {
            var range = Enumerable.Range(0, 100).Select(i => i.ToString());
            var set = CreateSet();
            var expected = new HashSet<string>();
            foreach (var item in range)
            {
                Assert.AreEqual(expected.Add(item), set.Add(item));
            }
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }

        [Test]
        public void Add_WhenHasDuplicates_RemovesDuplicates()
        {
            var range = new[] { "1", "2", "3", "4", "2", "5", "9", "2", "3", "8", "7", "1"};
            var set = CreateSet();
            var expected = new HashSet<string>();
            foreach (var item in range)
            {
                Assert.AreEqual(expected.Add(item), set.Add(item));
            }
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }

        [TestCase("1")]
        [TestCase("3")]
        [TestCase("7")]
        public void Remove_WhenItemsExist_RemovesItems(string testCase)
        {
            var range = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var set = CreateSet(range);
            var expected = new HashSet<string>(range);

            Assert.AreEqual(expected.Remove(testCase), set.Remove(testCase));
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }

        [TestCase("0")]
        [TestCase("abc")]
        public void Remove_WhenItemsDoNotExist_DoesNothing(string testCase)
        {
            var range = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var set = CreateSet(range);
            var expected = new HashSet<string>(range);

            Assert.AreEqual(expected.Remove(testCase), set.Remove(testCase));
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }

        [TestCase("1")]
        [TestCase("5")]
        [TestCase("7")]
        public void Contains_WhenItemsExist_ReturnsTrue(string testCase)
        {
            var range = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var set = CreateSet(range);
            var expected = new HashSet<string>(range);

            Assert.AreEqual(expected.Contains(testCase), set.Contains(testCase));
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }

        [TestCase("0")]
        [TestCase("abc")]
        public void Contains_WhenItemsDoNotExist_ReturnsFalse(string testCase)
        {
            var range = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var set = CreateSet(range);
            var expected = new HashSet<string>(range);

            Assert.AreEqual(expected.Contains(testCase), set.Contains(testCase));
            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
        }

        [Test]
        public void Clear_Test()
        {
            var range = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var set = CreateSet(range);
            var expected = new HashSet<string>(range);

            set.Clear();
            expected.Clear();

            CollectionAssert.AreEquivalent(expected, set);
            Assert.AreEqual(expected.Count, set.Count);
            Assert.AreEqual(expected.Contains("1"), set.Contains("1"));
        }

        [Test]
        public void Union_Test()
        {
            var range1 = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var range2 = new[] { "1", "10", "9", "8", "7" };
            var set1 = CreateSet(range1);
            var set2 = CreateSet(range2);
            var union = set1.Union(set2);
            var expected = new HashSet<string>(range1);
            expected.UnionWith(range2);

            CollectionAssert.AreEquivalent(expected, union);
            Assert.AreEqual(expected.Count, union.Count);
        }


        [Test]
        public void Intersect_Test()
        {
            var range1 = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var range2 = new[] { "1", "10", "9", "8", "7" };
            var set1 = CreateSet(range1);
            var set2 = CreateSet(range2);
            var intersection = set1.Intersect(set2);
            var expected = new HashSet<string>(range1);
            expected.IntersectWith(range2);

            CollectionAssert.AreEquivalent(expected, intersection);
            Assert.AreEqual(expected.Count, intersection.Count);
        }


        [Test]
        public void Diff_Test()
        {
            var range1 = new[] { "1", "2", "3", "4", "5", "6", "7" };
            var range2 = new[] { "1", "10", "9", "8", "7" };
            var set1 = CreateSet(range1);
            var set2 = CreateSet(range2);
            var diff = set1.Diff(set2);
            var expected = new HashSet<string>(range1);
            expected.ExceptWith(range2);

            CollectionAssert.AreEquivalent(expected, diff);
            Assert.AreEqual(expected.Count, diff.Count);
        }


        [Test]
        public void InvalidOperation_Tests()
        {
            Assert.Throws<ArgumentNullException>(() => CreateSet(null));
        }

        protected virtual ISimpleSet<string> CreateSet() => new SimpleHashSet<string>();
        protected virtual ISimpleSet<string> CreateSet(IEnumerable<string> items) => new SimpleHashSet<string>(items);
    }
}
