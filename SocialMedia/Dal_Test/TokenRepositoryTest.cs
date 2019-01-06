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
            AuthenticationUser user = new AuthenticationUser() { Email = "qdjnd@mewmf.fkemf", IsAvilable = true, Password = "dwqd", Username = "qdw" };
            TokenRipository tr = new TokenRipository();
            tr.AddNewToken(user);
        }

        [TestMethod]
        public void ChangeUserToken_ShuoldGenerateNewTokenForTheSameUser()
        {
            User user = new User() { Email = "qdjnd@mewmf.fkemf", IsAvailable = true, Password = "ddd", TokenId = "Gl8N2yTU5UCo3f7zW9lutQ==", Username = "ddd" };
            TokenRipository tr = new TokenRipository();
            tr.ChangeUserToken(user);
        }
    }
}
