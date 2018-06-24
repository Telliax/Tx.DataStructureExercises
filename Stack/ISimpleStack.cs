using System.Collections.Generic;

namespace Tx.DataStructureExersises.Stack
{
    interface ISimpleStack<T> : IEnumerable<T>
    {
        T Peek();
        T Pop();
        void Push(T item);
        void Clear();
        int Count { get; }
    }
}
