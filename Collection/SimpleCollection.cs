using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tx.DataStructureExersises.Collection
{
    class SimpleCollection<T> : ISimpleCollection<T>
    {
        public SimpleCollection()
        {
        }

        public SimpleCollection(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => Items.Take(Count).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count { get; private set; }

        public virtual void Add(T item)
        {
            InsertInternal(Count, item);
        }

        public virtual void Remove(T item)
        {
            var index = Array.FindIndex(Items, 0, Count, i => i.Equals(item));
            if (index < 0) return;
            RemoveAtInternal(index);
        }

        public void Clear()
        {
            Count = 0;
        }

        protected T[] Items = new T[16];

        protected void InsertInternal(int index, T item)
        {
            CheckSize();
            if (index != Count)
            {
                Array.Copy(Items, index, Items, index + 1, Count - index);
            }
            Items[index] = item;
            Count++;
        }

        protected void RemoveAtInternal(int index)
        {
            if (index != Count - 1)
            {
                Array.Copy(Items, index + 1, Items, index, Count - index);
            }
            Count--;
        }

        private void CheckSize()
        {
            if (Count < Items.Length) return;
            var newItems = new T[Items.Length * 2];
            Array.Copy(Items, newItems, Count);
            Items = newItems;
        }
    }
}
