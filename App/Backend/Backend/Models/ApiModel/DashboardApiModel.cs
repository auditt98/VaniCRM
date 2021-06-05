using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;

namespace Backend.Models.ApiModel
{

    public class DashboardApiModel
    {
        public List<S> stages { get; set; }
        public DashboardApiModel()
        {
            stages = new List<S>();
        }


        public class S
        {
            public int stageID { get; set; }
            public string stageName { get; set; }
            public int probability { get; set; }
            public List<D> deals { get; set; }
            public S()
            {
                deals = new List<D>();
            }
        }

        public class D
        {
            public int dealID { get; set; }
            public string dealName { get; set; }
            public int accountID { get; set; }
            public string accountName { get; set; }
            public int ownerID { get; set; }
            public string ownerUsername { get; set; }
            public List<T> tags { get; set; }
            public long expectedRevenue { get; set; }
            public string priority { get; set; }
            public D()
            {
                tags = new List<T>();
            }

        }

        public class T
        {
            public int tagID { get; set; }
            public string tagName { get; set; }
        }

    }
}