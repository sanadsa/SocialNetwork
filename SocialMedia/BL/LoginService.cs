﻿using Common.Environment_Services;
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

        public User LoginViaFacebook(string facebookToken, string email, string username)
        {
            try
            {
                return _userRepository.LoginViaFacebook(facebookToken, email, username);
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
