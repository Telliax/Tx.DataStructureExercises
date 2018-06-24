using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tx.DataStructureExersises.Stack
{
    [TestFixture]
    class SimpleStackTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyStack()
        {
            var stack = CreateStack();
            var expected = new Stack<int>();
            CollectionAssert.AreEqual(expected, stack);
            Assert.AreEqual(expected.Count, stack.Count);
        }

        [Test]
        public void Push_Test()
        {
            var stack = CreateStack();
            var expected = new Stack<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                stack.Push(i);
                expected.Push(i);
            }

            CollectionAssert.AreEqual(expected, stack);
            Assert.AreEqual(expected.Count, stack.Count);
        }

        [Test]
        public void Pop_Test()
        {
            var stack = CreateStack();
            var expected = new Stack<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                stack.Push(i);
                expected.Push(i);
            }

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(expected.Pop(), stack.Pop());
            }

            CollectionAssert.AreEqual(expected, stack);
            Assert.AreEqual(expected.Count, stack.Count);
        }

        [Test]
        public void Peek_Test()
        {
            var stack = CreateStack();
            var expected = new Stack<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                stack.Push(i);
                expected.Push(i);
                Assert.AreEqual(expected.Peek(), stack.Peek());
            }
        }

        [Test]
        public void Clear_Test()
        {
            var stack = CreateStack();
            var expected = new Stack<int>();
            foreach (var i in Enumerable.Range(0, 10))
            {
                stack.Push(i);
                expected.Push(i);
            }
            stack.Clear();
            expected.Clear();
            CollectionAssert.AreEqual(expected, stack);
            Assert.AreEqual(expected.Count, stack.Count);
        }

        [Test]
        public void InvalidOperation_Tests()
        {
            var stack = CreateStack();

            Assert.Throws<InvalidOperationException>(() => stack.Pop());
            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        protected virtual ISimpleStack<int> CreateStack() => new SimpleStack<int>();
    }
}
