﻿using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                user.Identity = GetUserIdentity(user.Email);
                user.Posts = GetPosts(user.Token.TokenId);
                return RedirectToAction(ConstantFields.WallView, ConstantFields.Account, user.UserAsJson);
            }
            else
                return RedirectToAction(ConstantFields.IndexView, ConstantFields.Home);
        }

        public ActionResult Wall()
        {
            try
            {
                var loginUser = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
                loginUser.Identity = GetUserIdentity(loginUser.Email);
                loginUser.Posts = GetPosts(loginUser.Email);
                
                ViewBag.Username = loginUser.Username;
                return View(ConstantFields.WallView, loginUser);
            }
            catch (Exception e)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
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
                    if (user != null)
                    {
                        user.Identity = GetUserIdentity(user.Email);
                        user.Posts = GetPosts(user.Email);
                        if (user.Posts == null)
                        {
                            user.Posts = new List<Post>();
                            Session[ConstantFields.CurrentUser] = user.UserAsJson;
                            Session[ConstantFields.IsAvailble] = true;
                            return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
                        }
                        else
                        {
                            Session[ConstantFields.CurrentUser] = user.UserAsJson;
                            Session[ConstantFields.IsAvailble] = true;
                            return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
                        }
                    }
                    else
                    {
                        return RedirectToAction("LoginWithMessage", "Home", new { msg = "User not found" });
                    }
                }
                else
                    return RedirectToAction(ConstantFields.IndexView, ConstantFields.Home);
            }
            catch (Exception e)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = e.Message });
            }
        }

        public ActionResult RegisterNewClient(RegisterUser registerUser)
        {
            var register = JsonConvert.SerializeObject(new
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
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Fuck off" });
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
                RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error while register new social user" });
        }

        [HttpPost]
        public ActionResult AddNewPost(UploadPost post)
        {
            var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
            var newPostJson = JsonConvert.SerializeObject(new
            {
                PostId = Guid.NewGuid().ToString(),
                UserEmail = user.Email,
                PostDate = DateTime.Now,
                Text = post.Text,
                Image = ConvertToByteArray(post.Image),
                Tags = post.Tags,
                Privacy = post.Privacy
            });
            var postRepsonse = _clientSocial.PostAsJsonAsync(ConstantFields.Social_AddNewPost, newPostJson).Result;
            if (!postRepsonse.IsSuccessStatusCode)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = postRepsonse.Content.ReadAsStringAsync().Result });
            }

            var postRes = postRepsonse.Content.ReadAsAsync<Post>().Result;
            var currentUser = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
            currentUser.Posts.Add(postRes);
            Session[ConstantFields.CurrentUser] = currentUser.UserAsJson;
            return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
        }

        private byte[] ConvertToByteArray(HttpPostedFileBase fileBase)
        {
            byte[] data;
            if (fileBase != null)
            {
                using (Stream inputStream = fileBase.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                return data;
            }
            return null;
        }

        [HttpGet]
        public ActionResult AddNewPost()
        {
            if (Session[ConstantFields.CurrentUser] != null)
            {
                var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
                List<ProfileUser> FollowingUsers = GetFollowing(user.Email);
                List<string> Following = new List<string>();
                foreach (var following in FollowingUsers)
                {
                    Following.Add(following.Username);
                }
                ViewBag.Following = new SelectList(Following);
                return View(ConstantFields.PostView);
            }
            else
            {
                return View(ConstantFields.ProfileView);
            }
        }

        public ActionResult GetIdentityPartial(UserIdentity userIdentity) => PartialView("_IdentityPartial", userIdentity);

        public ActionResult GetPostPartial(Post post) => PartialView("_PostPartial", post);

        private UserIdentity GetUserIdentity(string email) => new IdentityController().GetUserIdentity(email);

        private List<ProfileUser> GetFollowing(string email) => new ProfileController().GetFollowing(email);

        private ICollection<Post> GetPosts(string email) => new SocialController().GetMyPosts(email);

        public ActionResult LogOff()
        {
            Session[ConstantFields.CurrentUser] = null;
            return RedirectToAction(ConstantFields.IndexView, ConstantFields.Home);
        }
    }
}