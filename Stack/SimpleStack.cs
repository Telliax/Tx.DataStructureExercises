using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tx.DataStructureExersises.List;

namespace Tx.DataStructureExersises.Stack
{
    class SimpleStack<T> : ISimpleStack<T>
    {
        public IEnumerator<T> GetEnumerator() => _items.Reverse().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T Peek()
        {
            CheckNotEmpty();
            return _items[Count - 1];
        }

        public T Pop()
        {
            var res = Peek();
            _items.RemoveAt(Count - 1);
            return res;
        }

        public void Push(T item) => _items.Add(item);

        public void Clear() => _items.Clear();

        public int Count => _items.Count;

        private readonly SimpleList<T> _items = new SimpleList<T>();

        private void CheckNotEmpty()
        {
            if (Count == 0) throw new InvalidOperationException("Stack is empty!");
        }
    }
}
