using System;
using System.Collections.Generic;

namespace Tx.DataStructureExersises.Heap
{
    interface ISimpleHeap<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        T Min();
        T RemoveTop();
        void Insert(T value);
        int Count { get; }
    }
}
