﻿using Backend.Models.ApiModel;
using Backend.Repository;
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
    }
}