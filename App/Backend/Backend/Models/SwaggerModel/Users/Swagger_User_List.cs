using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.SwaggerModel.Users
{
    public class Swagger_User_List
    {
        public class U 
        {
            public int id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string skype { get; set; }
        }
        public U[] users { get; set; }
        public PagerResponseModel pageInfo { get; set; }
    }
}
