using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSite_SocialNetwork.Enums;

namespace WebSite_SocialNetwork.Models
{
    public class UploadPost
    {
        public string PostId { get; set; }

        public string UserEmail { get; set; }

        public DateTime PostDate { get;  set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string Text { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Picture")]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Tag Your Frinds")]
        public List<string> Tags { get; set; }

        public ePostPrivacy Privacy { get; set; }

        public string PostAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    PostId = this.PostId,
                    UserEmail = this.UserEmail,
                    PostDate = DateTime.Now,
                    Text = this.Text,
                    Image = this.Image,
                    Tags = this.Tags,
                    Privacy = this.Privacy
                });
            }
        }
    }
}