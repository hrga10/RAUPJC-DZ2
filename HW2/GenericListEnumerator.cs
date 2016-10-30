using System;
using Zad2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Zad3
{
    class GenericListEnumerator<X> : IEnumerator<X>
    {
        private GenericList<X> genericList;
        private int index = -1;

        public GenericListEnumerator(GenericList<X> genericList)
        {
            this.genericList = genericList;
        }

        public bool MoveNext()
        {
            index++;
            return index < genericList.Count;
        }

        public X Current
        {
            get
            {
                try
                {
                    return genericList.GetElement(index);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }


        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose()
        {
        }
    }
}
