using Tx.DataStructureExersises.Collection;

namespace Tx.DataStructureExersises.List
{
    interface ISimpleList<T> : ISimpleCollection<T>
    {
        T this[int index] { get; set; }
        void Insert(int index, T item);
        void RemoveAt(int index);
    }
}
