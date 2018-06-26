using System;
using System.Collections;
using System.Collections.Generic;

namespace Tx.DataStructureExersises.BitArray
{
    class SimpleBitArray : ISimpleBitArray
    {
        public SimpleBitArray(int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Size cannot be less than zero!");
            Count = size;
            _bytes = new byte[(-1L + size + BitsPerByte) / BitsPerByte];
        }

        public IEnumerator<bool> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool this[int index]
        {
            get
            {
                var pos = GetBitPosition(index);
                var mask = (1 << pos.Offset);
                return (_bytes[pos.Index] & mask) != 0;
            }
            set
            {
                var pos = GetBitPosition(index);
                var mask = (1 << pos.Offset);
                if (value)
                {
                    _bytes[pos.Index] |= (byte)mask;
                }
                else
                {
                    _bytes[pos.Index] &= (byte)~mask;
                }
            }
        }

        public void SetAll(bool value)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] = value;
            }
        }

        public ISimpleBitArray Xor(ISimpleBitArray other)
        {
            CheckSameSize(this, other);
            var res = new SimpleBitArray(Count);
            for (int i = 0; i < Count; i++)
            {
                res[i] = this[i] ^ other[i];
            }
            return res;
        }

        public ISimpleBitArray And(ISimpleBitArray other)
        {
            CheckSameSize(this, other);
            var res = new SimpleBitArray(Count);
            for (int i = 0; i < Count; i++)
            {
                res[i] = this[i] & other[i];
            }
            return res;
        }

        public ISimpleBitArray Or(ISimpleBitArray other)
        {
            CheckSameSize(this, other);
            var res = new SimpleBitArray(Count);
            for (int i = 0; i < Count; i++)
            {
                res[i] = this[i] | other[i];
            }
            return res;
        }

        public ISimpleBitArray Not()
        {
            var res = new SimpleBitArray(Count);
            for (int i = 0; i < Count; i++)
            {
                res[i] = !this[i];
            }
            return res;
        }

        public int Count { get; }

        private readonly byte[] _bytes;
        private const int BitsPerByte = 8;

        private (int Index, int Offset) GetBitPosition(int index)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException($"Count: {Count}, Index: {index}");
            return (index / BitsPerByte, index % BitsPerByte);
        }

        private static void CheckSameSize(ISimpleBitArray first, ISimpleBitArray second)
        {
            if (first.Count != second.Count)
                throw new InvalidOperationException($"Arrays of different size are not supported! Fisrt array size: {first.Count}. Second array size: {second.Count}");
        }
    }
}
