using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() { }

        public UserNotFoundException(string msg) : base(msg) { }

        public UserNotFoundException(string msg, System.Exception innerEx) : base(msg, innerEx) { }
    }
}
