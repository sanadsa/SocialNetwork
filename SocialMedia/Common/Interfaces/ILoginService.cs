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

        AuthenticationUser LoginViaFacebook(string token);

        bool Logout(string token);
    }
}
