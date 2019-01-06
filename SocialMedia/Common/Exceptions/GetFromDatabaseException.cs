using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class GetFromDatabaseException : System.Exception
    {
        public GetFromDatabaseException() : base() { }

        public GetFromDatabaseException(string msg) : base(msg) { }

        public GetFromDatabaseException(string msg, System.Exception innerEx) : base(msg, innerEx) { }
    }
}
