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
            
            var valueProposition = new Data();
            valueProposition.x = "Value Proposition";
            valueProposition.y = valuePropositionDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            
            var findKeyContacts = new Data();
            findKeyContacts.x = "Find Key Contacts";
            findKeyContacts.y = findKeyContactsDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);

            var sendProposal = new Data();
            sendProposal.x = "Send Proposal";
            sendProposal.y = sendProposalDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);

            var review = new Data();
            review.x = "Review";
            review.y = reviewDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);

            var negotiate = new Data();
            negotiate.x = "Negotiate";
            negotiate.y = negotiateDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);

            var won = new Data();
            won.x = "Won";
            won.y = wonDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);

            var lost = new Data();
            lost.x = "Lost";
            lost.y = lostDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            report.data.AddRange(new List<Data> { qualified, valueProposition, findKeyContacts, sendProposal, review, negotiate, won, lost });
            return report;
        }

        public ChartReportApiModel GetTopSalesReport()
        {
            var report = new ChartReportApiModel();
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
    }
}