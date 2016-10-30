using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExceptions
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(Guid id) : base(MessageId(id))
        {
        }

        private static String MessageId(Guid id)
        {
            return String.Format("Duplicate id: {0}", id.ToString());
        }
    }
}
