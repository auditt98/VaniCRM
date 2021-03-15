using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.SwaggerModel
{
    public class ResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}