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
                return contacts.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = contacts.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.Mobile.Contains(q)).OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();


            return result;
        }

        public IEnumerable<CONTACT> GetAllContacts(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = db.CONTACTs.Count();
            }
            if (String.IsNullOrEmpty(q))
            {
                return db.CONTACTs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var contacts = db.CONTACTs.Where(c => c.Name.ToLower().Contains(q) || c.ACCOUNT.Name.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q)).OrderBy(c => c.ID);
            return contacts;
        }

        public CONTACT GetOne(int id)
        {
            return db.CONTACTs.Find(id);
        }
    }
}