using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class ReportApiModel
    {
        public string reportName { get; set; }
    }

    public class ChartReportApiModel : ReportApiModel
    {
        public List<Data> data { get; set; }
        public List<Data> data1 { get; set; }
        public List<Data> data2 { get; set; }
        public List<string> labels { get; set; }
        public class Data
        {
            public string x { get; set; }
            public long y { get; set; }
        }
        public ChartReportApiModel()
        {
            data = new List<Data>();
            data1 = new List<Data>();
            data2 = new List<Data>();
            labels = new List<string>();
        }
    }

    public class AmountByStageReport : ReportApiModel
    {
        public class AmountByStageData
        {
            public int id { get; set; }
            public string stage { get; set; }
            public long amount { get; set; }
            public AmountByStageData(int id, string stage)
            {
                this.id = id;
                this.stage = stage;
                amount = 0;
            }
        }

        public List<AmountByStageData> chartData { get; set; }
        public AmountByStageReport()
        {
            chartData = new List<AmountByStageData>();
            reportName = "AMOUNT BY STAGE";
        }
    }
}