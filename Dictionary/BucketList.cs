using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tx.DataStructureExersises.List;

namespace Tx.DataStructureExersises.Dictionary
{
    class BucketList<TItem, TKey> : IEnumerable<TItem>
    {
        public BucketList(Func<TItem, TKey> keySelector, int size = 16)
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
            _buckets = new Bucket[size];
        }

        public bool TryAdd(TItem item, bool overwriteIfKeyExists)
        {
            CheckSize();
            var key = _keySelector(item);
            var bucket = FindBucket(key);
            if (bucket.TryAdd(key, item, overwriteIfKeyExists))
            {
                ItemCount++;
                return true;
            }
            return false;
        }

        public bool Contains(TItem item)
        {
            var key = _keySelector(item);
            return ContainsKey(key);
        }

        public bool ContainsKey(TKey key)
        {
            var bucket = FindBucket(key);
            return bucket.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TItem item)
        {
            var bucket = FindBucket(key);
            return bucket.TryGetItem(key, out item);
        }

        public bool Remove(TItem item)
        {
            var key = _keySelector(item);
            return RemoveKey(key);
        }

        public bool RemoveKey(TKey key)
        {
            var bucket = FindBucket(key);
            if (bucket.RemoveKey(key))
            {
                ItemCount--;
                return true;
            }
            return false;
        }

        public void ClearItems()
        {
            foreach (var bucket in _buckets)
            {
                bucket?.Clear();
            }
            ItemCount = 0;
        }

        public int ItemCount { get; private set; }
        public int BucketCount => _buckets.Length;

        public IEnumerator<TItem> GetEnumerator() => _buckets.Where(b => b != null).SelectMany(b => b).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private Bucket[] _buckets;
        private readonly Func<TItem, TKey> _keySelector;

        private Bucket FindBucket(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            var index = Math.Abs(key.GetHashCode()) % _buckets.Length;
            var bucket = _buckets[index];
            if (bucket == null)
            {
                _buckets[index] = bucket = new Bucket(_keySelector);
            }
            return bucket;
        }

        private void CheckSize()
        {
            if (ItemCount * 2 <= BucketCount) return;
            var newBuckets = new BucketList<TItem, TKey>(_keySelector, BucketCount * 2);
            foreach (var item in this)
            {
                if (!newBuckets.TryAdd(item, overwriteIfKeyExists: false))
                {
                    throw new InvalidOperationException("Duplicate key was encountered during bucket array resize. This should not happen");
                }
            }
            _buckets = newBuckets._buckets;
        }

        private class Bucket : IEnumerable<TItem>
        {
            public Bucket(Func<TItem, TKey> keySelector)
            {
                _keySelector = keySelector;
            }

            public bool TryAdd(TKey key, TItem item, bool overwriteIfKeyExists)
            {
                var index = FindKey(key);
                if (index < 0)
                {
                    _items.Add(item);
                    return true;
                }
                if (overwriteIfKeyExists)
                {
                    _items[index] = item;
                }
                return false;
            }

            public bool RemoveKey(TKey key)
            {
                var index = FindKey(key);
                if (index < 0)
                {
                    return false;
                }
                _items.RemoveAt(index);
                return true;
            }

            public void Clear() => _items.Clear();

            public bool TryGetItem(TKey key, out TItem value)
            {
                var index = FindKey(key);
                if (index < 0)
                {
                    value = default(TItem);
                    return false;
                }
                value = _items[index];
                return true;
            }

            public bool ContainsKey(TKey key) => FindKey(key) >= 0;

            public IEnumerator<TItem> GetEnumerator() => _items.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private readonly SimpleList<TItem> _items = new SimpleList<TItem>();
            private readonly Func<TItem, TKey> _keySelector;

            private int FindKey(TKey key)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    var currentKey = _keySelector(_items[i]);
                    if (key.Equals(currentKey))
                    {
                        return i;
                    }
                }
                return -1;
            }
        }
    }
}
