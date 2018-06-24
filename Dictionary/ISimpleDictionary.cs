using System.Collections.Generic;

namespace Tx.DataStructureExersises.Dictionary
{
    interface ISimpleDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        TValue this[TKey key] { get; set; }
        void Add(TKey key, TValue value);
        bool TryGetValue(TKey key, out TValue value);
        bool Contains(TKey key);
        void Remove(TKey key);
        void Clear();
        IEnumerable<TKey> Keys { get; }
        IEnumerable<TValue> Values { get; }
        int Count { get; }
    }
}
