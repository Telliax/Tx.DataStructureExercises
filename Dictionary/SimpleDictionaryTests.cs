using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.Dictionary
{
    [TestFixture]
    class SimpleDictionaryTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyDict()
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            CollectionAssert.AreEqual(expected, dict);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [Test]
        public void Indexer_NewValuesTest()
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict[key] = i;
                expected[key] = i;
                Assert.AreEqual(expected[key], dict[key]);
            }

            CollectionAssert.AreEquivalent(expected, dict);
            CollectionAssert.AreEquivalent(expected.Keys, dict.Keys);
            CollectionAssert.AreEquivalent(expected.Values, dict.Values);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [Test]
        public void Indexer_ValueOverrideTest()
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            dict["1"] = 1;
            expected["1"] = 1;
            dict["1"] = 2;
            expected["1"] = 2;

            Assert.AreEqual(dict["1"], expected["1"]);
            CollectionAssert.AreEquivalent(expected, dict);
            CollectionAssert.AreEquivalent(expected.Keys, dict.Keys);
            CollectionAssert.AreEquivalent(expected.Values, dict.Values);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [Test]
        public void Add_Test()
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict.Add(key, i);
                expected.Add(key, i);
            }

            CollectionAssert.AreEquivalent(expected, dict);
            CollectionAssert.AreEquivalent(expected.Keys, dict.Keys);
            CollectionAssert.AreEquivalent(expected.Values, dict.Values);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [Test]
        public void Remove_ExistingKeys_AreRemoved()
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict[key] = i;
                expected[key] = i;
            }

            foreach (var i in Enumerable.Range(20, 10))
            {
                var key = i.ToString();
                dict.Remove(key);
                expected.Remove(key);
                Assert.AreEqual(expected.ContainsKey(key), dict.Contains(key));
            }

            CollectionAssert.AreEquivalent(expected, dict);
            CollectionAssert.AreEquivalent(expected.Keys, dict.Keys);
            CollectionAssert.AreEquivalent(expected.Values, dict.Values);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [TestCase("1000")]
        [TestCase("absdc")]
        public void Remove_NonExistingKeys_DoNotChangeDictionary(string testKey)
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict[key] = i;
                expected[key] = i;
            }

            dict.Remove(testKey);
            expected.Remove(testKey);

            CollectionAssert.AreEquivalent(expected, dict);
            CollectionAssert.AreEquivalent(expected.Keys, dict.Keys);
            CollectionAssert.AreEquivalent(expected.Values, dict.Values);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [TestCase("0")]
        [TestCase("25")]
        [TestCase("77")]
        [TestCase("99")]
        [TestCase("100")]
        [TestCase("abc")]
        public void TryGetValue_Test(string testKey)
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict.Add(key, i);
                expected.Add(key, i);
            }

            Assert.AreEqual(expected.TryGetValue(testKey, out var expectedValue),
                            dict.TryGetValue(testKey, out var actualValue));
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("0")]
        [TestCase("25")]
        [TestCase("77")]
        [TestCase("99")]
        [TestCase("100")]
        [TestCase("abc")]
        public void Contains_Test(string testKey)
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict.Add(key, i);
                expected.Add(key, i);
            }

            Assert.AreEqual(expected.ContainsKey(testKey), dict.Contains(testKey));
        }

        [Test]
        public void Clear_Test()
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict.Add(key, i);
                expected.Add(key, i);
            }

            dict.Clear();
            expected.Clear();

            CollectionAssert.AreEqual(expected, dict);
            Assert.AreEqual(expected.Count, dict.Count);
            Assert.AreEqual(expected.ContainsKey("1"), dict.Contains("1"));
        }


        [Test]
        public void InvalidOperation_Tests()
        {
            var dict = CreateDictionary();
            dict.Add("1", 1);
            Assert.Throws<InvalidOperationException>(() => dict.Add("1", 2));
            Assert.Throws<KeyNotFoundException>(() => dict["3"].ToString());
            Assert.Throws<ArgumentNullException>(() => dict[null].ToString());
            Assert.Throws<ArgumentNullException>(() => dict[null] = 2);
            Assert.Throws<ArgumentNullException>(() => dict.Add(null, 2));
            Assert.Throws<ArgumentNullException>(() => dict.Remove(null));
            Assert.Throws<ArgumentNullException>(() => dict.Contains(null));
            Assert.Throws<ArgumentNullException>(() => dict.TryGetValue(null, out var value));
        }

        protected virtual ISimpleDictionary<string, int> CreateDictionary() => new SimpleDictionary<string, int>();
    }
}
