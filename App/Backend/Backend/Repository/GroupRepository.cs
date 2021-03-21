using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class GroupRepository
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<GROUP> GetAllGroups(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if(pageSize == 0)
            {
                pageSize = db.GROUPs.Count();
            }
            if (String.IsNullOrEmpty(q))
            {
                return db.GROUPs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var groups = db.GROUPs.Where(c => c.Name.Contains(q)).OrderBy(c => c.ID).ToList();
            return groups;
        } 

    }
}