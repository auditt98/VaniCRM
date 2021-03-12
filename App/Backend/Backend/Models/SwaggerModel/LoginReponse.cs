using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.SwaggerModel
{
    public class SwaggerLoginReponse
    {
        public class SwaggerDataFormat
        {
            public class SwaggerUserLoginModel
            {
                public string username { get; set; }
                public string lastName { get; set; }
                public string firstName { get; set; }
                public string jwt { get; set; }
            }

            public SwaggerUserLoginModel user { get; set; }
        }
        public string status { get; set; }
        public SwaggerDataFormat data { get; set; }
        public string message { get; set; }

    }
}