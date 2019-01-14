using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite_SocialNetwork.Constants
{
    public static class ConstantFields
    {
        public const string Headers_Type = "application/json";

        /* Pages names */
        public const string LoginView = "LoginView";

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

        /* Social Service Urls */
        public const string Social_BaseAddress = "http://localhost:33452/";
        public const string Social_GetMyPosts = "api/Feed/GetMyPosts";
    }
}