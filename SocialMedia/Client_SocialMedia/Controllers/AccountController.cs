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

namespace Client_SocialMedia.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _clientHttp;

        public AccountController()
        {
            _clientHttp = new HttpClient();
            _clientHttp.BaseAddress = new Uri("http://localhost:61154/");
            _clientHttp.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
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
            HttpResponseMessage response = await _clientHttp.PostAsJsonAsync("api/Login", userLogin);
            if (response.IsSuccessStatusCode)
                return View(""); 
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> LoginViaFacebook(string returnUrl)
        {
        }
    }
}