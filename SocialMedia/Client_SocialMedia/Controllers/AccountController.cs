using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Client_SocialMedia.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Client_SocialMedia.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Client_SocialMedia.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _clientHttp;

        public AccountController()
        {
            _clientHttp = new HttpClient();
            _clientHttp.BaseAddress = new Uri(ConstantFields.Authentication_BaseAddress);
            _clientHttp.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            UserLogin userLogin = new UserLogin() { Username = loginViewModel.Username, Password = loginViewModel.Password };
            var userLoginJson = JsonConvert.SerializeObject(userLogin);
            HttpResponseMessage response = await _clientHttp.PostAsJsonAsync(ConstantFields.Authentication_Login, userLoginJson);
            if (response.IsSuccessStatusCode)
                return View(""); //TODO should return the view with the user model.
            else
                return View(""); //TODO should return the view with the user model.
        }

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> LoginViaFacebook(string returnUrl)
        //{
        //    return View();
        //}
    }
}