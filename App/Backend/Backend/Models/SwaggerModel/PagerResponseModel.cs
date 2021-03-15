using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.SwaggerModel
{
    public class PagerResponseModel
    {
        public class T
        {
            public int totalPage { get; set; }
            public int pageSize { get; set; }
            public int startIndex { get; set; }
            public int endIndex { get; set; }
            public int currentPage { get; set; }
        }
        public T pageInfo { get; set; }
    }
}