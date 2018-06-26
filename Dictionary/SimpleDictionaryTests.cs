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
        public void Indexer_WhenKeyDoesNotExists_AddsValue()
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
        public void Indexer_WhenKeyExists_UpdatesValue()
        {
            var dict = CreateDictionary();
            var expected = new Dictionary<string, int>();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict[key] = i;
                expected[key] = i;
            }
            foreach (var i in Enumerable.Range(0, 10))
            {
                var key = i.ToString();
                dict[key] = i * 10;
                expected[key] = i * 10;
                Assert.AreEqual(expected[key], dict[key]);
            }

            CollectionAssert.AreEquivalent(expected, dict);
            CollectionAssert.AreEquivalent(expected.Keys, dict.Keys);
            CollectionAssert.AreEquivalent(expected.Values, dict.Values);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [Test]
        public void Add_WhenKeyDoesNotExists_AddsValue()
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

        [TestCase("0")]
        [TestCase("7")]
        [TestCase("99")]
        public void Add_WhenKeyExists_Throws(string testKey)
        {
            var dict = CreateDictionary();
            foreach (var i in Enumerable.Range(0, 100))
            {
                var key = i.ToString();
                dict.Add(key, i);
            }
            Assert.Throws<InvalidOperationException>(() => dict.Add(testKey, 2));
        }

        [TestCase("0")]
        [TestCase("7")]
        [TestCase("99")]
        public void Remove_WhenKeyExists_RemovesKey(string testKey)
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

            Assert.AreEqual(expected.ContainsKey(testKey), dict.Contains(testKey));
            CollectionAssert.AreEquivalent(expected, dict);
            CollectionAssert.AreEquivalent(expected.Keys, dict.Keys);
            CollectionAssert.AreEquivalent(expected.Values, dict.Values);
            Assert.AreEqual(expected.Count, dict.Count);
        }

        [TestCase("1000")]
        [TestCase("absdc")]
        public void Remove_WhenKeyDoesNotExist_DoesNothing(string testKey)
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
        public void TryGetValue_WhenKeyExists_ReturnsTrueAndValue(string testKey)
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

        [TestCase("100")]
        [TestCase("abc")]
        public void TryGetValue_WhenKeyDoesNotExist_ReturnsFalseAndDefault(string testKey)
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
        public void Contains_WhenKeyExists_ReturnsTrue(string testKey)
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

        [TestCase("100")]
        [TestCase("abc")]
        public void Contains_WhenKeyDoesNotExist_ReturnsFalse(string testKey)
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
