using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tx.DataStructureExersises.Collection;
using Tx.DataStructureExersises.Dictionary;

namespace Tx.DataStructureExersises.Set
{
    class SimpleHashSet<T> : ISimpleSet<T>
    {
        public SimpleHashSet()
        {
        }

        public SimpleHashSet(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => _buckets.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _buckets.ItemCount;

        public bool Contains(T item) => _buckets.Contains(item);

        public bool Add(T item) => _buckets.TryAdd(item, overwriteIfKeyExists:false);

        public bool Remove(T item) => _buckets.Remove(item);

        public ISimpleSet<T> Union(ISimpleSet<T> other)
        {
            var set = new SimpleHashSet<T>();
            foreach (var item in this.Concat(other))
            {
                set.Add(item);
            }
            return set;
        }

        public ISimpleSet<T> Intersect(ISimpleSet<T> other)
        {
            var set = new SimpleHashSet<T>();
            foreach (var item in this)
            {
                if (other.Contains(item))
                {
                    set.Add(item);
                }
            }
            return set;
        }

        public ISimpleSet<T> Diff(ISimpleSet<T> other)
        {
            var set = new SimpleHashSet<T>();
            foreach (var item in this)
            {
                if (!other.Contains(item))
                {
                    set.Add(item);
                }
            }
            return set;
        }

        void ISimpleCollection<T>.Add(T item) => Add(item);

        void ISimpleCollection<T>.Remove(T item) => Remove(item);

        public void Clear() => _buckets.ClearItems();

        private readonly BucketList<T, T> _buckets = new BucketList<T, T>(t => t);
    }
}
