using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class DealRepository
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<DEAL> GetAllDeals(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = db.DEALs.Count();
            }
            if (String.IsNullOrEmpty(q))
            {
                return db.DEALs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var deals = db.DEALs.Where(c => c.Name.ToLower().Contains(q) || c.PRIORITY.Name.ToLower().Contains(q) || c.ACCOUNT.Name.ToLower().Contains(q)).OrderBy(c => c.ID);
            return deals;
        }
    }
}