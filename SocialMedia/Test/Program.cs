using Common.Models;
using Identity.Common.Model;
using Newtonsoft.Json;
using Social.Common.Enums;
using Social.Common.Models;
using Social.DAL;
using Social.Service_Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //PostRepository p = new PostRepository();
            //Post post = new Post
            //{
            //    PostId = 1,
            //    Image = new byte[0],
            //    Tags = new List<string> { "amazing", "wow" },
            //    Text = "this is test text"
            //};
            //p.AddPost(1, post);

            //UserServiceTest service = new UserServiceTest();
            //service.AddUser();
            User user = new User()
            {
                Username = "itamar",
                Email = "itamardaisy@gmail.com",
                Password = "1234",
                Identity = new UserIdentity() { Email = "itamardaisy@gmail.com", Address = "dddd", Age = 0, FirstName = "fdf", LastName = "fwfw", WorkAddress = "dwqdqd" },
                IsAvailable = true,
                Posts = new List<Post>(),
                Token = new Token() { CreatedTime = DateTime.Now.ToLongDateString(), IsValid = true, TokenId = "fwefeqgrgh4===fewf", Username = "itamar" }
            };
            Console.WriteLine(user.UserAsJson);
            Console.Read();
        }
    }

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
