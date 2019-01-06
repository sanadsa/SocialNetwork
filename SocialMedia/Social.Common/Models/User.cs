using Social.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public List<string> FollowersIds { get; set; }

        public List<string> Following { get; set; }

        public List<string> BlockedIds { get; set; }
    }
}
