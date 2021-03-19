using Backend.Domain;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class LeadService
    {
        LeadRepository _leadRepository = new LeadRepository();


        public List<LEAD> GetUserLeads(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _leadRepository.GetUserLeads(userID, q, currentPage, pageSize).ToList();
        }
    }
}