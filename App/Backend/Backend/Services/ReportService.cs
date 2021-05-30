using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class ReportService
    {
        public ReportRepository _reportRepository = new ReportRepository();
        public ChartReportApiModel GetAmountByStageReport()
        {
            return _reportRepository.GetAmountByStageReport();   
        }

        public ChartReportApiModel GetTopSalesReport()
        {
            return _reportRepository.GetTopSalesReport();
        }

        public ChartReportApiModel GetTopMarketingsReport()
        {
            return _reportRepository.GetTopMarketingsReport();
        }

        public ChartReportApiModel GetKeyAccountsReport()
        {
            return _reportRepository.GetKeyAccountsReport();
        }

        public ChartReportApiModel GetAccountsByIndustryReport()
        {
            return _reportRepository.GetAccountsByIndustryReport();
        }

        public ChartReportApiModel GetRevenueComparisonReport()
        {
            return _reportRepository.GetRevenueComparisonReport();
        }

        public ExportablesApiModel GetExportables(int currentPage = 1, int pageSize = 10)
        {
            if(pageSize == 0)
            {
                pageSize = 10;
            }
            if(currentPage == 0)
            {
                currentPage = 1;
            }
            var exportables = new ExportablesApiModel();
            List<ExportablesApiModel.Exportables> list = new List<ExportablesApiModel.Exportables>();
            list.Add(new ExportablesApiModel.Exportables("Potential Customers Report", $"{StaticStrings.ServerHost}reports/exportables/leads", false));
            list.Add(new ExportablesApiModel.Exportables("Customers Report", $"{StaticStrings.ServerHost}reports/exportables/accounts", false));
            list.Add(new ExportablesApiModel.Exportables("Deals Report", $"{StaticStrings.ServerHost}reports/exportables/deals", true));
            list.Add(new ExportablesApiModel.Exportables("Revenue Report", $"{StaticStrings.ServerHost}reports/exportables/revenue", true));
            list.Add(new ExportablesApiModel.Exportables("Campaign Report", $"{StaticStrings.ServerHost}reports/exportables/campaigns", false));
            exportables.exportables = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            exportables.pageInfo = new Extensions.Pager(list.Count(), currentPage, pageSize, 99999);

            return exportables;
        }

        //public 
    }
}