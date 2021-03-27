using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class PriorityRepository
    {
        DatabaseContext db = new DatabaseContext();

        public IEnumerable<PRIORITY> GetAllPriorities()
        {
            return db.PRIORITies;
        }

    }
}