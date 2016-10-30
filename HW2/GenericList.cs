using System;
using Zad3;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad2
{
    public class GenericList<X> : IGenericList<X>
    {
        X[] _internalStorage;
        private int count;
        private int capacity;

        public GenericList()
        {
            capacity = 4;
            count = 0;
            _internalStorage = new X[4];
        }

        public GenericList(int initialSize)
        {
            if (initialSize < 1)
            {
                throw new ArgumentException("Argument has to be positive!");
            }
            capacity = initialSize;
            count = 0;
            _internalStorage = new X[initialSize];
        }
        
        public void Add(X item)
        {
            if (count == capacity)
            {
                capacity = capacity * 2;
                X[] pom = new X[capacity];
                for (int i = 0; i < count; ++i)
                {
                    pom[i] = _internalStorage[i];
                }
                _internalStorage = pom;
            }

            _internalStorage[count] = item;
            count++;
        }

        public bool Remove(X item)
        {
            for (int i = 0; i < count; ++i)
            {
                if (_internalStorage[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                return false;
            }

            for (int i = index; i < count - 1; ++i)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }
            count--;
            return true;
        }

        public X GetElement(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            return _internalStorage[index];
        }

        public int IndexOf(X item)
        {
            for(int i = 0; i < count; ++i)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public int Count
        {
            get
            {
                return count;
            }
        }
        
        public void Clear()
        {
            _internalStorage = new X[capacity];
            count = 0;
        }
        
        public bool Contains(X item)
        {
            if (IndexOf(item) != -1)
            {
                return true;
            }
            return false;
        }

        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
       
    }
}
