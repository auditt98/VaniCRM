using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    public class ResponseFormat
    {
        public string status { get; set; }
        public object data { get; set; }
        public string message { get; set; }

        public ResponseFormat() { }

        public ResponseFormat(string status)
        {
            this.status = status;
        }

        public static ResponseFormat Success { get { return new ResponseFormat("success"); }}
        public static ResponseFormat Fail { get { return new ResponseFormat("fail"); } }
        public static ResponseFormat Error { get { return new ResponseFormat("error"); } }


    }
}