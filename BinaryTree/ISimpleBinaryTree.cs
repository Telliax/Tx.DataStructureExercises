using System;
using System.Collections.Generic;
using Tx.DataStructureExersises.Collection;

namespace Tx.DataStructureExersises.BinaryTree
{
    interface ISimpleBinaryTree<T> : ISimpleCollection<T>
        where T : IComparable<T>
    {
        IBinaryTreeNode<T> Root { get; }

        bool Contains(T value);
        T Min();
        T Max();
    }

    interface IBinaryTreeNode<out T>
    {
        T Value { get; }
        IBinaryTreeNode<T> Left { get; }
        IBinaryTreeNode<T> Right { get; }
    }
}
