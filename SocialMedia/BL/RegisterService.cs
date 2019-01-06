using Common.Environment_Services;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class RegisterService : IRegisterService
    {
        private IValidation _validation;
        private IUserRepository _userRepository;

        public RegisterService(IValidation validation, IUserRepository userRepository)
        {
            _validation = validation;
            _userRepository = userRepository;
        }

        public bool AddUser(string email, string username, string password)
        {
            if (_validation.CheckNewUserFields(email, username, password))
            {
                AuthenticationUser newUser = new AuthenticationUser() { Email = email, Username = username, Password = password };
                try
                {
                    _userRepository.AddUserToDatabase(newUser);
                    return true;
                }
                catch (SaveToDatabaseException ex)
                {
                    LogService.WriteExceptionsToLogger(ex);
                    return false;
                }
                catch (System.Exception ex)
                {
                    LogService.WriteExceptionsToLogger(ex);
                    return false;
                }
            }
            else
                return false;
        }
    }
}
