using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUserRepository
    {
        void AddUserToDatabase(AuthenticationUser user);

        Task<User> Login(string email, string password);

        Task<bool> CheckIfUserExist(AuthenticationUser user);
    }
}
