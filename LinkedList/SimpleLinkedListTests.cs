using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.LinkedList
{
    [TestFixture]
    class SimpleLinkedListTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyList()
        {
            var list = CreateList();
            var expected = new LinkedList<int>();
            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void CtorWithItems_CreatesFilledList()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new LinkedList<int>(range);
            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void AddAfter_Test()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new LinkedList<int>(range);

            foreach (var node in list.ToArray())
            {
                list.AddAfter(node, node.Value * 2);
            }

            var current = expected.First;
            for (int i = 0; i < 10; i++)
            {
                var next = current.Next;
                expected.AddAfter(current, current.Value * 2);
                current = next;
            }

            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
            Assert.AreEqual(expected.First.Value, list.Front.Value);
            Assert.AreEqual(expected.Last.Value, list.Back.Value);
        }


        [Test]
        public void AddBefore_Test()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new LinkedList<int>(range);

            foreach (var node in list.ToArray())
            {
                list.AddBefore(node, node.Value * 2);
            }

            var current = expected.First;
            for (int i = 0; i < 10; i++)
            {
                var next = current.Next;
                expected.AddBefore(current, current.Value * 2);
                current = next;
            }

            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
            Assert.AreEqual(expected.First.Value, list.Front.Value);
            Assert.AreEqual(expected.Last.Value, list.Back.Value);
        }

        [Test]
        public void Remove_Test()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new LinkedList<int>(range);

            foreach (var node in list.ToArray().Skip(5))
            {
                list.Remove(node);
            }

            var current = expected.First;
            for (int i = 0; i < 10; i++)
            {
                var next = current.Next;
                if (i >= 5)
                {
                    expected.Remove(current);
                }
                current = next;
            }

            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
            Assert.AreEqual(expected.First.Value, list.Front.Value);
            Assert.AreEqual(expected.Last.Value, list.Back.Value);
        }

        [Test]
        public void AddFront_Test()
        {
            var list = CreateList();
            var expected = new LinkedList<int>();
            foreach (var item in Enumerable.Range(0, 100))
            {
                list.AddFront(item);
                expected.AddFirst(item);
            }
            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
            Assert.AreEqual(expected.First.Value, list.Front.Value);
            Assert.AreEqual(expected.Last.Value, list.Back.Value);
        }

        [Test]
        public void AddBack_Test()
        {
            var list = CreateList();
            var expected = new LinkedList<int>();
            foreach (var item in Enumerable.Range(0, 100))
            {
                list.AddBack(item);
                expected.AddLast(item);
            }
            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
            Assert.AreEqual(expected.First.Value, list.Front.Value);
            Assert.AreEqual(expected.Last.Value, list.Back.Value);
        }

        [Test]
        public void RemoveFront_Test()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new LinkedList<int>(range);

            for (int i = 0; i < 5; i++)
            {
                list.RemoveFront();
                expected.RemoveFirst();
            }

            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
            Assert.AreEqual(expected.First.Value, list.Front.Value);
            Assert.AreEqual(expected.Last.Value, list.Back.Value);
        }

        [Test]
        public void RemoveBack_Test()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new LinkedList<int>(range);

            for (int i = 0; i < 5; i++)
            {
                list.RemoveBack();
                expected.RemoveLast();
            }

            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
            Assert.AreEqual(expected.First.Value, list.Front.Value);
            Assert.AreEqual(expected.Last.Value, list.Back.Value);
        }

        [Test]
        public void Clear_Test()
        {
            var range = Enumerable.Range(0, 10);
            var list = CreateList(range);
            var expected = new LinkedList<int>(range);

            list.Clear();
            expected.Clear();

            CollectionAssert.AreEqual(expected, list.Select(n => n.Value));
            Assert.AreEqual(expected.Count, list.Count);
        }

        [Test]
        public void InvalidOperation_Tests()
        {
            var list = CreateList();
            var list2 = CreateList(new[] {1, 2, 3});

            Assert.Throws<InvalidOperationException>(() => list.Front.ToString());
            Assert.Throws<InvalidOperationException>(() => list.Back.ToString());
            Assert.Throws<InvalidOperationException>(() => list.RemoveFront());
            Assert.Throws<InvalidOperationException>(() => list.RemoveBack());
            Assert.Throws<InvalidOperationException>(() => list.Remove(list2.Front));
            Assert.Throws<InvalidOperationException>(() => list.AddAfter(list2.Front, 1));
            Assert.Throws<InvalidOperationException>(() => list.AddBefore(list2.Front, 1));
            Assert.Throws<InvalidOperationException>(() => list.Remove(new InvalidNode()));
            Assert.Throws<InvalidOperationException>(() => list.AddAfter(new InvalidNode(), 1));
            Assert.Throws<InvalidOperationException>(() => list.AddBefore(new InvalidNode(), 1));
            Assert.Throws<ArgumentNullException>(() => CreateList(null));
        }

        protected virtual ISimpleLinkedList<int> CreateList() => new SimpleLinkedList<int>();
        protected virtual ISimpleLinkedList<int> CreateList(IEnumerable<int> items) => new SimpleLinkedList<int>(items);

        private class InvalidNode : ILinkedNode<int>
        {
            public int Value { get; }
            public ILinkedNode<int> NextNode { get; }
            public ILinkedNode<int> PrevNode { get; }
        }
    }
}
