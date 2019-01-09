using Social.Common.Models;
using Social.DAL;
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
            PostRepository p = new PostRepository();
            Post post = new Post
            {
                Comments = new List<Comment> { },
                ID = 1,
                Image = new byte[0],
                Likes = 2,
                Tags = new List<string> { "amazing", "wow" },
                Text = "this is test text"
            };
            p.AddPost(1, post);
        }
    }
}
