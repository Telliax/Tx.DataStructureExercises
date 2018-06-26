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

        private static void Swap(this ISimpleList<int> list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        private static void Move(this ISimpleList<int> list, int oldIndex, int newIndex)
        {
            var value = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, value);
        }
    }
}
