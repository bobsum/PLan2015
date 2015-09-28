using System.Collections.Generic;

namespace Bismuth.Framework.Particles
{
    public class Pool<T> where T : new()
    {
        private Stack<T> _stack = new Stack<T>();
        private int _maxSize = 0;

        public Pool() { }

        public Pool(int size) : this(size, 0) { }

        public Pool(int size, int maxSize)
        {
            _maxSize = maxSize;

            for (int i = 0; i < size; i++)
            {
                _stack.Push(new T());
            }
        }

        public T Fetch()
        {
            if (_stack.Count > 0)
            {
                return _stack.Pop();
            }
            return new T();
        }

        public void Insert(T item)
        {
            if (_maxSize == 0 || _stack.Count < _maxSize)
            {
                _stack.Push(item);
            }
        }

        public void Clear()
        {
            _stack.Clear();
        }
    }
}
