using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class LikesModels
    {
        public int UserID { get; set; } 
        public int PostID { get; set; }
        public DateTime Time { get; set; }

        public LikesModels() { }
    }
}