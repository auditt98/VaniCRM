using Backend.Domain;
using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Services
{
    public class GroupService
    {
        private GroupRepository _groupRepository = new GroupRepository();
        private DatabaseContext db = new DatabaseContext();
        public GroupValidator _groupValidator = new GroupValidator();
        public GroupListApiModel GetGroupList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var apiModel = new GroupListApiModel();
            var dbGroups = _groupRepository.GetAllGroups(query, pageSize, currentPage);
            apiModel.groups = dbGroups.groups.Select(c => new GroupListApiModel.GroupInfo() { id = c.ID, groupName = c.Name, numberOfPermissions = c.PERMISSIONs.Count, numberOfUsers = c.USERs.Count }).ToList();
            apiModel.pageInfo = dbGroups.p;
            return apiModel;
        }

        public GroupDetailApiModel GetOne(int id)
        {
            var dbGroup = _groupRepository.GetOne(id);
            var apiModel = new GroupDetailApiModel();
            if (dbGroup != null)
            {
                apiModel.id = dbGroup.ID;
                apiModel.name = dbGroup.Name;
                apiModel.perms = new List<GroupDetailApiModel.PermissionGroup>();
                var permissionOrders = _groupRepository.GetAllPermissionOrder().OrderBy(c => c.ID);
                foreach (var po in permissionOrders)
                {
                    var permGroup = new GroupDetailApiModel.PermissionGroup();
                    permGroup.permissionGroupId = po.ID;
                    permGroup.permissionGroupName = po.Name;
                    permGroup.permissions = new List<GroupDetailApiModel.Permission>();
                    foreach (var p in po.PERMISSIONs)
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

        public GroupBlankApiModel GetPermsForCreate()
        {
            var apiModel = new GroupBlankApiModel();
            apiModel.perms = new List<GroupBlankApiModel.PermissionGroup>();
            var permissionOrders = _groupRepository.GetAllPermissionOrder().OrderBy(c => c.ID);
            foreach (var po in permissionOrders)
            {
                var permGroup = new GroupBlankApiModel.PermissionGroup();
                permGroup.permissionGroupId = po.ID;
                permGroup.permissionGroupName = po.Name;
                permGroup.permissions = new List<GroupBlankApiModel.Permission>();
                foreach (var p in po.PERMISSIONs)
                {
                    var perm = new GroupBlankApiModel.Permission();
                    perm.id = p.ID;
                    perm.name = p.Name;
                    perm.description = p.Description;
                    perm.isChecked = false;
                    permGroup.permissions.Add(perm);
                }
                apiModel.perms.Add(permGroup);
            }
            return apiModel;
        }

        public bool Delete(int id)
        {
            return _groupRepository.Delete(id);
        }

        public bool Create(GroupCreateApiModel apiModel)
        {
            var validator = _groupValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _groupRepository.Create(apiModel);
            }
            return false;
        }

        public bool Update(int id, GroupCreateApiModel apiModel)
        {
            var validator = _groupValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _groupRepository.Update(id, apiModel);
            }
            return false;
        }
    }
}