using Tx.DataStructureExersises.Collection;

namespace Tx.DataStructureExersises.Set
{
    interface ISimpleSet<T> : ISimpleCollection<T>
    {
        bool Contains(T item);
        new bool Add(T item);
        new bool Remove(T item);

        ISimpleSet<T> Union(ISimpleSet<T> other);
        ISimpleSet<T> Intersect(ISimpleSet<T> other);
        ISimpleSet<T> Diff(ISimpleSet<T> other);
    }
}
