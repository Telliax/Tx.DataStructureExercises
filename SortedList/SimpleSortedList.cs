using System;
using System.Collections.Generic;
using Tx.DataStructureExersises.Collection;

namespace Tx.DataStructureExersises.SortedList
{
    class SimpleSortedList<T> : SimpleCollection<T>, ISimpleSortedList<T>
        where T : IComparable<T>
    {
        public SimpleSortedList()
        {
        }

        public SimpleSortedList(IEnumerable<T> items) : base(items)
        {
        }

        public override void Add(T item)
        {
            var index = BinarySearch(item);
            if (index < 0)
            {
                index = ~index;
            }
            InsertInternal(index, item);
        }

        public override void Remove(T item)
        {
            var index = BinarySearch(item);
            if (index < 0) return;
            RemoveAtInternal(index);
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return Items[index];
            }
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            RemoveAtInternal(index);
        }

        public int BinarySearch(T searchedValue)
        {
            var left = 0;
            var right = Count - 1;
            while (left <= right)
            {
                var middle = (left + right) / 2;
                var compare = this[middle].CompareTo(searchedValue);
                if (compare == 0)
                {
                    return middle;
                }
                if (compare < 0)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }
            return ~left;
        }

        private void CheckIndex(int index)
        {
            if (index >= Count || index < 0) throw new IndexOutOfRangeException($"Index: {index}, Count: {Count}");
        }
    }
}
