using System;
using Common.Models;
using Dal.UserRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dal_Test
{
    [TestClass]
    public class AuthenticationRepositoriesTest
    {
        [TestMethod]
        public void AddUserToDatabase_ShouldAddedUser()
        {
            UserRepository AR = new UserRepository();
            AuthenticationUser user1 = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar1" };
            AuthenticationUser user2 = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar2" };
            AuthenticationUser user3 = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar3" };
            AuthenticationUser user4 = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar4" };
            AuthenticationUser user5 = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar5" };
            AuthenticationUser user6 = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar6" };

            AR.AddUserToDatabase(user1);
            AR.AddUserToDatabase(user2);
            AR.AddUserToDatabase(user3);
            AR.AddUserToDatabase(user4);
            AR.AddUserToDatabase(user5);
            AR.AddUserToDatabase(user6);
        }

        [TestMethod]
        public void Login_ShouldReturnTheWantedUser()
        {
            UserRepository AR = new UserRepository();
            var user2 = AR.Login("itamar1", "1234").Result;
        }

        [TestMethod]
        public void LoginViaFacebook_ShouldReturnTheWantedUser()
        {
            UserRepository AR = new UserRepository();
            AuthenticationUser user = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar" };
            AR.AddUserToDatabase(user);
        }

    }
}
