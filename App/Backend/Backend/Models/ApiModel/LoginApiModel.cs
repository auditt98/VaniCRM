using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class LoginApiModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class ResetPasswordApiModel
    {
        public string key { get; set; }
        public string newPassword { get; set; }
    }
}