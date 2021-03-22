using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class LeadRepository
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<LEAD> GetUserLeads(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var leads = dbUser.LeadsAsOwner.ToList();
            if (pageSize == 0)
            {
                pageSize = leads.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return leads.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = leads.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.Website.ToLower().Contains(q.ToLower()) || c.Skype.ToLower().Contains(q.ToLower()) || c.CompanyName.ToLower().Contains(q.ToLower())).OrderBy(c => c.ID).Skip((currentPage - 1)*pageSize).Take(pageSize).ToList();
            return result;
        }

        public IEnumerable<LEAD> GetAllLeads(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = db.LEADs.Count();
            }
            if (String.IsNullOrEmpty(q))
            {
                return db.LEADs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var leads = db.LEADs.Where(c => c.Name.ToLower().Contains(q) || c.CompanyName.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q) || c.LEAD_SOURCE.Name.ToLower().Contains(q) || (c.Owner.FirstName + " " + c.Owner.LastName).ToLower().Contains(q) || c.PRIORITY.Name.ToLower().Contains(q)).OrderBy(c => c.ID).ToList();
            return leads;
        }
    }
}