using System;
using System.Collections.Generic;

namespace Bismuth.Framework.Composite
{
    public class NodeList : IList<INode>
    {
        private readonly List<INode> _items = new List<INode>();
        private readonly INode _owner;

        public NodeList(INode owner)
        {
            _owner = owner;
        }

        private void AddParent(INode item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Parent != null) throw new ArgumentException(string.Format("item is already child of node: {0}", item.Parent.Name));
            item.Parent = _owner;
        }

        private void RemoveParent(INode item)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (item.Parent != _owner) throw new ArgumentException("item is not child of this node.");
            item.Parent = null;
        }

        public int IndexOf(INode item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, INode item)
        {
            AddParent(item);
            _items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _items[index].Parent = null;
            _items.RemoveAt(index);
        }

        public INode this[int index]
        {
            get { return _items[index]; }
            set
            {
                AddParent(value);
                _items[index].Parent = null;
                _items[index] = value;
            }
        }

        public void Add(INode item)
        {
            AddParent(item);
            _items.Add(item);
        }

        public void Clear()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].Parent = null;
            }

            _items.Clear();
        }

        public bool Contains(INode item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(INode[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(INode item)
        {
            RemoveParent(item);
            return _items.Remove(item);
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
