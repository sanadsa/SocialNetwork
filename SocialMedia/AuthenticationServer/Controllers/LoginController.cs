using AuthenticationServer.Models;
using Common.Environment_Services;
using Common.Interfaces;
using Common.Models;
using Newtonsoft.Json.Linq;
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

        [HttpGet]
        [Route("api/Login")]
        public User Login([FromBody]object user)
        {
            try
            {
                //var othe = (UserLogin)user;
                //return _loginService.Login(user.Email, user.Password);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
            return null;
        }

        [HttpPost]
        [Route("api/LoginViaFacebook")]
        public User LoginViaFacebook(string facebookToken, string email, string username)
        {
            try
            {
                return _loginService.LoginViaFacebook(facebookToken, email, username);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
            return null;
        }
    }
}
