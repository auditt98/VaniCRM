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
        public int id { get; set; }
        public string name { get; set; }
        public List<PermissionGroup> perms { get; set; }
        public GroupDetailApiModel() { perms = new List<PermissionGroup>(); }

        public class PermissionGroup
        {
            public int permissionGroupId { get; set; }
            public string permissionGroupName { get; set; }
            public List<Permission> permissions { get; set; }
            public PermissionGroup() { permissions = new List<Permission>(); }
        }

        public class Permission
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public bool isChecked { get; set; }
            public Permission() { }
        }
    }

    public class GroupBlankApiModel
    {
        public List<PermissionGroup> perms { get; set; }
        public GroupBlankApiModel() { perms = new List<PermissionGroup>(); }

        public class PermissionGroup
        {
            public int permissionGroupId { get; set; }
            public string permissionGroupName { get; set; }
            public List<Permission> permissions { get; set; }
            public PermissionGroup() { permissions = new List<Permission>(); }
        }

        public class Permission
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public bool isChecked { get; set; }
            public Permission() { }
        }
    }

    public class GroupCreateApiModel
    {
        public string name { get; set; }
        public List<int> permissions { get; set; }
    }

}