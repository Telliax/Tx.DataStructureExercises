using System.Collections.Generic;

namespace Tx.DataStructureExersises.BitArray
{
    interface ISimpleBitArray : IEnumerable<bool>
    {
        bool this[int index] { get; set; }
        void SetAll(bool value);

        ISimpleBitArray Xor(ISimpleBitArray other);
        ISimpleBitArray And(ISimpleBitArray other);
        ISimpleBitArray Or(ISimpleBitArray other);
        ISimpleBitArray Not();

        int Count { get; }
    }
}
