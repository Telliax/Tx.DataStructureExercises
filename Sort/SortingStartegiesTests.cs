using System;
using System.Collections.Generic;
using NUnit.Framework;
using Tx.DataStructureExersises.List;

namespace Tx.DataStructureExersises.Sort
{
    [TestFixture]
    class SortingStartegiesTests
    {
        [TestCase]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 2, 1, 2, 2, 1)]
        [TestCase(10, 9, 8, 7, 6, 5, 4, 3, 2, 1)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [TestCase(1, 2, 5, 6, 3, 4, 9, 10, 7, 8)]
        [TestCase(10, 7, 6, 9, 8, 5, 2, 4, 3, 1)]
        [TestCase(5, 3, 9, 4, 10, 2, 7, 1, 8, 6)]
        public void BubbleSort_Test(params int[] testCase) => TestSort(testCase, l => l.BubbleSort());

        [TestCase]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 2, 1, 2, 2, 1)]
        [TestCase(10, 9, 8, 7, 6, 5, 4, 3, 2, 1)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [TestCase(1, 2, 5, 6, 3, 4, 9, 10, 7, 8)]
        [TestCase(10, 7, 6, 9, 8, 5, 2, 4, 3, 1)]
        [TestCase(5, 3, 9, 4, 10, 2, 7, 1, 8, 6)]
        public void SelectSort_Test(params int[] testCase) => TestSort(testCase, l => l.SelectSort());

        [TestCase]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 2, 1, 2, 2, 1)]
        [TestCase(10, 9, 8, 7, 6, 5, 4, 3, 2, 1)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [TestCase(1, 2, 5, 6, 3, 4, 9, 10, 7, 8)]
        [TestCase(10, 7, 6, 9, 8, 5, 2, 4, 3, 1)]
        [TestCase(5, 3, 9, 4, 10, 2, 7, 1, 8, 6)]
        public void InsertSort_Test(params int[] testCase) => TestSort(testCase, l => l.InsertSort());

        [TestCase]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 2, 1, 2, 2, 1)]
        [TestCase(10, 9, 8, 7, 6, 5, 4, 3, 2, 1)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [TestCase(1, 2, 5, 6, 3, 4, 9, 10, 7, 8)]
        [TestCase(10, 7, 6, 9, 8, 5, 2, 4, 3, 1)]
        [TestCase(5, 3, 9, 4, 10, 2, 7, 1, 8, 6)]
        public void ShellSort_Test(params int[] testCase) => TestSort(testCase, l => l.ShellSort());

        [TestCase]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 2, 1, 2, 2, 1)]
        [TestCase(10, 9, 8, 7, 6, 5, 4, 3, 2, 1)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [TestCase(1, 2, 5, 6, 3, 4, 9, 10, 7, 8)]
        [TestCase(10, 7, 6, 9, 8, 5, 2, 4, 3, 1)]
        [TestCase(5, 3, 9, 4, 10, 2, 7, 1, 8, 6)]
        public void MergeSort_Test(params int[] testCase) => TestSort(testCase, l => l.MergeSort());

        [TestCase]
        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 2, 1, 2, 2, 1)]
        [TestCase(10, 9, 8, 7, 6, 5, 4, 3, 2, 1)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [TestCase(1, 2, 5, 6, 3, 4, 9, 10, 7, 8)]
        [TestCase(10, 7, 6, 9, 8, 5, 2, 4, 3, 1)]
        [TestCase(5, 3, 9, 4, 10, 2, 7, 1, 8, 6)]
        public void QuickSort_Test(params int[] testCase) => TestSort(testCase, l => l.QuickSort());

        private void TestSort(int[] input, Action<ISimpleList<int>> sort)
        {
            var actual = new SimpleList<int>(input);
            var expected = new List<int>(input);
            expected.Sort();
            sort(actual);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
