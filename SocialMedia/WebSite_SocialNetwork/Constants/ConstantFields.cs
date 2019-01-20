using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite_SocialNetwork.Models;

namespace WebSite_SocialNetwork.Constants
{
    public static class ConstantFields
    {
        public const string Headers_Type = "application/json";

        /* Pages names */
        public const string LoginView = "LoginView";
        public const string ProfileView = "Profile";
        public const string OtherProfileView = "OtherProfile";
        public const string PostView = "AddPostView";
        public const string WallView = "Wall";
        public const string IndexView = "Index";
        public const string ErrorView = "Error";

        /* Controller Names */
        public const string Account = "Account";
        public const string Home = "Home";
        public const string Identity = "Identity";
        public const string Social = "Social";
        public const string Profile = "Profile";

        /* Authentication Urls */
        public const string Authentication_BaseAddress = "http://localhost:61154/";
        public const string Authentication_Login = "api/Login/Login";
        public const string Authentication_LoginViaFacebook = "api/Login/LoginViaFacebook";
        public const string Authentication_Register = "api/Register/RegisterNewUser";

        /* Identity Service Urls */
        public const string Identity_BaseAddress = "http://localhost:33452/";
        public const string Identity_CreateUserIdentity = "api/Identity/CreateUserIdentity";
        public const string Identity_CheckIfUserExist = "api/Identity/CheckIfUserExist";
        public const string Identity_UpdateUserIdentity = "api/Identity/UpdateUserIdentity";
        public const string Identity_GetUserIdentity = "api/Identity/GetUserIdentity";
        public static string UpdateUserIdentity(UserIdentity identity) => $"api/Identity/UpdateUserIdentity?userIdentity={identity}";

        /* Social Service Urls */
        public const string Social_BaseAddress = "http://localhost:13608/";
        public const string Social_GetMyPosts = "api/Feed/GetMyPosts";
        public const string Social_AddNewPost = "api/Post/CreatePost";
        public const string Social_AddNewUser = "api/User/CreateUser";
        public const string Social_GetFollowing = "api/User/GetFollowing";
        public const string Social_GetFollowers = "api/User/GetFollowers";
        public const string Social_GetBlocked = "api/User/GetBlocked";

        /* TempData Keys */
        public const string CurrentUser = "CurrentUser";
        public const string ProfileUser = "ProfileUser";

        /* Cookie Names */
        public const string UserCookie = "UserCookie";
        public const string UserCookie_username = "User name";
        public const string IsAvailble = "application/json";

        /* Facebook Login */
        public const string Facebook_AppId = "302110027103118";
        public const string Facebook_AppSecret = "8023d3896c8487f4642f2411a727b391";
        public const string Facebook_AccessTokenPath = "oauth/access_token";
        public const string Facebook_AccessTokenSession = "AccessToken";
        public const string Facebook_GetFieldsUrl = "me?fields=link,first_name,last_name,email,id";
        public const string Facebook_ResponseType = "code";
        public const string Facebook_Scope = "email";
    }
}