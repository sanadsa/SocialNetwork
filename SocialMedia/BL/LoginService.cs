using Common.Environment_Services;
using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class LoginService : ILoginService
    {
        private IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(string email, string password)
        {
            try
            {
                return _userRepository.Login(email, password).Result;
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return null;
            }
        }

        public User LoginViaFacebook(string userFacebookId, string email, string username)
        {
            try
            {
                FacebookUser facebookUser = new FacebookUser()
                {
                    Email = email,
                    IsAvilable = false,
                    Username = username,
                    UserFacebookId = userFacebookId
                };
                return _userRepository.LoginViaFacebook(userFacebookId, facebookUser);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return null;
            }

        }

        public bool Logout(string token)
        {
            throw new NotImplementedException();
        }
    }
}
