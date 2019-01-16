using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite_SocialNetwork.Models
{
    public class Profile
    {
        public UserIdentity Identity { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int FollowingCount { get; set; }
        public int FollowersCount { get; set; }
        public int BlockingCount { get; set; }
        public string ProfileAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    Identity = this.Identity,
                    Username = this.Username,
                    Email = this.Email,
                    Following = this.FollowingCount,
                    Followers = this.FollowersCount,
                    Blocking = this.BlockingCount,
                });
            }
        }
    }
}