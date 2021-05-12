using Backend.Domain;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Backend.Extensions.Enum;
using static Backend.Models.ApiModel.AmountByStageReport;
using static Backend.Models.ApiModel.ChartReportApiModel;

namespace Backend.Repository
{
    public class ReportRepository
    {
        public DatabaseContext db = new DatabaseContext();
        public UserRepository _userRepository = new UserRepository();
        public ChartReportApiModel GetAmountByStageReport()
        {
            var report = new ChartReportApiModel();
            report.reportName = "AMOUNT BY STAGE";
            var deals = db.DEALs.Where(c => c.STAGE_HISTORY.Count > 0).ToList();

            //qualified deals
            var qualifiedDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.QUALIFIED);
            var valuePropositionDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.VALUE_PROPOSITION);
            var findKeyContactsDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.FIND_KEY_CONTACTS);
            var sendProposalDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.SEND_PROPOSAL);
            var reviewDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.REVIEW);
            var negotiateDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.NEGOTIATE);
            var wonDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.WON);
            var lostDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.LOST);

            var qualified = new Data();
            qualified.x = "Qualified";
            qualified.y = qualifiedDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            var qualifiedCount = new Data();
            qualifiedCount.y = qualifiedDeals.Count();

            var valueProposition = new Data();
            valueProposition.x = "Value Proposition";
            valueProposition.y = valuePropositionDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            var valuePropositionCount = new Data();
            valuePropositionCount.y = valuePropositionDeals.Count();

            var findKeyContacts = new Data();
            findKeyContacts.x = "Find Key Contacts";
            findKeyContacts.y = findKeyContactsDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            var findKeyContactsCount = new Data();
            findKeyContactsCount.y = findKeyContactsDeals.Count();


            var sendProposal = new Data();
            sendProposal.x = "Send Proposal";
            sendProposal.y = sendProposalDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);

            var sendProposalCount = new Data();
            sendProposalCount.y = sendProposalDeals.Count();

            var review = new Data();
            review.x = "Review";
            review.y = reviewDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            var reviewCount = new Data();
            reviewCount.y = reviewDeals.Count();


            var negotiate = new Data();
            negotiate.x = "Negotiate";
            negotiate.y = negotiateDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            var negotiateCount = new Data();
            negotiateCount.y = negotiateDeals.Count();


            var won = new Data();
            won.x = "Won";
            won.y = wonDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            var wonCount = new Data();
            wonCount.y = wonDeals.Count();

            var lost = new Data();
            lost.x = "Lost";
            lost.y = lostDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            var lostCount = new Data();
            lostCount.y = lostDeals.Count();

            report.labels.AddRange(new List<string> { "Qualified", "Value Proposition", "Find Key Contacts", "Send Proposal", "Review", "Negotiate", "Won", "Lost" });
            report.data.AddRange(new List<Data> { qualified, valueProposition, findKeyContacts, sendProposal, review, negotiate, won, lost });
            report.data1.AddRange(new List<Data> { qualifiedCount, valuePropositionCount, findKeyContactsCount, sendProposalCount, reviewCount, negotiateCount, wonCount, lostCount });
            return report;
        }

        public ChartReportApiModel GetTopMarketingsReport()
        {
            var report = new ChartReportApiModel();
            report.reportName = "HIGHEST MARKET-REACH MARKETERS";
            var dict = new Dictionary<USER, int>();
            var leads = db.LEADs.ToList();
            foreach(var lead in leads)
            {
                var createdUser = lead.CreatedUser;
                if(createdUser != null)
                {
                    if (dict.ContainsKey(createdUser))
                    {
                        dict[createdUser] = dict[createdUser] + 1;
                    }
                    else
                    {
                        dict[createdUser] = 1;
                    }
                }
            }

            var convertedAccounts = db.ACCOUNTs.Where(c => c.ConvertFrom != 0).ToList();
            foreach(var account in convertedAccounts)
            {
                var convertedUser = account.CreatedUser;
                if (convertedUser != null)
                {
                    if (dict.ContainsKey(convertedUser))
                    {
                        dict[convertedUser] = dict[convertedUser] + 1;
                    }
                    else
                    {
                        dict[convertedUser] = 1;
                    }
                }
            }

            var ordered = dict.OrderByDescending(x => x.Value).Take(10);
            foreach (var item in ordered)
            {
                var entry = new Data();
                entry.x = item.Key.Username;
                entry.y = item.Value;
                report.data.Add(entry);
            }
            return report;
        }

        public ChartReportApiModel GetTopSalesReport()
        {
            var report = new ChartReportApiModel();
            report.reportName = "TOP SALES HAVE HIGH TURNOVER";
            var dict = new Dictionary<USER, long>();

            var deals = db.DEALs.Where(c => c.STAGE_HISTORY.Count > 0).ToList();
            var wonDeals = deals.Where(c => c.STAGE_HISTORY.OrderByDescending(x => x.ModifiedAt).FirstOrDefault().STAGE_ID == (int)EnumStage.WON);
            foreach(var deal in wonDeals)
            {
                var owner = deal.Owner;
                if(owner != null)
                {
                    if (dict.ContainsKey(owner))
                    {
                        dict[owner] = dict[owner] + deal.Amount.GetValueOrDefault();
                    }
                    else
                    {
                        dict[owner] = deal.Amount.GetValueOrDefault();
                    }
                }
            }
            var ordered = dict.OrderByDescending(x => x.Value).Take(10);
            foreach(var item in ordered)
            {
                var entry = new Data();
                entry.x = item.Key.Username;
                entry.y = item.Value;
                report.data.Add(entry);
            }
            return report;
        }

        public ChartReportApiModel GetKeyAccountsReport()
        {
            var report = new ChartReportApiModel();
            report.reportName = "KEY ACCOUNTS";
            var dict = new Dictionary<ACCOUNT, long>();

            var ordered = db.ACCOUNTs.OrderByDescending(c => c.DEALs.Count).Take(10).ToList();
            
            foreach (var item in ordered)
            {
                //label
                report.labels.Add(item.Name);

                var numDealData = new Data();
                numDealData.x = item.Name;
                numDealData.y = item.DEALs.Count;
                report.data.Add(numDealData);

                var amountData = new Data();
                amountData.x = item.Name;
                amountData.y = item.DEALs.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
                report.data1.Add(amountData);
            }
            return report;
        }

    }
}