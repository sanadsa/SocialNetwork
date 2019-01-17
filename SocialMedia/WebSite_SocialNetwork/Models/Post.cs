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
        public int UserId { get; set; }

        public int PostId { get; set; }

        public int Username { get; set; }

        public DateTime PostDate { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public List<string> Tags { get; set; }

        public EPostPrivacy Privacy { get; set; }

        public string PostAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    UserId = this.UserId,
                    PostId = this.PostId,
                    Username = this.Username,
                    PostDate = this.PostDate,
                    Text = this.Text,
                    Image = this.ImageUrl,
                    Tags = this.Tags,
                    Privacy = this.Privacy
                });
            }
        }
    }
}