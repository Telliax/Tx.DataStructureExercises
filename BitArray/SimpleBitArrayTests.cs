using System;
using NUnit.Framework;

namespace Tx.DataStructureExersises.BitArray
{
    [TestFixture]
    class SimpleBitArrayTests
    {
        [Test]
        public void DefaultCtor_CreatesEmptyArray()
        {
            var size = 12;
            var array = CreateArray(size);
            var expected = new System.Collections.BitArray(size);

            CollectionAssert.AreEqual(array, expected);
            Assert.AreEqual(expected.Count, array.Count);
        }

        [TestCase(0, 1, 2)]
        [TestCase(3, 5, 7)]
        [TestCase(2, 4, 6)]
        public void Indexer_Tests(params int[] indexes)
        {
            var size = 12;
            var array = CreateArray(size);
            var expected = new System.Collections.BitArray(size);
            foreach (var i in indexes)
            {
                array[i] = expected[i] = true;
            }
            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(expected[i], array[i]);
            }
            CollectionAssert.AreEqual(array, expected);
            Assert.AreEqual(expected.Count, array.Count);
        }

        [TestCase(new[] { 0, 1, 2 }, new[] { 4, 5, 6 })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 2, 3, 7 })]
        [TestCase(new[] { 4, 2, 7 }, new[] { 2, 4, 7 })]
        public void Or_Test(int[] bits1, int[] bits2)
        {
            var size = 12;
            var array1 = CreateArray(size);
            var array2 = CreateArray(size);
            var expected1 = new System.Collections.BitArray(size);
            var expected2 = new System.Collections.BitArray(size);
            foreach (var bit in bits1)
            {
                array1[bit] = expected1[bit] = true;
            }
            foreach (var bit in bits2)
            {
                array2[bit] = expected2[bit] = true;
            }

            var array = array1.Or(array2);
            var expected = expected1.Or(expected2);
            CollectionAssert.AreEqual(array, expected);
            Assert.AreEqual(expected.Count, array.Count);
        }

        [TestCase(new[] { 0, 1, 2 }, new[] { 4, 5, 6 })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 2, 3, 7 })]
        [TestCase(new[] { 4, 2, 7 }, new[] { 2, 4, 7 })]
        public void And_Test(int[] bits1, int[] bits2)
        {
            var size = 12;
            var array1 = CreateArray(size);
            var array2 = CreateArray(size);
            var expected1 = new System.Collections.BitArray(size);
            var expected2 = new System.Collections.BitArray(size);
            foreach (var bit in bits1)
            {
                array1[bit] = expected1[bit] = true;
            }
            foreach (var bit in bits2)
            {
                array2[bit] = expected2[bit] = true;
            }

            var array = array1.And(array2);
            var expected = expected1.And(expected2);
            CollectionAssert.AreEqual(array, expected);
            Assert.AreEqual(expected.Count, array.Count);
        }

        [TestCase(new[] { 0, 1, 2 }, new[] { 4, 5, 6 })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 2, 3, 7 })]
        [TestCase(new[] { 4, 2, 7 }, new[] { 2, 4, 7 })]
        public void Xor_Test(int[] bits1, int[] bits2)
        {
            var size = 12;
            var array1 = CreateArray(size);
            var array2 = CreateArray(size);
            var expected1 = new System.Collections.BitArray(size);
            var expected2 = new System.Collections.BitArray(size);
            foreach (var bit in bits1)
            {
                array1[bit] = expected1[bit] = true;
            }
            foreach (var bit in bits2)
            {
                array2[bit] = expected2[bit] = true;
            }

            var array = array1.Xor(array2);
            var expected = expected1.Xor(expected2);
            CollectionAssert.AreEqual(array, expected);
            Assert.AreEqual(expected.Count, array.Count);
        }

        [TestCase(new[] { 0, 1, 2 })]
        [TestCase(new[] { 1, 2, 3 })]
        [TestCase(new[] { 4, 2, 7 })]
        public void Or_Test(int[] bits)
        {
            var size = 12;
            var array1 = CreateArray(size);
            var expected1 = new System.Collections.BitArray(size);
            foreach (var bit in bits)
            {
                array1[bit] = expected1[bit] = true;
            }

            var array = array1.Not();
            var expected = expected1.Not();
            CollectionAssert.AreEqual(array, expected);
            Assert.AreEqual(expected.Count, array.Count);
        }

        [TestCase(new[] { 1, 2, 3 }, true)]
        [TestCase(new[] { 4, 2, 7 }, false)]
        public void Or_SetAll(int[] bits, bool value)
        {
            var size = 12;
            var array = CreateArray(size);
            var expected = new System.Collections.BitArray(size);
            foreach (var bit in bits)
            {
                array[bit] = expected[bit] = true;
            }

            array.SetAll(value);
            expected.SetAll(value);

            CollectionAssert.AreEqual(array, expected);
            Assert.AreEqual(expected.Count, array.Count);
        }

        [Test]
        public void InvalidOperation_Tests()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CreateArray(-1));
            Assert.Throws<IndexOutOfRangeException>(() => CreateArray(12)[12].ToString());
            Assert.Throws<IndexOutOfRangeException>(() => CreateArray(12)[-1].ToString());
            Assert.Throws<InvalidOperationException>(() => CreateArray(4).And(CreateArray(5)));
            Assert.Throws<InvalidOperationException>(() => CreateArray(4).Or(CreateArray(5)));
            Assert.Throws<InvalidOperationException>(() => CreateArray(4).Xor(CreateArray(5)));
        }

        protected virtual ISimpleBitArray CreateArray(int size) => new SimpleBitArray(size);
    }
}
