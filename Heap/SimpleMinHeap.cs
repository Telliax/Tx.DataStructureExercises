using System;
using System.Collections;
using System.Collections.Generic;
using Tx.DataStructureExersises.List;

namespace Tx.DataStructureExersises.Heap
{
    class SimpleHeap<T> : ISimpleHeap<T>
        where T : IComparable<T>
    {
        public T Min()
        {
            CheckNotEmpty();
            return _items[0];
        }

        public T RemoveTop()
        {
            CheckNotEmpty();
            var top = _items[0];
            _items[0] = _items[Count - 1];
            _items.RemoveAt(Count - 1);
            BubbleDown(0);
            return top;
        }

        public void Insert(T value)
        {
            _items.Add(value);
            BubleUp(Count - 1);
        }

        public int Count => _items.Count;

        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly SimpleList<T> _items = new SimpleList<T>();

        private void CheckNotEmpty()
        {
            if (Count == 0) throw new InvalidOperationException("Heap is empty!");
        }

        private void BubleUp(int index)
        {
            while (true)
            {
                var parentIndex = GetParentIndex(index);
                if (parentIndex < 0) return;

                if (_items[parentIndex].CompareTo(_items[index]) <= 0) return;

                Swap(parentIndex, index);
                index = parentIndex;
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                var children = GetChildrenIndex(index);
                if (children.Left >= Count) return;

                int smallerChildIndex;
                if (children.Right < Count && _items[children.Right].CompareTo(_items[children.Left]) < 0)
                {
                    smallerChildIndex = children.Right;
                }
                else
                {
                    smallerChildIndex = children.Left;
                }

                if (_items[smallerChildIndex].CompareTo(_items[index]) >= 0) return;

                Swap(smallerChildIndex, index);
                index = smallerChildIndex;
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _items[index1];
            _items[index1] = _items[index2]; ;
            _items[index2] = temp;
        }

        private int GetParentIndex(int index)
        {
            if (index == 0) return -1;
            return (index - 1) / 2;
        }

        private (int Left, int Right) GetChildrenIndex(int index)
        {
            var left = 2 * index + 1;
            var right = left + 1;
            return (left, right);
        }
    }
}
