using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Exceptions
{
    public class Neo4jException : Exception
    {
        public Neo4jException() : base() { }
        public Neo4jException(string msg) : base(msg) { }
        public Neo4jException(string msg, Exception innerEx) : base(msg, innerEx) { }
    }
}
