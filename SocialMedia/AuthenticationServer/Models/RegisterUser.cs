using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationServer.Models
{
    public class RegisterUser
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAvilable { get; set; }
    }
}