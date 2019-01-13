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
        Task<bool> AddUserToDatabase(AuthenticationUser user);

        Task<User> Login(string email, string password);

        Task<bool> CheckIfUserExist(AuthenticationUser user);

        Task<User> LoginViaFacebook(string facebookToken, FacebookUser facebookUser);
    }
}
