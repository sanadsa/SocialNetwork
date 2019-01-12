using Social.Common.Models;
using Social.DAL;
using Social.Service_Test;
using System;
using System.Collections.Generic;
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

            UserServiceTest service = new UserServiceTest();
            service.AddUser();
        }
    }
}
