using Social.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    public class IncomePost
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Username { get; set; }
        public DateTime PostDate { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public List<string> Tags { get; set; }
        public ePostPrivacy Privacy { get; set; }
    }
}
