using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class AuthorizationService
    {
        DatabaseContext db = new DatabaseContext();
        UserService _userService = new UserService();
        public List<int> permissions { get; set; }

        public AuthorizationService SetPerm(params int[] p)
        {
            permissions.AddRange(p.ToList());
            return this;
        }
        //public static List<int> Perms(params int[] p)
        //{
        //    permissions = p;
        //    return permissions.ToList();
        //}


        public bool Authorize(int userID)
        {
            var dbUser = _userService.GetOne(userID);
            var groupPerms = dbUser.GROUP.PERMISSIONs.Select(c => c.ID).ToList();

            foreach(var permission in permissions)
            {
                if (!groupPerms.Contains(permission)){
                    return false;
                }
            }

            return true;
        }


    }
}