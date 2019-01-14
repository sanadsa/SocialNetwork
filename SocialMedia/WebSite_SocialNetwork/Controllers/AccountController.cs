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

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "302110027103118",
                client_secret = "8023d3896c8487f4642f2411a727b391",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
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
            var JsonUser = JsonConvert.SerializeObject(facebookUser);
            var response = _client.PostAsJsonAsync<string>(ConstantFields.Authentication_LoginViaFacebook, JsonUser).Result;
            var user = response.Content.ReadAsAsync<User>().Result;
            if (response.IsSuccessStatusCode)
            {
                OnLoginProcess(user);
                user.Identity = SetUserIdentity(user.Email);
                user.Posts = GetPosts(user.Token.TokenId);
                return RedirectToAction("Wall", "Account", user);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This method gets a user after login success and checks if the identity is exist.
        /// if the identity no exist it will create new one.
        /// </summary>
        /// <param name="user"></param>
        private void OnLoginProcess(User user)
        {
            var response1 = _client.PostAsJsonAsync(ConstantFields.Identity_CheckIfUserExist, user.Email).Result;
            if (response1.IsSuccessStatusCode)
            {
                var checkUser = response1.Content.ReadAsAsync<bool>().Result;
                if (checkUser == true)
                    SetUserCookie(user);
            }
            else
            {
                UserIdentity userIdentity = new UserIdentity() { Email = user.Email };
                var userIdentityJson = JsonConvert.SerializeObject(userIdentity);
                var response2 = _client.PostAsJsonAsync(ConstantFields.Identity_CreateUserIdentity, userIdentityJson).Result;
                if (!response2.IsSuccessStatusCode)
                    throw new Exception("A problem during saving to the identity database");
                SetUserCookie(user);
            }
        }

        /// <summary>
        /// Gets the login user and insert to he's cookie the username.
        /// </summary>
        /// <param name="user"></param>
        private void SetUserCookie(User user)
        {
            HttpCookie cookie = new HttpCookie("UserCookie");
            cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.Values["User name"] = user.Username;
        }

        public ActionResult Wall(User user)
        {
            ViewBag.IsLogin = true;
            ViewBag.Username = user.Username;
            return View();
        }

        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var loginUser = JsonConvert.SerializeObject(new { Username = loginViewModel.Username, Password = loginViewModel.Password });
            var response = _client.PostAsJsonAsync(ConstantFields.Authentication_Login, loginUser).Result;
            var user = response.Content.ReadAsAsync<User>().Result;
            if (response.IsSuccessStatusCode)
            {
                user.Identity = SetUserIdentity(user.Email);
                user.Posts = GetPosts(user.Token.TokenId);
                return RedirectToAction("Wall", "Account", user);
            }
            return RedirectToAction("Index", "Home");
        }

        private UserIdentity SetUserIdentity(string email) => new IdentityController().GetUserIdentity(email);

        private ICollection<Post> GetPosts(string token) => new SocialController().GetMyPosts(token);

        public ActionResult LogOff()
        {
            return View();
        }

        public ActionResult RegisterNewClient(RegisterUser registerUser)
        {
            var register = JsonConvert.SerializeObject(
                new
                {
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

        public ActionResult GetIdentityPartial(UserIdentity userIdentity)
        {
            return PartialView("_IdentityPartial", userIdentity);
        }

        public ActionResult GetPostPartial(Post post)
        {
            return PartialView("_PostPartial", post);
        }
    }
}