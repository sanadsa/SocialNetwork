using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSite_SocialNetwork.Models
{
    public class UserIdentity
    {
        public string Email { get; set; }

        [DefaultValue(" ")]
        public string FirstName { get; set; }

        [DefaultValue(" ")]
        public string LastName { get; set; }

        [DefaultValue(0)]
        public int Age { get; set; }

        [DefaultValue(" ")]
        public string Address { get; set; }

        [DefaultValue(" ")]
        public string WorkAddress { get; set; }
    }
}
