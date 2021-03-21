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

        public IEnumerable<USER> Search(string query = "", int pageSize = 0, int currentPage = 1)
        {
            //var pager = new Pager(db.USERs.Count(), currentPage, pageSize);
            var q = query.ToLower();
            
            if (String.IsNullOrEmpty(q))
            {
                return db.USERs.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = db.USERs.Where(c => c.Username.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.ToLower().Contains(q) || c.Skype.ToLower().Contains(q) || c.FirstName.ToLower().Contains(q) || c.LastName.ToLower().Contains(q));
            return result.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
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