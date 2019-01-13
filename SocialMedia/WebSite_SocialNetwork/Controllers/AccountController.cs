using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSite_SocialNetwork.Constants;
using WebSite_SocialNetwork.Models;

namespace WebSite_SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _client;
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallBack");
                return uriBuilder.Uri;
            }
        }

        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConstantFields.Authentication_BaseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl( new
            {
                client_id = "302110027103118",
                client_secret = "8023d3896c8487f4642f2411a727b391",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic facebookResult = fb.Post("oauth/access_token", new
            {
                client_id = "302110027103118",
                client_secret = "8023d3896c8487f4642f2411a727b391",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = facebookResult.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,last_name,email,id");
            string email = me.email;
            string firstName = me.first_name;
            string lastName = me.last_name;
            string userId = me.id;
            FacebookUser facebookUser = new FacebookUser() { Email = email, FacebookUserId = userId, Username = firstName + " " + lastName };
            var result = _client.PostAsJsonAsync<FacebookUser>(ConstantFields.Authentication_LoginViaFacebook, facebookUser);
            if (result.Result.IsSuccessStatusCode)
            {
                ViewBag.IsLogin = true;
                ViewBag.Username = facebookUser.Username;
                return RedirectToAction("Wall", "Account", user);
            }
            else
            {
                ViewBag.IsLogin = true;
            }
        }

        public ActionResult Wall()
        {
            return View();
        }


        private void AddFacebookUserIdentity(string email, string username, string userId)
        {
            
        }

        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var loginUser = JsonConvert.SerializeObject(new { Username = loginViewModel.Username, Password = loginViewModel.Password });
            var response = _client.PostAsJsonAsync(ConstantFields.Authentication_Login, loginUser).Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.IsLogin = true;
                return RedirectToAction("Index",
                                        "Home",
                                        JsonConvert.DeserializeObject<User>(response.Content.ReadAsAsync<string>().Result));
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult LogOff()
        {
            return View();
        }

        public ActionResult RegisterNewClient(RegisterUser registerUser)
        {
            var register = JsonConvert.SerializeObject(
                new {
                    Username = registerUser.Username,
                    Password = registerUser.Password,
                    Email = registerUser.Email,
                    IsAvilable = registerUser.IsAvilable
                });
            var response = _client.PostAsJsonAsync(ConstantFields.Authentication_Register, register).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "Home");
        }
    }
}