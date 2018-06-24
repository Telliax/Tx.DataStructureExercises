using System;
using System.Collections;
using System.Collections.Generic;

namespace Tx.DataStructureExersises.LinkedList
{
    class SimpleLinkedList<T> : ISimpleLinkedList<T>
    {
        public SimpleLinkedList()
        {
        }

        public SimpleLinkedList(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                AddBack(item);
            }
        }

        public IEnumerator<ILinkedNode<T>> GetEnumerator()
        {
            ILinkedNode<T> current = _front;
            while (current != null)
            {
                yield return current;
                current = current.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ILinkedNode<T> Front
        {
            get
            {
                CheckNotEmpty();
                return _front;
            }
        }

        public ILinkedNode<T> Back
        {
            get
            {
                CheckNotEmpty();
                return _back;
            }
        }

        public void AddAfter(ILinkedNode<T> node, T item)
        {
            var impl = CheckNode(node);
            AddAfter(impl, item);
        }

        public void AddBefore(ILinkedNode<T> node, T item)
        {
            var impl = CheckNode(node);
            AddBefore(impl, item);
        }

        public void Remove(ILinkedNode<T> node)
        {
            var impl = CheckNode(node);
            impl.RemoveFromList();
        }

        public void AddFront(T item)
        {
            if (Count == 0)
            {
                AddFirst(item);
            }
            else
            {
                AddBefore(_front, item);
            }
        }

        public void AddBack(T item)
        {
            if (Count == 0)
            {
                AddFirst(item);
            }
            else
            {
                AddAfter(_back, item);
            }
        }

        public void RemoveFront()
        {
            CheckNotEmpty();
            _front.RemoveFromList();
        }

        public void RemoveBack()
        {
            CheckNotEmpty();
            _back.RemoveFromList();
        }

        public void Clear()
        {
            Count = 0;
            _front = _back = null;
        }

        public int Count { get; private set; }

        private LinkedNode _front;
        private LinkedNode _back;

        private LinkedNode CheckNode(ILinkedNode<T> node)
        {
            if (node is LinkedNode impl && ReferenceEquals(this, impl.List))
            {
                return impl;
            }

            throw new InvalidOperationException("Node does not belong to this list.");
        }

        private void AddFirst(T item)
        {
            var newNode = new LinkedNode(item, this);
            _front = _back = newNode;
            Count++;
        }

        private void AddAfter(LinkedNode node, T item)
        {
            node.InsertNext(item);
        }

        private void AddBefore(LinkedNode node, T item)
        {
            node.InsertPrev(item);
        }

        private void CheckNotEmpty()
        {
            if (Count == 0) throw new InvalidOperationException("List is empty!");
        }

        private class LinkedNode : ILinkedNode<T>
        {
            public LinkedNode(T value, SimpleLinkedList<T> list)
            {
                Value = value;
                List = list;
            }

            public T Value { get; }
            public ILinkedNode<T> NextNode => _next;
            public ILinkedNode<T> PrevNode => _prev;
            public SimpleLinkedList<T> List { get; private set; }

            public void InsertNext(T value)
            {
                var newNode = new LinkedNode(value, List)
                {
                    _prev = this,
                    _next = _next
                };

                var oldNext = _next;
                _next = newNode;

                if (oldNext != null)
                {
                    oldNext._prev = newNode;
                }
                else
                {
                    List._back = newNode;
                }

                List.Count++;
            }

            public void InsertPrev(T value)
            {
                var newNode = new LinkedNode(value, List)
                {
                    _next = this,
                    _prev = _prev
                };

                var oldPrev = _prev;
                _prev = newNode;

                if (oldPrev != null)
                {
                    oldPrev._next = newNode;
                }
                else
                {
                    List._front = newNode;
                }

                List.Count++;
            }

            public void RemoveFromList()
            {
                if (_prev != null)
                {
                    _prev._next = _next;
                }
                else
                {
                    List._front = _next;
                }
                if (_next != null)
                {
                    _next._prev = _prev;
                }
                else
                {
                    List._back = _prev;
                }
                List.Count--;
                _prev = null;
                _next = null;
                List = null;
            }

            private LinkedNode _next;
            private LinkedNode _prev;
        }
    }
}
