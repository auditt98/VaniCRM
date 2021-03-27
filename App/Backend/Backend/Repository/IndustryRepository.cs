using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class IndustryRepository
    {
        DatabaseContext db = new DatabaseContext();
        public IEnumerable<INDUSTRY> GetAllIndustries()
        {
            return db.INDUSTRies;
        }
    }
}