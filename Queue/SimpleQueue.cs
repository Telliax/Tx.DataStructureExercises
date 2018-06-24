using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tx.DataStructureExersises.LinkedList;

namespace Tx.DataStructureExersises.Queue
{
    class SimpleQueue<T> : ISimpleQueue<T>
    {
        public IEnumerator<T> GetEnumerator() => _items.Select(i => i.Value).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Enqueue(T item) => _items.AddBack(item);

        public T Dequeue()
        {
            var res = Peek();
            _items.RemoveFront();
            return res;
        }

        public T Peek() => _items.Front.Value;

        public void Clear() => _items.Clear();

        public int Count => _items.Count;

        private readonly SimpleLinkedList<T> _items = new SimpleLinkedList<T>();
    }
}
