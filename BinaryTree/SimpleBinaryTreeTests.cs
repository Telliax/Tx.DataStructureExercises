using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.BinaryTree
{
    [TestFixture]
    class SimpleBinaryTreeTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyTree()
        {
            var tree = CreateTree();

            CollectionAssert.AreEqual(Enumerable.Empty<int>(), tree);
            Assert.AreEqual(0, tree.Count);
        }

        [Test]
        public void CtorWithItems_CreatesFilledTree()
        {
            var expected = new List<int> { 20, 10, 30, 5, 25, 35, 100, 15};
            var tree = CreateTree(expected);

            CollectionAssert.AreEquivalent(expected, tree);
            Assert.AreEqual(expected.Count, tree.Count);
            Assert.AreEqual(expected.Max(), tree.Max());
            Assert.AreEqual(expected.Min(), tree.Min());
        }

        [TestCase(11)]
        [TestCase(-100)]
        [TestCase(11111)]
        [TestCase(38)]
        [TestCase(70)]
        [TestCase(77)]
        [TestCase(223)]
        public void Add_Test(int item)
        {
            var expected = new List<int> {77, 66, 34, 68, 13, 44, 223, 1};
            var tree = CreateTree(expected);
            tree.Add(item);
            expected.Add(item);

            CollectionAssert.AreEquivalent(expected, tree);
            Assert.AreEqual(expected.Count, tree.Count);
            Assert.IsTrue(tree.Contains(item));
            Assert.AreEqual(expected.Max(), tree.Max());
            Assert.AreEqual(expected.Min(), tree.Min());
        }

        [TestCase(44, ExpectedResult = true)]
        [TestCase(11, ExpectedResult = true)]
        [TestCase(35, ExpectedResult = true)]
        [TestCase(77, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(70, ExpectedResult = false)]
        public bool Contains_Test(int searchedItem)
        {
            var tree = CreateTree(new []{ 44, 55, 33, 45, 77, 11, 22, 35 });
            return tree.Contains(searchedItem);
        }

        [TestCase(44)]
        [TestCase(11)]
        [TestCase(35)]
        [TestCase(77)]
        [TestCase(55)]
        [TestCase(1)]
        [TestCase(70)]
        public void Remove_Test(int item)
        {
            var expected = new List<int> { 44, 55, 33, 45, 77, 11, 22, 35 };
            var tree = CreateTree(expected);

            tree.Remove(item);
            expected.Remove(item);

            CollectionAssert.AreEquivalent(expected, tree);
            Assert.AreEqual(expected.Count, tree.Count);
            Assert.AreEqual(expected.Max(), tree.Max());
            Assert.AreEqual(expected.Min(), tree.Min());
        }

        [Test]
        public void Clear_Test()
        {
            var tree = CreateTree(new[] { 20, 10, 30, 5, 25, 35, 100, 15 });
            tree.Clear();

            CollectionAssert.AreEqual(Enumerable.Empty<int>(), tree);
            Assert.AreEqual(0, tree.Count);
            Assert.IsFalse(tree.Contains(25));
        }

        public void InvalidOperation_Tests()
        {
            Assert.Throws<ArgumentNullException>(() => CreateTree(null));
            Assert.Throws<InvalidOperationException>(() => CreateTree().Min());
            Assert.Throws<InvalidOperationException>(() => CreateTree().Max());
        }

        protected virtual ISimpleBinaryTree<int> CreateTree() => new SimpleBinaryTree<int>();
        protected virtual ISimpleBinaryTree<int> CreateTree(IEnumerable<int> items) => new SimpleBinaryTree<int>(items);
    }
}
