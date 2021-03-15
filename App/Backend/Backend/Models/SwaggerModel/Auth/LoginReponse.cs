using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.SwaggerModel
{
    public class SwaggerLoginReponse : ResponseModel
    {
        public string status { get; set; }
        public new LoginSwaggerModel data { get; set; }
        public string message { get; set; }

        public class LoginSwaggerModel
        {
            public class T
            {
                public string username { get; set; }
                public string lastName { get; set; }
                public string firstName { get; set; }
                public string jwt { get; set; }
                public int group { get; set; }
            }
            public T user { get; set; }
        }

    }
}