using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite_SocialNetwork.Models
{
    public class Token
    {
        public Token()
        {

        }
        public string Username { get; set; }

        public string CreatedTime { get; set; }

        public string TokenId { get; set; }

        public bool IsValid { get; set; }
    }
}