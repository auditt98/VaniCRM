using Backend.Domain;
using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class CompetitorRepository
    {
        DatabaseContext db = new DatabaseContext();

        public (IEnumerable<COMPETITOR> competitors, Pager p) GetAllCompetitors(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();

            if (pageSize == 0)
            {
                pageSize = 10;
            }

            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.COMPETITORs.Count(), currentPage, pageSize, 9999);
                return (db.COMPETITORs.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }
            var competitors = db.COMPETITORs.Where(c => c.Name.ToLower().Contains(q)).OrderByDescending(c => c.ID);
            if (competitors.Count() > 0)
            {
                Pager p = new Pager(competitors.Count(), currentPage, pageSize, 9999);

                return (competitors.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (competitors, null);
            }
        }
    }
}