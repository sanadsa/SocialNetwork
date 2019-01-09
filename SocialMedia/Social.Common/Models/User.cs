using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    /// <summary>
    /// class for user in the social network app
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> FollowersIds { get; set; }
        public List<string> Following { get; set; }
        public List<string> BlockedIds { get; set; }
    }
}
