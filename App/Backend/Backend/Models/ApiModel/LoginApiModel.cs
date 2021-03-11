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
        public string refreshToken { get; set; }
        public string jwtToken { get; set; }
        public string seriesIdentifier { get; set; }
    }
}