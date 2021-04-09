using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class GroupRepository
    {
        DatabaseContext db = new DatabaseContext();

        public (IEnumerable<GROUP> groups, Pager p) GetAllGroups(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if(pageSize == 0)
            {
                pageSize = 10;
            }
            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.GROUPs.Count(), currentPage, pageSize, 9999);
                return (db.GROUPs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(), pager);
            }
            var groups = db.GROUPs.Where(c => c.Name.Contains(q)).OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            if (groups.Count() > 0)
            {
                Pager p = new Pager(groups.Count(), currentPage, pageSize, 9999);

                return (groups.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (groups, null);
            }
        } 

        public GROUP GetOne(int id)
        {
            return db.GROUPs.Find(id);
        }

        public List<PERMISSION_ORDER> GetAllPermissionOrder()
        {
            return db.PERMISSION_ORDER.ToList();
        }

        public bool Delete(int id)
        {
            var dbGroup = db.GROUPs.Find(id);
            if(dbGroup != null)
            {
                db.GROUPs.Remove(dbGroup);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Create(GroupCreateApiModel g)
        {
            var newGroup = new GROUP();
            try
            {
                newGroup.Name = g.name;
                foreach (var p in g.permissions)
                {
                    var perm = db.PERMISSIONs.Find(p);
                    newGroup.PERMISSIONs.Add(perm);
                }
                db.GROUPs.Add(newGroup);
                db.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
            
        }

        public bool Update(int id, GroupCreateApiModel g)
        {
            var dbGroup = db.GROUPs.Find(id);
            if(dbGroup != null)
            {
                dbGroup.Name = g.name;
                dbGroup.PERMISSIONs.Clear();
                
                foreach(var p in g.permissions)
                {
                    var perm = db.PERMISSIONs.Find(p);
                    dbGroup.PERMISSIONs.Add(perm);
                }
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}