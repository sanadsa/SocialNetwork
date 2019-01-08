using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationServer.Models
{
    public class FacebookLogin
    {
        public string FacebookToken { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
    }
}