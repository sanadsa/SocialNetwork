using BL;
using Client_SocialMedia.Models;
using Dal.UserRepositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserLogin userLogin = new UserLogin() { Password = "ddd", Email = "ddd" };
            var userLoginJson = JsonConvert.SerializeObject(userLogin);
            var user = JsonConvert.DeserializeObject<UserLogin>(userLoginJson);
            Console.WriteLine(user);

            //new LoginService(new UserRepository()).Login(user.Email, user.Password);

            //UserLogin userLogin = new UserLogin() { Username = "sss", Password = "sss" };
            //var jObject = JsonConvert.SerializeObject(userLogin);
            //Console.WriteLine(jObject);
            //Console.WriteLine(jObject.GetType());
            Console.Read();
        }
    }

    class UserLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
