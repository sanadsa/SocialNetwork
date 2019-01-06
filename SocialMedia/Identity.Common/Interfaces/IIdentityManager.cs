using Identity.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.Interfaces
{
    public interface IIdentityManager
    {
        void AddUser(UserIdentity user);
        void UpdateUser(UserIdentity identity);
        void DeleteUser(UserIdentity identity);
        UserIdentity GetUser(string email);
    }
}
