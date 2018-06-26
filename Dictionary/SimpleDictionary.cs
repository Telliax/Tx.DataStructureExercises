using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tx.DataStructureExersises.List;

namespace Tx.DataStructureExersises.Dictionary
{
    class SimpleDictionary<TKey, TValue> : ISimpleDictionary<TKey, TValue>
    {
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() 
            => _buckets.Where(b => b != null).SelectMany(b => b).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out var value))
                {
                   throw new KeyNotFoundException(key.ToString());
                }
                return value;
            }
            set => Add(key, value, throwIfExists: false);
        }

        public void Add(TKey key, TValue value) => Add(key, value, throwIfExists:true);

        public bool TryGetValue(TKey key, out TValue value)
        {
            var bucket = FindBucket(key);
            return bucket.TryGetValue(key, out value);
        } 

        public bool Contains(TKey key) => TryGetValue(key, out var value);

        public void Remove(TKey key)
        {
            var bucket = FindBucket(key);
            if (bucket.Remove(key))
            {
                Count--;
            }
        }

        public void Clear()
        {
            foreach (var bucket in _buckets)
            {
                bucket?.Clear();
            }
            Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(p => p.Key);

        public IEnumerable<TValue> Values => this.Select(p => p.Value);

        public int Count { get; private set; }

        private Bucket[] _buckets = new Bucket[16];

        private Bucket FindBucket(TKey key) => FindBucket(_buckets, key);

        private static Bucket FindBucket(Bucket[] buckets, TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            var index = Math.Abs(key.GetHashCode()) % buckets.Length;
            var bucket = buckets[index];
            if (bucket == null)
            {
                buckets[index] = bucket = new Bucket();
            }
            return bucket;
        }

        private void Add(TKey key, TValue value, bool throwIfExists)
        {
            CheckSize();
            var newPair = new KeyValuePair<TKey, TValue>(key, value);
            var bucket = FindBucket(key);
            if (bucket.Add(newPair, throwIfExists))
            {
                Count++;
            }
        }

        private void CheckSize()
        {
            if (Count * 2 <= _buckets.Length) return;
            var newBuckets = new Bucket[_buckets.Length * 2];
            foreach (var pair in this)
            {
                var bucket = FindBucket(newBuckets, pair.Key);
                bucket.Add(pair, throwIfExists:true);
            }
            _buckets = newBuckets;
        }

        private class Bucket : IEnumerable<KeyValuePair<TKey, TValue>>
        {
            public bool Add(KeyValuePair<TKey, TValue> pair, bool throwIfExists)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    var current = _items[i];
                    if (current.Key.Equals(pair.Key))
                    {
                        if (throwIfExists)
                        {
                            throw new InvalidOperationException($"Key '{pair.Key}' already exists!");
                        }
                        _items[i] = pair;
                        return false;
                    }
                }
                _items.Add(pair);
                return true;
            }

            public bool Remove(TKey key)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    var pair = _items[i];
                    if (pair.Key.Equals(key))
                    {
                        _items.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }

            public void Clear() => _items.Clear();

            public bool TryGetValue(TKey key, out TValue value)
            {
                foreach (var pair in _items)
                {
                    if (!pair.Key.Equals(key)) continue;
                    value = pair.Value;
                    return true;
                }
                value = default(TValue);
                return false;
            }

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _items.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private readonly SimpleList<KeyValuePair<TKey, TValue>> _items 
                = new SimpleList<KeyValuePair<TKey, TValue>>();
        }
    }
}
