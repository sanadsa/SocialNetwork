using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social.Common.Models;
using System.Web.Http;
using Newtonsoft.Json;

namespace Social.Service_Test
{
    [TestClass]
    public class UserServiceTest
    {
        private HttpClient _client;
        private string uri = "http://localhost:13608/";

        public UserServiceTest()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(uri);
        }

        [TestMethod]
        [HttpPost]
        public void AddUser()
        {
            User user = new User
            {
                UserId = 88,
                Username = "nitzan",
                Email = "n@gmail.com",
                Token = "t543tg"
            };
            string json = JsonConvert.SerializeObject(user);
            var result = _client.PostAsync($"api/User/CreateUser?user={user}", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.Content.ReadAsStringAsync().Result);
            }

            string response = result.Content.ReadAsStringAsync().Result;
        }
    }
}
