using Identity.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.Interfaces
{
    public interface IIdentityRepository
    {
        void AddUserIdentity(UserIdentity user);
        void ModifyUserIdentity(UserIdentity user);
        IEnumerable<UserIdentity> GetAllUserIdentities();
        UserIdentity GetUserIdentity(string email);
        IEnumerable<UserIdentity> SearchUserIdentities(string email);
        void DeleteUserIdentity(UserIdentity user);
    }
}
