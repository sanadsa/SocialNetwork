using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class SaveToDatabaseException : Exception
    {
        public SaveToDatabaseException() : base() { }

        public SaveToDatabaseException(string msg) : base(msg) { }

        public SaveToDatabaseException(string msg, Exception innerEx) : base(msg, innerEx) { }
    }
}
