using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSite_SocialNetwork.Models
{
    public class User
    {
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public Token Token { get; set; }

        [DefaultValue(false)]
        public bool IsAvailable { get; set; }

        public UserIdentity Identity { get; set; }

        public ICollection<Post> Posts { get; set; }

        public string Comment { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public string UserAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    Username = this.Username,
                    Email = this.Email,
                    Password = this.Password,
                    Token = this.Token,
                    IsAvailable = this.IsAvailable,
                    Identity = this.Identity,
                    Posts = this.Posts
                });
            }
        }
    }
}