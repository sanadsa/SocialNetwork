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
        public void AddUserToDatabase_ShouldReturnTheWantedUser()
        {
            UserRepository AR = new UserRepository();
            AuthenticationUser user = new AuthenticationUser { Email = "1", Password = "1234", Username = "itamar" };
            AR.AddUserToDatabase(user);
        }
    }
}
