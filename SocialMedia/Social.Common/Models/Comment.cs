using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    /// <summary>
    /// class for comment on post
    /// </summary>
    public class Comment
    {
        public string CommentId { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public List<string> Tags { get; set; }
    }
}
