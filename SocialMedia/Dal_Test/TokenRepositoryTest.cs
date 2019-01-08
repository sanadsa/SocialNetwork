using System;
using Common.Models;
using Dal.TokenRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dal_Test
{
    [TestClass]
    public class TokenRepositoryTest
    {
        [TestMethod]
        public void AddNewTokenTest()
        {
            AuthenticationUser user = new AuthenticationUser() { Email = "qdjnd@mewmf.fkemf", IsAvilable = true, Password = "1234", Username = "itamar1" };
            TokenRipository tr = new TokenRipository();
            tr.AddNewToken(user);
        }

        [TestMethod]
        public void ChangeUserToken_ShuoldGenerateNewTokenForTheSameUser()
        {
            Token token = new Token() { Username = "itamar1", CreatedTime = "1/8/2019 11:19:14 PM" };
            User user = new User() { Email = "qdjnd@mewmf.fkemf", IsAvailable = true, Password = "ddd", Token = token, Username = "itamar1" };
            TokenRipository tr = new TokenRipository();
            tr.ChangeUserToken(user);
        }
    }
}
