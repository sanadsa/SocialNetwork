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
        private HttpClient _clientIdentity;
        private HttpClient _clientSocial;
        private IdentityController _identityController;
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
            _identityController = new IdentityController();
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConstantFields.Authentication_BaseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
            _clientIdentity = new HttpClient();
            _clientIdentity.BaseAddress = new Uri(ConstantFields.Identity_BaseAddress);
            _clientIdentity.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));

            _clientSocial = new HttpClient();
            _clientSocial.BaseAddress = new Uri(ConstantFields.Social_BaseAddress);
            _clientSocial.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConstantFields.Facebook_AppId,
                client_secret = ConstantFields.Facebook_AppSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = ConstantFields.Facebook_ResponseType,
                scope = ConstantFields.Facebook_Scope
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
            dynamic facebookResult = fb.Post(ConstantFields.Facebook_AccessTokenPath, new
            {
                client_id = ConstantFields.Facebook_AppId,
                client_secret = ConstantFields.Facebook_AppSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = facebookResult.access_token;
            Session[ConstantFields.Facebook_AccessTokenSession] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get(ConstantFields.Facebook_GetFieldsUrl);
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
                user.Identity = SetUserIdentity(user.Email);
                user.Posts = GetPosts(user.Token.TokenId);
                return RedirectToAction(ConstantFields.WallView, ConstantFields.Account, user.UserAsJson);
            }
            else
                return RedirectToAction(ConstantFields.IndexView, ConstantFields.Home);
        }

        /// <summary>
        /// Gets the login user and insert to he's cookie the username.
        /// </summary>
        /// <param name="user"></param>
        private void SetUserCookie(User user)
        {
            HttpCookie cookie = new HttpCookie(ConstantFields.UserCookie);
            cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.Values[ConstantFields.UserCookie_username] = user.Username;
        }

        public ActionResult Wall()
        {
            var loginUser = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
            loginUser.Identity = SetUserIdentity(loginUser.Email);
            ViewBag.Username = loginUser.Username;
            return View(ConstantFields.WallView, loginUser);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var loginUser = JsonConvert.SerializeObject(new
            {
                Username = loginViewModel.Username,
                Password = loginViewModel.Password
            });
            var response = _client.PostAsJsonAsync(ConstantFields.Authentication_Login, loginUser).Result;
            var user = response.Content.ReadAsAsync<User>().Result;
            if (response.IsSuccessStatusCode)
            {
                user.Identity = SetUserIdentity(user.Email);
                user.Posts = GetPosts(user.Token.TokenId);
                if (user.Posts == null)
                {
                    user.Posts = new List<Post>();
                    Session[ConstantFields.CurrentUser] = user.UserAsJson;
                    return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
                }
                else
                {
                    Session[ConstantFields.CurrentUser] = user.UserAsJson;
                    return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
                }
            }
            else
                return RedirectToAction(ConstantFields.IndexView, ConstantFields.Home);
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
            var identity = JsonConvert.SerializeObject(
                new
                {
                    Email = registerUser.Email,
                    FirstName = "",
                    LastName = "",
                    Age = 0,    
                    Address = "",
                    WorkAddress = ""
                });
            AddSocialUser(registerUser);
            var registerResponse = _client.PostAsJsonAsync(ConstantFields.Authentication_Register, register).Result;
            var identityRepsonse = _clientIdentity.PostAsJsonAsync(ConstantFields.Identity_CreateUserIdentity, identity).Result;
            if (!identityRepsonse.IsSuccessStatusCode || !registerResponse.IsSuccessStatusCode)
                throw new Exception("Error while register new user");
            return RedirectToAction(ConstantFields.IndexView, ConstantFields.Home);
        }

        public void AddSocialUser(RegisterUser user)
        {
            var social = JsonConvert.SerializeObject(
                new
                {
                    UserId = 1,
                    Token = "",
                    Username = user.Username,
                    Email = user.Email
                });
            var socialResponse = _clientSocial.PostAsJsonAsync(ConstantFields.Social_AddNewUser, social).Result;
            if (!socialResponse.IsSuccessStatusCode)
                throw new Exception("Error while register new social user");
        }

        [HttpPost]
        public ActionResult AddNewPost(UploadPost post)
        {
            var response = _client.PostAsJsonAsync(ConstantFields.Social_AddNewPost, post.PostAsJson).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
            else
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, "Error adding new post");
        }

        public ActionResult AddNewPost()
        {
            if (Session[ConstantFields.CurrentUser] != null)
            {
                var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
                List<string> Folowings = new List<string>() { "njcsakc", "kbfjaba" }; //TODO: connect to action that gives me all the folowings user names.
                ViewBag.Folowings = new SelectList(Folowings);
                return View(ConstantFields.PostView);
            }
            else
            {
                return View(ConstantFields.PostView);
            }
        }

        public ActionResult GetIdentityPartial(UserIdentity userIdentity) => PartialView("_IdentityPartial", userIdentity);

        public ActionResult GetPostPartial(Post post) => PartialView("_PostPartial", post);
        
        private UserIdentity SetUserIdentity(string email) => new IdentityController().GetUserIdentity(email);

        private ICollection<Post> GetPosts(string token) => new SocialController().GetMyPosts(token);

        public ActionResult LogOff() => View();

    }
}