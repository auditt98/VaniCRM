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
                return accounts.Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var result = accounts.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.TaxCode.ToLower().Contains(q.ToLower()));

            return result;
        }

    }
}