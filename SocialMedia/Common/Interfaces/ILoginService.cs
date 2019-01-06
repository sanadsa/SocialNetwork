using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ILoginService
    {
        User Login(string username, string password);

        User LoginViaFacebook(string token, string email, string username);

        bool Logout(string token);
    }
}
