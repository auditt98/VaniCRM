using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backend.Extensions;
namespace Backend.Repository
{
    public class UserRepository
    {
        DatabaseContext db = new DatabaseContext();
        public IEnumerable<USER> GetAll()
        {
            return db.USERs.ToList();
        }

        public (IEnumerable<USER> users, Pager p) GetAllUsers(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();

            if (pageSize == 0)
            {
                pageSize = 10;
            }

            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.USERs.Count(), currentPage, pageSize, 9999);
                return (db.USERs.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }
            var users = db.USERs.Where(c => c.Username.ToLower().Contains(q) || c.FirstName.ToLower().Contains(q) || c.LastName.ToLower().Contains(q) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q) || c.Skype.ToLower().Contains(q)).OrderByDescending(c => c.ID);
            if (users.Count() > 0)
            {
                Pager p = new Pager(users.Count(), currentPage, pageSize, 9999);

                return (users.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (users, null);
            }
        }

        public IEnumerable<USER> Search(string query = "", int pageSize = 0, int currentPage = 1)
        {
            //var pager = new Pager(db.USERs.Count(), currentPage, pageSize);
            var q = query.ToLower();
            
            if (String.IsNullOrEmpty(q))
            {
                return db.USERs.OrderByDescending(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = db.USERs.Where(c => c.Username.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.ToLower().Contains(q) || c.Skype.ToLower().Contains(q) || c.FirstName.ToLower().Contains(q) || c.LastName.ToLower().Contains(q));
            return result.OrderByDescending(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        public USER GetById(int id = 0)
        {
            return db.USERs.Find(id);
        }

        public USER GetByEmail(string email)
        {
            return db.USERs.Where(c => c.Email == email).FirstOrDefault();
        }

        public bool Create(USER u)
        {
            try
            {
                db.USERs.Add(u);
                db.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
            
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.USERs.Find(id);
                if(user != null)
                {
                    db.USERs.Remove(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

    }
}