using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PostsModels
    {
        public string Data { get; set; } // содержание поста
        public DateTime Time { get; set; } // дата создания
        public List <LikesModels> Likes { get; set; } // лайки
        public int ID { get; set; } // идентификатор

        public PostsModels() { }
    }
}