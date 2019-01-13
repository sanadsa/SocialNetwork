using AuthenticationServer.Models;
using Common.Environment_Services;
using Common.Interfaces;
using Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthenticationServer.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        private readonly ILoginService _loginService;
        private readonly IValidation _validation;
        private JObject jObject;


        public LoginController(ILoginService loginService, IValidation validation)
        {
            _loginService = loginService;
            _validation = validation;
            jObject = new JObject();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<User> Login([FromBody]string userLoginJson)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<UserLogin>(userLoginJson);
                return await _loginService.Login(user.Username, user.Password);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
            return null;
        }

        [HttpPost]
        [Route("LoginViaFacebook")]
        public async Task<User> LoginViaFacebook([FromBody]string userLoginJson)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<FacebookLogin>(userLoginJson);
                return await _loginService.LoginViaFacebook(user.FacebookUserId, user.Email, user.Username);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
            return null;
        }
    }
}
