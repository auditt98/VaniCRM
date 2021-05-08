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

        public AmountByStageReport GetAmountByStage()
        {
            var report = new AmountByStageReport();
            
            AmountByStageData qualified = new AmountByStageData((int)EnumStage.QUALIFIED, "Qualified");
            AmountByStageData valueProposition = new AmountByStageData((int)EnumStage.VALUE_PROPOSITION, "Value Proposition");
            AmountByStageData findKeyContacts = new AmountByStageData((int)EnumStage.FIND_KEY_CONTACTS, "Find key contacts");
            AmountByStageData sendProposal = new AmountByStageData((int)EnumStage.SEND_PROPOSAL, "Send Proposal");
            AmountByStageData review = new AmountByStageData((int)EnumStage.REVIEW, "Review");
            AmountByStageData negotiate = new AmountByStageData((int)EnumStage.NEGOTIATE, "Negotiate");
            AmountByStageData won = new AmountByStageData((int)EnumStage.WON, "Won");
            AmountByStageData lost = new AmountByStageData((int)EnumStage.LOST, "Lost");

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
            
            qualified.amount = qualifiedDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            valueProposition.amount = valuePropositionDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            findKeyContacts.amount = findKeyContactsDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            sendProposal.amount = sendProposalDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            review.amount = reviewDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            negotiate.amount = negotiateDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            won.amount = wonDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            lost.amount = lostDeals.Aggregate(0, (long amount, DEAL next) =>
                                                amount += next.Amount.Value);
            report.chartData.AddRange(new List<AmountByStageData> { qualified, valueProposition, findKeyContacts, sendProposal, review, negotiate, won, lost });
            return report;
        }
    }
}