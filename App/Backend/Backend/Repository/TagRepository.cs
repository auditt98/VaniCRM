using Backend.Domain;
using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class TagRepository
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<TAG> GetAllTags(string query = "")
        {
            var q = query.ToLower();
            if (String.IsNullOrEmpty(q))
            {
                return db.TAGs.OrderBy(c => c.ID);
            }
            var tags = db.TAGs.Where(c => c.Name.ToLower().Contains(q)).OrderBy(c => c.ID);
            return tags;
        }

        public TAG GetOneByName(string name)
        {
            return db.TAGs.Where(c => c.Name.ToLower().Contains(name.ToLower())).FirstOrDefault();
        }

        public TAG Create(string name)
        {
            var newTag = new TAG();
            newTag.Name = name;
            db.TAGs.Add(newTag);
            db.SaveChanges();
            return newTag;
        }
    }
}