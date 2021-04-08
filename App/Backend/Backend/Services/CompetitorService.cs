using Backend.Models.ApiModel;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class CompetitorService
    {
        CompetitorRepository _competitorRepository = new CompetitorRepository();

        public CompetitorListApiModel GetCompetitorList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbCompetitor = _competitorRepository.GetAllCompetitors(query, pageSize, currentPage);
            var apiModel = new CompetitorListApiModel();

            apiModel.competitors = dbCompetitor.competitors.Select(c => new CompetitorListApiModel.CompetitorInfo() { id = c.ID, name = c.Name, strengths = c.Strengths, weaknesses = c.Weaknesses, website = c.Website}).ToList();
            apiModel.pageInfo = dbCompetitor.p;
            return apiModel;
        }
    }
}