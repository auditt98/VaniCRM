using Backend.Domain;
using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class AccountRepository
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<ACCOUNT> GetUserAccounts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var accounts = dbUser.AccountsAsOwner.ToList();
            accounts.AddRange(dbUser.AccountsAsCollaborator);

            if (pageSize == 0)
            {
                pageSize = accounts.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return accounts.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var result = accounts.OrderBy(c=>c.ID).Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.TaxCode.ToLower().Contains(q.ToLower())).Skip((currentPage - 1) * pageSize).Take(pageSize);

            return result;
        }

        public IEnumerable<ACCOUNT> GetAllAccounts(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = db.ACCOUNTs.Count();
            }
            if (String.IsNullOrEmpty(q))
            {
                return db.ACCOUNTs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var accounts = db.ACCOUNTs.Where(c => c.Name.ToLower().Contains(q) || c.Website.ToLower().Contains(q) || c.Phone.Contains(q) || (c.Owner.FirstName + " " + c.Owner.LastName).ToLower().Contains(q)).OrderBy(c=>c.ID);
            return accounts;
        }

    }
}