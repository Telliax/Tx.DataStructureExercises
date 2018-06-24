using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.Queue
{
    [TestFixture]
    class SimpleQueueTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyQueue()
        {
            var queue = CreateQueue();
            var expected = new Queue<int>();
            CollectionAssert.AreEqual(expected, queue);
            Assert.AreEqual(expected.Count, queue.Count);
        }

        [Test]
        public void Enqueue_Test()
        {
            var queue = CreateQueue();
            var expected = new Queue<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                queue.Enqueue(i);
                expected.Enqueue(i);
            }

            CollectionAssert.AreEqual(expected, queue);
            Assert.AreEqual(expected.Count, queue.Count);
        }

        [Test]
        public void Dequeue_Test()
        {
            var queue = CreateQueue();
            var expected = new Queue<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                queue.Enqueue(i);
                expected.Enqueue(i);
            }

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(expected.Dequeue(), queue.Dequeue());
            }

            CollectionAssert.AreEqual(expected, queue);
            Assert.AreEqual(expected.Count, queue.Count);
        }

        [Test]
        public void Peek_Test()
        {
            var queue = CreateQueue();
            var expected = new Queue<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                queue.Enqueue(i);
                expected.Enqueue(i);
                Assert.AreEqual(expected.Peek(), queue.Peek());
            }
        }

        [Test]
        public void Clear_Test()
        {
            var queue = CreateQueue();
            var expected = new Queue<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                queue.Enqueue(i);
                expected.Enqueue(i);
            }
            queue.Clear();
            expected.Clear();
            CollectionAssert.AreEqual(expected, queue);
            Assert.AreEqual(expected.Count, queue.Count);
        }

        [Test]
        public void InvalidOperation_Tests()
        {
            var queue = CreateQueue();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        protected virtual ISimpleQueue<int> CreateQueue() => new SimpleQueue<int>();
    }
}
