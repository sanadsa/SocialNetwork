using Common.Environment_Services;
using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthenticationServer.Controllers
{
    public class LoginController : ApiController
    {
        private readonly ILoginService _loginService;
        private readonly IValidation _validation;

        public LoginController(ILoginService loginService, IValidation validation)
        {
            _loginService = loginService;
            _validation = validation;
        }

        [HttpPost]
        [Route("api/Login")]
        public User Login(string username, string password)
        {
            try
            {
                return _loginService.Login(username, password);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
            return null;
        }

        [HttpPost]
        [Route("api/LoginViaFacebook")]
        public AuthenticationUser LoginViaFacebook(string facebookToken)
        {
            try
            {
                return _loginService.LoginViaFacebook(facebookToken);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
            return null;
        }

    }
}
