using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Service_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserServiceTest service = new UserServiceTest();
            service.AddUser();
        }
    }
}
