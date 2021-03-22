using Backend.Domain;
using Backend.Models.ApiModel;
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

        public List<LeadListApiModel.LeadInfo> GetLeadList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbLeads = _leadRepository.GetAllLeads(query, pageSize, currentPage);
            return dbLeads.Select(c => new LeadListApiModel.LeadInfo() { id = c.ID, companyName = c.CompanyName, email = c.Email, phone = c.Phone, leadSource = c.LEAD_SOURCE.Name, leadOwner = c.Owner.FirstName + " " + c.Owner.LastName, priority = c.PRIORITY.Name }).ToList();
        }

        public List<LEAD> GetUserLeads(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _leadRepository.GetUserLeads(userID, q, currentPage, pageSize).ToList();
        }
    }
}