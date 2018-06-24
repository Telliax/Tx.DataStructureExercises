using System;
using Tx.DataStructureExersises.Collection;

namespace Tx.DataStructureExersises.SortedList
{
    interface ISimpleSortedList<T> : ISimpleCollection<T>
        where T : IComparable<T>
    {
        T this[int index] { get; }
        void RemoveAt(int index);
    }
}
