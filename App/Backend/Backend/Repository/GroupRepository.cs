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

        public (IEnumerable<GROUP> groups, Pager p) GetAllGroups(string query = "", int pageSize = 0, int currentPage = 1, List<string> sort = null)
        {
            var q = query.ToLower();
            if(pageSize == 0)
            {
                pageSize = 10;
            }

            //Search and build paging
            var searchResult = db.GROUPs.ToList();

            Pager page;

            if (String.IsNullOrEmpty(q))
            {
                page = new Pager(db.LEADs.Count(), currentPage, pageSize, 9999);
            }
            else
            {
                searchResult = searchResult.Where(c => (c.Name != null && c.Name.ToLower().Contains(q))).ToList();
                if (searchResult.Count() > 0)
                {
                    page = new Pager(searchResult.Count(), currentPage, pageSize, 9999);
                }
                else
                {
                    page = new Pager(0, currentPage, pageSize, 9999);
                }
            }

            var sortResult = searchResult.OrderBy(c => 1);


            if (sort != null)
            {
                if (sort.Count() > 0)
                {
                    foreach (var sortQuery in sort)
                    {
                        if (sortQuery.Contains("desc."))
                        {
                            var s = sortQuery.Replace("desc.", "");
                            switch (s)
                            {
                                case "groupName":
                                    sortResult = sortResult.ThenByDescending(c => c.Name);
                                    break;
                                case "numberOfUsers":
                                    sortResult = sortResult.ThenByDescending(c => c.USERs.Count);
                                    break;
                                case "numberOfPermissions":
                                    sortResult = sortResult.ThenByDescending(c => c.PERMISSIONs.Count);
                                    break;
                                default:
                                    sortResult = sortResult.ThenByDescending(c => c.ID);
                                    break;
                            }


                        }
                        else if (sortQuery.Contains("asc."))
                        {
                            var s = sortQuery.Replace("asc.", "");
                            switch (s)
                            {
                                case "groupName":
                                    sortResult = sortResult.ThenBy(c => c.Name);
                                    break;
                                case "numberOfUsers":
                                    sortResult = sortResult.ThenBy(c => c.USERs.Count);
                                    break;
                                case "numberOfPermissions":
                                    sortResult = sortResult.ThenBy(c => c.PERMISSIONs.Count);
                                    break;
                                default:
                                    sortResult = sortResult.ThenByDescending(c => c.ID);
                                    break;
                            }
                        }
                        else
                        {
                            sortResult = sortResult.ThenByDescending(c => c.ID);
                        }
                    }
                }
                else
                {
                    sortResult = sortResult.ThenByDescending(c => c.ID);
                }
            }
            else
            {
                sortResult = sortResult.ThenByDescending(c => c.ID);
            }

            //Take
            var takeResult = sortResult.Skip((currentPage - 1) * pageSize).Take(pageSize);
            return (takeResult, page);
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