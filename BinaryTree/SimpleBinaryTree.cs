using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tx.DataStructureExersises.Queue;

namespace Tx.DataStructureExersises.BinaryTree
{
    class SimpleBinaryTree<T> : ISimpleBinaryTree<T>
        where T : IComparable<T>
    {
        public SimpleBinaryTree()
        {
        }

        public SimpleBinaryTree(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => TraverseBreadthFirst().Select(n => n.Value).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count { get; private set; }

        public void Add(T item)
        {
            var node = new TreeNode(item);

            if (Count == 0)
            {
                _root = node;
            }
            else
            {
                var current = _root;
                while (true)
                {
                    if (current > node)
                    {
                        if (current.Left == null)
                        {
                            current.Left = node;
                            break;
                        }
                        current = current.Left;
                    }
                    else
                    {
                        if (current.Right == null)
                        {
                            current.Right = node;
                            break;
                        }
                        current = current.Right;
                    }
                }
            }
            Count++;
        }

        public void Remove(T item)
        {
            var node = FindNode(item);
            if (node == null) return;
            if (Count == 1)
            {
                Clear();
            }
            else
            {
                Remove(node);
                Count--;
            }     
        }

        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        public IBinaryTreeNode<T> Root => _root;

        public bool Contains(T value)
        {
            return FindNode(value) != null;
        }

        public T Min()
        {
            CheckNotEmpty();
            return FindLeftmostNode(_root).Value;
        }

        public T Max()
        {
            CheckNotEmpty();
            return FindRightmostNode(_root).Value;
        }

        public override string ToString()
        {
            return String.Join(" || ", TraverseBreadthFirst()
                                        .GroupBy(n => n.Depth)
                                        .OrderBy(g => g.Key)
                                        .Select(g => String.Join(" ", g)));
        }

        private TreeNode _root;

        private IEnumerable<TreeNode> TraverseBreadthFirst()
        {
            if (Count == 0) yield break;

            var queue = new SimpleQueue<TreeNode>();
            queue.Enqueue(_root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;
                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }
        }

        private TreeNode FindNode(T value)
        {
            var searchedNode = new TreeNode(value);
            var current = _root;
            while (true)
            {
                if (current == null) return null;
                if (current == searchedNode) return current;
                current = current > searchedNode ? current.Left : current.Right;
            }
        }

        private TreeNode FindLeftmostNode(TreeNode root)
        {
            while (true)
            {
                if (root.Left == null)
                {
                    return root;
                }
                root = root.Left;
            }
        }

        private TreeNode FindRightmostNode(TreeNode root)
        {
            while (true)
            {
                if (root.Right == null)
                {
                    return root;
                }
                root = root.Right;
            }
        }

        private void Remove(TreeNode node)
        {
            TreeNode replacement;
            if (node.Left != null && node.Right != null)
            {
                replacement = FindLeftmostNode(node.Right);
                Remove(replacement);
                replacement.Left = node.Left;
                replacement.Right = node.Right;
            }
            else
            {
                replacement = node.Left ?? node.Right;
            }

            if (node == _root)
            {
                _root = replacement;
                replacement.Parent = null;
            }
            else  if (node.Parent > node)
            {
                node.Parent.Left = replacement;
            }
            else
            {
                node.Parent.Right = replacement;
            }
        }

        private void CheckNotEmpty()
        {
            if (Count == 0) throw new InvalidOperationException("Tree is empty!");
        }

        private class TreeNode : IBinaryTreeNode<T>, IComparable<TreeNode>, IEquatable<TreeNode>
        {
            private TreeNode _right;
            private TreeNode _left;

            public TreeNode(T value)
            {
                Value = value;
            }

            public T Value { get; }

            IBinaryTreeNode<T> IBinaryTreeNode<T>.Left => Left;
            IBinaryTreeNode<T> IBinaryTreeNode<T>.Right => Right;

            public TreeNode Left
            {
                get => _left;
                set
                {
                    _left = value;
                    if (value != null)
                    {
                        value.Parent = this;
                    }
                }
            }

            public TreeNode Right
            {
                get => _right;
                set
                {
                    _right = value;
                    if (value != null)
                    {
                        value.Parent = this;
                    }
                }
            }

            public TreeNode Parent { get; set; }

            public int Depth
            {
                get
                {
                    var depth = 0;
                    var current = this;
                    while (current.Parent != null)
                    {
                        depth++;
                        current = current.Parent;
                    }
                    return depth;
                }
            }

            public override string ToString()
            {
                return Value.ToString();
            }

            #region equality memebers (autogenerated)

            public int CompareTo(TreeNode other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return Value.CompareTo(other.Value);
            }

            public bool Equals(TreeNode other)
            {
                if (ReferenceEquals(this, other)) return true;
                if (ReferenceEquals(null, other)) return false;
                return EqualityComparer<T>.Default.Equals(Value, other.Value);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as TreeNode);
            }

            public override int GetHashCode()
            {
                return EqualityComparer<T>.Default.GetHashCode(Value);
            }

            public static bool operator ==(TreeNode left, TreeNode right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(TreeNode left, TreeNode right)
            {
                return !Equals(left, right);
            }

            public static bool operator <(TreeNode left, TreeNode right)
            {
                return Comparer<TreeNode>.Default.Compare(left, right) < 0;
            }

            public static bool operator >(TreeNode left, TreeNode right)
            {
                return Comparer<TreeNode>.Default.Compare(left, right) > 0;
            }

            public static bool operator <=(TreeNode left, TreeNode right)
            {
                return Comparer<TreeNode>.Default.Compare(left, right) <= 0;
            }

            public static bool operator >=(TreeNode left, TreeNode right)
            {
                return Comparer<TreeNode>.Default.Compare(left, right) >= 0;
            }

            #endregion
        }
    }
}
