using System.Collections.Generic;

namespace Tx.DataStructureExersises.Queue
{
    interface ISimpleQueue<T> : IEnumerable<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        void Clear();
        int Count { get; }

    }
}
