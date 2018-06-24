using System.Collections.Generic;

namespace Tx.DataStructureExersises.Collection
{
    interface ISimpleCollection<T> : IEnumerable<T>
    {
        int Count { get; }
        void Add(T item);
        void Remove(T item);
        void Clear();
    }
}
