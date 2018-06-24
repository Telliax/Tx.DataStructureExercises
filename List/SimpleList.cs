using System;
using System.Collections.Generic;
using Tx.DataStructureExersises.Collection;

namespace Tx.DataStructureExersises.List
{
    class SimpleList<T> : SimpleCollection<T>, ISimpleList<T>
    {
        public SimpleList()
        {
        }

        public SimpleList(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return Items[index];
            } 
            set
            {
                CheckIndex(index);
                Items[index] = value;
            }
        }

        public void Insert(int index, T item)
        {
            CheckIndex(index, strictRange: false);
            InsertInternal(index, item);
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            RemoveAtInternal(index);
        }

        private void CheckIndex(int index, bool strictRange = true)
        {
            var validRange = (strictRange ? index < Count : index <= Count) && index >= 0;
            if (!validRange) throw new IndexOutOfRangeException($"Index: {index}, Count: {Count}");
        }
    }
}
