using AuthenticationServer.Models;
using BL;
using Common.Environment_Services;
using Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthenticationServer.Controllers
{
    [RoutePrefix("api/Register")]
    public class RegisterController : ApiController
    {
        private IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public async Task<bool> RegisterNewUser([FromBody]string registerJson)
        {
            try
            {
                if (registerJson != null)
                {
                    var registerUser = JsonConvert.DeserializeObject<RegisterUser>(registerJson);
                    if (registerUser != null)
                        return await _registerService.AddUser(registerUser.Email, registerUser.Username, registerUser.Password);
                    else
                        return false;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return false;
            }
        }
    }
}
