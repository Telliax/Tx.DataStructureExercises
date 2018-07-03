using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Tx.DataStructureExersises.List;

namespace Tx.DataStructureExersises.Sort
{
    static class SortingStrategies
    {
        public static void BubbleSort(this ISimpleList<int> list)
        {
            for (int i = list.Count; i > 1; i--)
            {
                for (int j = 1; j < i; j++)
                {
                    if (list[j - 1] > list[j])
                    {
                        list.Swap(j - 1, j);
                    }
                }
            }
        }

        public static void SelectSort(this ISimpleList<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var minIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[minIndex] > list[j])
                    {
                        minIndex = j;
                    }
                }
                list.Swap(minIndex, i);
            }
        }

        private static void Swap(this ISimpleList<int> list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static void InsertSort(this ISimpleList<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (list[j] > list[i])
                    {
                        list.Move(i, j);
                        break;
                    }
                }
            }
        }


        private static void Move(this ISimpleList<int> list, int oldIndex, int newIndex)
        {
            var value = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, value);
        }

        public static void ShellSort(this ISimpleList<int> list)
        {
            Debug.WriteLine("Initial: " + String.Join(" ", list));
            for (int gap = list.Count / 2; gap > 0; gap/=2)
            {
                for (var i = gap; i < list.Count; i++)
                {
                    var current = list[i];
                    int j = i - gap;
                    for (; j >= 0; j -= gap)
                    {
                        if (list[j] < current) break;

                        list[j + gap] = list[j];
                    }
                    list[j + gap] = current;
                    Debug.WriteLine($"Insert  {current} [{i}] -> [{j + gap}]");
                    Debug.WriteLine($"Gap {gap}: " + String.Join(" ", list));
                }
            }
        }

        public static void MergeSort(this ISimpleList<int> list)
        {
            var result = SplitAndMerge(list, 0, list.Count - 1);
            list.Clear();
            foreach (var value in result)
            {
                list.Add(value);
            }
        }

        private static IEnumerable<int> SplitAndMerge(ISimpleList<int> list, int start, int end)
        {
            if (end < start) return Enumerable.Empty<int>();
            if (end == start) return new [] {list[start]};
            var middle = (end + start) / 2;

            var res1 = SplitAndMerge(list, start, middle);
            var res2 = SplitAndMerge(list, middle + 1, end);
            var merged = Merge(res1, res2);
            Debug.WriteLine("Merged: " + String.Join(" ", merged));
            return merged;
        }

        private static IEnumerable<int> Merge(IEnumerable<int> res1, IEnumerable<int> res2)
        {
            using (var first = res1.GetEnumerator())
            using (var second = res2.GetEnumerator())
            {
                var firstEmpty = !first.MoveNext();
                var secondEmpty = !second.MoveNext();
                while (!firstEmpty || !secondEmpty)
                {
                    if (secondEmpty || (!firstEmpty && second.Current > first.Current))
                    {
                        yield return first.Current;
                        firstEmpty = !first.MoveNext();
                    }
                    else
                    {
                        yield return second.Current;
                        secondEmpty = !second.MoveNext();
                    }
                }
            }
        }

        public static void QuickSort(this ISimpleList<int> list)
        {
            QuickSort(list, 0, list.Count - 1);
        }

        private static void QuickSort(ISimpleList<int> list, int start, int end)
        {
            if (start >= end) return;

            var splitIndex = Split(list, start, end);
            QuickSort(list, start, splitIndex - 1);
            QuickSort(list, splitIndex + 1, end);
        }

        private static int Split(ISimpleList<int> list, int start, int end)
        {
            var pivot = list[end];
            var wall = start;
            for (int i = start; i < end; i++)
            {
                if (list[i] > pivot) continue;
                list.Swap(wall, i);
                wall++;
            }
            list.Swap(wall, end);
            return wall;
        }
    }
}
