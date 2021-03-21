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

        public List<GroupListApiModel.GroupInfo> GetGroupList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbGroups = _groupRepository.GetAllGroups(query, pageSize, currentPage);
            return dbGroups.Select(c => new GroupListApiModel.GroupInfo() { id = c.ID, groupName = c.Name, numberOfPermissions = c.PERMISSIONs.Count, numberOfUsers = c.USERs.Count }).ToList();
        }

    }
}