using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class GroupListApiModel
    {
        public List<GroupInfo> groups;
        public Pager pageInfo;
        public class GroupInfo
        {
            public int id { get; set; }
            public string groupName { get; set; }
            public int numberOfUsers { get; set; }
            public int numberOfPermissions { get; set; }
        }

        public GroupListApiModel() { groups = new List<GroupInfo>(); }
    }


    public class GroupDetailApiModel
    {

    }
}