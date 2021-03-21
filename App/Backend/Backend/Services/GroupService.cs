using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class GroupService
    {
        GroupRepository _groupRepository = new GroupRepository();
        DatabaseContext db = new DatabaseContext();
        public List<GroupListApiModel.GroupInfo> GetGroupList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbGroups = _groupRepository.GetAllGroups(query, pageSize, currentPage);
            return dbGroups.Select(c => new GroupListApiModel.GroupInfo() { id = c.ID, groupName = c.Name, numberOfPermissions = c.PERMISSIONs.Count, numberOfUsers = c.USERs.Count }).ToList();
        }

        public GroupDetailApiModel GetOne(int id)
        {
            var dbGroup = _groupRepository.GetOne(id);
            var apiModel = new GroupDetailApiModel();
            if(dbGroup != null)
            {
                apiModel.id = dbGroup.ID;
                apiModel.name = dbGroup.Name;
                apiModel.perms = new List<GroupDetailApiModel.PermissionGroup>();
                var permissionOrders = _groupRepository.GetAllPermissionOrder().OrderBy(c=>c.ID);
                foreach(var po in permissionOrders)
                {
                    var permGroup = new GroupDetailApiModel.PermissionGroup();
                    permGroup.permissionGroupId = po.ID;
                    permGroup.permissionGroupName = po.Name;
                    permGroup.permissions = new List<GroupDetailApiModel.Permission>();
                    foreach(var p in po.PERMISSIONs)
                    {
                        var perm = new GroupDetailApiModel.Permission();
                        perm.id = p.ID;
                        perm.name = p.Name;
                        perm.description = p.Description;
                        perm.isChecked = dbGroup.PERMISSIONs.Contains(p);
                        permGroup.permissions.Add(perm);
                    }
                    apiModel.perms.Add(permGroup);
                }
                return apiModel;
            }
            else
            {
                return null;
            }

        }

    }
}