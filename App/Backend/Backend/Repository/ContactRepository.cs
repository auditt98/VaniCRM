using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class ContactRepository
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<CONTACT> GetUserContacts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var contacts = dbUser.ContactsAsOwner.ToList();
            contacts.AddRange(dbUser.ContactsAsCollaborator);

            if (pageSize == 0)
            {
                pageSize = contacts.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return contacts.Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var result = contacts.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.Mobile.Contains(q) || c.Skype.ToLower().Contains(q.ToLower()));


            return result;
        }
    }
}