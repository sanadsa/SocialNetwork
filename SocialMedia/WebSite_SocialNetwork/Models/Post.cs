using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSite_SocialNetwork.Enums;

namespace WebSite_SocialNetwork.Models
{
    public class Post
    {
        public string PostId { get; set; }

        public string UserEmail { get; set; }

        public DateTime PostDate { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public List<string> Tags { get; set; }

        public EpostPrivacy Privacy { get; set; }

        public string PostAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    PostId = this.PostId,
                    UserEmail = this.UserEmail,
                    PostDate = this.PostDate,
                    Text = this.Text,
                    ImageUrl = this.ImageUrl,
                    Tags = this.Tags,
                    Privacy = this.Privacy
                });
            }
        }
    }
}