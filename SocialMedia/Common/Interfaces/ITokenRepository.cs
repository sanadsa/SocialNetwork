using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface ITokenRepository
    {
        Token AddNewToken(AuthenticationUser user);

        Token AddNewFacebookUserToken(FacebookUser user);

        Token ChangeUserToken(User user);
    }
}
