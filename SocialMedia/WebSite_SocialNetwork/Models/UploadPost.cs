﻿using Newtonsoft.Json;
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
        public string Username { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public DateTime PostDate { get { return PostDate; } set { PostDate = DateTime.Now; } }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Content")]
        public string Text { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Picture")]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Tag Your Frinds")]
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
                    Image = this.Image,
                    Tags = this.Tags,
                    Privacy = this.Privacy
                });
            }
        }
    }
}