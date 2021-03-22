using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class CampaignRepository
    {
        DatabaseContext db = new DatabaseContext();
        public IEnumerable<CAMPAIGN> GetAllCampaigns(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = db.CAMPAIGNs.Count();
            }
            if (String.IsNullOrEmpty(q))
            {
                return db.CAMPAIGNs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var campaigns = db.CAMPAIGNs.Where(c => c.Name.ToLower().Contains(q) || c.CAMPAIGN_TYPE.Name.ToLower().Contains(q)).OrderBy(c => c.ID);
            return campaigns;
        }
    }
}