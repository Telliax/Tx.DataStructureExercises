using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tx.DataStructureExersises.Dictionary
{
    class SimpleDictionary<TKey, TValue> : ISimpleDictionary<TKey, TValue>
    {
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _buckets.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public TValue this[TKey key]
        {
            get
            {
                if (!_buckets.TryGetValue(key, out var pair))
                {
                   throw new KeyNotFoundException(key.ToString());
                }
                return pair.Value;
            }
            set => _buckets.TryAdd(new KeyValuePair<TKey, TValue>(key, value), overwriteIfKeyExists:true);
        }

        public void Add(TKey key, TValue value)
        {
            if (!_buckets.TryAdd(new KeyValuePair<TKey, TValue>(key, value), overwriteIfKeyExists:false))
            {
                throw new InvalidOperationException($"Duplicate keys are not allowed. Key: {key}");
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_buckets.TryGetValue(key, out var pair))
            {
                value = pair.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        public bool Contains(TKey key) => _buckets.ContainsKey(key);

        public void Remove(TKey key) => _buckets.RemoveKey(key);

        public void Clear() => _buckets.ClearItems();

        public IEnumerable<TKey> Keys => this.Select(p => p.Key);

        public IEnumerable<TValue> Values => this.Select(p => p.Value);

        public int Count => _buckets.ItemCount;

        private readonly BucketList<KeyValuePair<TKey, TValue>, TKey> _buckets 
            = new BucketList<KeyValuePair<TKey, TValue>, TKey>(p => p.Key);
    }
}
