using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_SocialMedia.Models
{
    public class UserIdentityModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string WorkAddress { get; set; }
    }
}