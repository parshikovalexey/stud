using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ChangePasswordModels
    {
        public int id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string repeatNewPassword { get; set; }
    }
}
