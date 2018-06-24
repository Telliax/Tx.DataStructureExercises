using System.Collections.Generic;

namespace Tx.DataStructureExersises.LinkedList
{
    interface ISimpleLinkedList<T> : IEnumerable<ILinkedNode<T>>
    {
        ILinkedNode<T> Front { get; }
        ILinkedNode<T> Back { get; }
        void AddAfter(ILinkedNode<T> node, T item);
        void AddBefore(ILinkedNode<T> node, T item);
        void Remove(ILinkedNode<T> node);
        void AddFront(T item);
        void AddBack(T item);
        void RemoveFront();
        void RemoveBack();
        void Clear();
        int Count { get; }
    }

    interface ILinkedNode<out T>
    {
        T Value { get; }
        ILinkedNode<T> NextNode { get; }
        ILinkedNode<T> PrevNode { get;  }
    }
}
