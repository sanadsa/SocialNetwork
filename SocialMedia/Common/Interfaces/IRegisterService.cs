using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IRegisterService
    {
        bool AddUser(string email, string username, string password);
    }
}
