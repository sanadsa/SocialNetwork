using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite_SocialNetwork.Models
{
    public class ProfileUser
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}