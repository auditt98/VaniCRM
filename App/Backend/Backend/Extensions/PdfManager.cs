using Backend.Services;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    public class PdfManager
    {
        public string targetFolder { get; set; }
        public string fileName { get; set; }
        public string pdfDestination { get; set; }
        public string logoFile { get; set; }
        public PdfDocument pdfDoc { get; set; }
        public Document document { get; set; }
        public int headerFontSize { get; set; }
        public int fontSize { get; set; }

        public PdfManager(string fileName)
        {
            targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            this.fileName = fileName;
            pdfDestination = System.IO.Path.Combine(targetFolder, fileName);
            logoFile = System.IO.Path.Combine(targetFolder, "logo.png");
            headerFontSize = 18;
            fontSize = 11;
        }

        public PdfManager GenerateLeadsReport()
        {
            LeadService _leadService = new LeadService();
            FileInfo file = new FileInfo(pdfDestination);
            var fontDestination = System.IO.Path.Combine(targetFolder, "times.ttf");
            //document settings
            pdfDoc = new PdfDocument(new PdfWriter(pdfDestination));
            pdfDoc.SetDefaultPageSize(PageSize.A4);
            document = new Document(pdfDoc, PageSize.A4, false).SetFontSize(12);
            //set header
            PdfFont font = PdfFontFactory.CreateFont(fontDestination, PdfEncodings.IDENTITY_H, true);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            Paragraph docHeader = new Paragraph("Potential Customers (Leads) Report").SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(headerFontSize).SetFont(bold);
            //set logo
            Image logo = new Image(ImageDataFactory
            .Create(logoFile))
            .SetTextAlignment(TextAlignment.LEFT).SetHeight(60).SetWidth(60);

            document.Add(logo);
            document.Add(docHeader);

            var currentItem = 1;
            Table table = new Table(UnitValue.CreatePercentArray(7), false).SetFont(font);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetFixedLayout();

            Cell c11 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("No."));
            Cell c12 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Lead's name"));
            Cell c13 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Company"));
            Cell c14 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Email Address"));
            Cell c15 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Phone Number"));
            Cell c16 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Lead Owner"));
            Cell c17 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Source"));
            table.AddHeaderCell(c11);
            table.AddHeaderCell(c12);
            table.AddHeaderCell(c13);
            table.AddHeaderCell(c14);
            table.AddHeaderCell(c15);
            table.AddHeaderCell(c16);
            table.AddHeaderCell(c17);

            var leadList = _leadService.GetLeadList(pageSize: 99999, currentPage: 1, sort: new List<string> { "asc.name" });
            foreach (var lead in leadList.leads)
            {

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(currentItem.ToString())));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(lead.name ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(lead.companyName ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(lead.email ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(lead.phone ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(lead.leadOwner ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(lead.leadSource ?? "")));
                currentItem++;
            }

            document.Add(table);

            document.Add(new Paragraph(new Text("\n")));

            var dateSignature = new Paragraph("Hanoi, " + DateTime.Now.ToString("D")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(30);
                //.SetFixedPosition(PageSize.A4.GetRight() - 170, PageSize.A4.GetBottom() + 80, 150);
            document.Add(dateSignature);
            document.Add(new Paragraph(new Text("\n\n")));
            document.Add(new Paragraph(new Text("Signature: ________________________")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(0));


            int numberOfPages = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= numberOfPages; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i.ToString())),
                    559, PageSize.A4.GetBottom() + 20, i, TextAlignment.RIGHT,//806
                    VerticalAlignment.BOTTOM, 0);
            }
            document.Close();
            return this;
        }

        public PdfManager GenerateAccountsReport()
        {
            AccountService _accountService = new AccountService();
            FileInfo file = new FileInfo(pdfDestination);
            var fontDestination = System.IO.Path.Combine(targetFolder, "times.ttf");
            //document settings
            pdfDoc = new PdfDocument(new PdfWriter(pdfDestination));
            pdfDoc.SetDefaultPageSize(PageSize.A4);
            document = new Document(pdfDoc, PageSize.A4, false).SetFontSize(12);
            //set header
            PdfFont font = PdfFontFactory.CreateFont(fontDestination, PdfEncodings.IDENTITY_H, true);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            Paragraph docHeader = new Paragraph("Customers (Accounts) Report").SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(headerFontSize).SetFont(bold);
            //set logo
            Image logo = new Image(ImageDataFactory
            .Create(logoFile))
            .SetTextAlignment(TextAlignment.LEFT).SetHeight(60).SetWidth(60);

            document.Add(logo);
            document.Add(docHeader);

            var currentItem = 1;
            Table table = new Table(UnitValue.CreatePercentArray(6), false).SetFont(font);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetFixedLayout();

            Cell c11 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("No."));
            Cell c12 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Account's name"));
            Cell c13 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Website"));
            Cell c14 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Email"));
            Cell c15 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Phone Number"));
            Cell c16 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Account Owner"));

            table.AddHeaderCell(c11);
            table.AddHeaderCell(c12);
            table.AddHeaderCell(c13);
            table.AddHeaderCell(c14);
            table.AddHeaderCell(c15);
            table.AddHeaderCell(c16);

            var accountList = _accountService.GetAccountList(pageSize: 99999, currentPage: 1, sort: new List<string> { "asc.name" });
            foreach (var account in accountList.accounts)
            {

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(currentItem.ToString())));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(account.name ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(account.website ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(account.email ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(account.phone ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(account.owner ?? "")));
                currentItem++;
            }

            document.Add(table);

            document.Add(new Paragraph(new Text("\n")));

            var dateSignature = new Paragraph("Hanoi, " + DateTime.Now.ToString("D")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(30);
            document.Add(dateSignature);
            document.Add(new Paragraph(new Text("\n\n")));
            document.Add(new Paragraph(new Text("Signature: ________________________")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(0));


            int numberOfPages = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= numberOfPages; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i.ToString())),
                    559, PageSize.A4.GetBottom() + 20, i, TextAlignment.RIGHT,//806
                    VerticalAlignment.BOTTOM, 0);
            }
            document.Close();
            return this;
        }

        public PdfManager GenerateCampaignsReport()
        {
            CampaignService _campaignService = new CampaignService();
            FileInfo file = new FileInfo(pdfDestination);
            var fontDestination = System.IO.Path.Combine(targetFolder, "times.ttf");
            //document settings
            pdfDoc = new PdfDocument(new PdfWriter(pdfDestination));
            pdfDoc.SetDefaultPageSize(PageSize.A4);
            document = new Document(pdfDoc, PageSize.A4, false).SetFontSize(12);
            //set header
            PdfFont font = PdfFontFactory.CreateFont(fontDestination, PdfEncodings.IDENTITY_H, true);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            Paragraph docHeader = new Paragraph("Campaigns Report").SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(headerFontSize).SetFont(bold);
            //set logo
            Image logo = new Image(ImageDataFactory
            .Create(logoFile))
            .SetTextAlignment(TextAlignment.LEFT).SetHeight(60).SetWidth(60);

            document.Add(logo);
            document.Add(docHeader);

            var currentItem = 1;
            Table table = new Table(UnitValue.CreatePercentArray(7), false).SetFont(font);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetFixedLayout();

            Cell c11 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("No."));
            Cell c12 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Campaign's Name"));
            Cell c13 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Type"));
            Cell c14 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Status"));
            Cell c15 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Start Date"));
            Cell c16 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("End Date"));
            Cell c17 = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph("Owner"));

            table.AddHeaderCell(c11);
            table.AddHeaderCell(c12);
            table.AddHeaderCell(c13);
            table.AddHeaderCell(c14);
            table.AddHeaderCell(c15);
            table.AddHeaderCell(c16);
            table.AddHeaderCell(c17);

            var campaignList = _campaignService.GetCampaignList(pageSize: 99999, currentPage: 1, sort: new List<string> { "asc.name" });
            foreach (var campaign in campaignList.campaigns)
            {

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(currentItem.ToString())));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(campaign.name ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(campaign.type ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(campaign.status ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(campaign.startDate.ToString("D") ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(campaign.endDate.ToString("D") ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(campaign.owner ?? "")));
                currentItem++;
            }

            document.Add(table);

            document.Add(new Paragraph(new Text("\n")));

            var dateSignature = new Paragraph("Hanoi, " + DateTime.Now.ToString("D")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(30);
            document.Add(dateSignature);
            document.Add(new Paragraph(new Text("\n\n")));
            document.Add(new Paragraph(new Text("Signature: ________________________")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(0));


            int numberOfPages = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= numberOfPages; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i.ToString())),
                    559, PageSize.A4.GetBottom() + 20, i, TextAlignment.RIGHT,//806
                    VerticalAlignment.BOTTOM, 0);
            }
            document.Close();
            return this;
        }

        public PdfManager GenerateDealsReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            DealService _dealService = new DealService();
            FileInfo file = new FileInfo(pdfDestination);
            var fontDestination = System.IO.Path.Combine(targetFolder, "times.ttf");
            //document settings
            pdfDoc = new PdfDocument(new PdfWriter(pdfDestination));
            pdfDoc.SetDefaultPageSize(PageSize.A4);
            document = new Document(pdfDoc, PageSize.A4, false).SetFontSize(12);
            //set header
            PdfFont font = PdfFontFactory.CreateFont(fontDestination, PdfEncodings.IDENTITY_H, true);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            Paragraph docHeader = new Paragraph("Deals Report").SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(headerFontSize).SetFont(bold);
            //set logo
            Image logo = new Image(ImageDataFactory
            .Create(logoFile))
            .SetTextAlignment(TextAlignment.LEFT).SetHeight(60).SetWidth(60);

            document.Add(logo);
            document.Add(docHeader);

            if (fromDate != null && toDate != null)
            {
                var fromTo = new Paragraph("From " + fromDate.Value.ToString("D") + " to " + toDate.Value.ToString("D")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(30);
                document.Add(fromTo);
            }

            var currentItem = 1;
            Table table = new Table(UnitValue.CreatePercentArray(9), false).SetFont(font);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetFixedLayout();

            List<string> headers = new List<string> { "No.", "Deal's Name", "Expected Closing Date", "Actual Closing Date", "Created Date", "Amount", "Stage", "Customer's Name", "Owner" };

            foreach(var header in headers)
            {
                var newHeaderCell = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph(header));
                table.AddHeaderCell(newHeaderCell);
            }

            var dealList = _dealService.GetDealList(pageSize: 99999, currentPage: 1, sort: new List<string> { "asc.name" });

            dealList.deals = dealList.deals.Where(c => c.createdAt.CompareTo(new DateTime(1, 1, 1)) != 0 || c.endOn.CompareTo(new DateTime(1, 1, 1)) != 0 || c.expectedDate.CompareTo(new DateTime(1, 1, 1)) != 0).ToList();
            if(fromDate != null && toDate != null)
            {
                dealList.deals = dealList.deals.Where(c => (c.createdAt >= fromDate && c.createdAt <= toDate) || (c.endOn >= fromDate && c.endOn <= toDate) || (c.expectedDate >= fromDate && c.expectedDate <= toDate)).ToList();
            }

            foreach (var deal in dealList.deals)
            {
                var x = deal.expectedDate.CompareTo(new DateTime(1, 1, 1)) != 0;
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(currentItem.ToString())));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.name ?? "")));

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.expectedDate.CompareTo(new DateTime(1, 1, 1)) != 0 ? deal.expectedDate.ToString("dd'/'MM'/'yyyy") : "")));

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.endOn.CompareTo(new DateTime(1, 1, 1)) != 0 ? deal.endOn.ToString("dd'/'MM'/'yyyy") : "")));

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.createdAt.CompareTo(new DateTime(1, 1, 1)) != 0 ? deal.createdAt.ToString("dd'/'MM'/'yyyy") : "")));

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.amount.ToString() ?? "" )));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.stage ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.accountName ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.owner ?? "")));
                currentItem++;
            }

            document.Add(table);

            document.Add(new Paragraph(new Text("\n")));

            var dateSignature = new Paragraph("Hanoi, " + DateTime.Now.ToString("D")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(30);
            document.Add(dateSignature);
            document.Add(new Paragraph(new Text("\n\n")));
            document.Add(new Paragraph(new Text("Signature: ________________________")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(0));


            int numberOfPages = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= numberOfPages; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i.ToString())),
                    559, PageSize.A4.GetBottom() + 20, i, TextAlignment.RIGHT,//806
                    VerticalAlignment.BOTTOM, 0);
            }
            document.Close();
            return this;
        }

        public PdfManager GenerateRevenueReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            DealService _dealService = new DealService();
            FileInfo file = new FileInfo(pdfDestination);
            var fontDestination = System.IO.Path.Combine(targetFolder, "times.ttf");
            //document settings
            pdfDoc = new PdfDocument(new PdfWriter(pdfDestination));
            pdfDoc.SetDefaultPageSize(PageSize.A4);
            document = new Document(pdfDoc, PageSize.A4, false).SetFontSize(12);
            //set header
            PdfFont font = PdfFontFactory.CreateFont(fontDestination, PdfEncodings.IDENTITY_H, true);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            Paragraph docHeader = new Paragraph("Revenue Report").SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(headerFontSize).SetFont(bold);
            //set logo
            Image logo = new Image(ImageDataFactory
            .Create(logoFile))
            .SetTextAlignment(TextAlignment.LEFT).SetHeight(60).SetWidth(60);

            document.Add(logo);
            document.Add(docHeader);

            if (fromDate != null && toDate != null)
            {
                var fromTo = new Paragraph("From " + fromDate.Value.ToString("D") + " to " + toDate.Value.ToString("D")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(30);
                document.Add(fromTo);
            }

            var currentItem = 1;
            Table table = new Table(UnitValue.CreatePercentArray(6), false).SetFont(font);
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.SetFixedLayout();

            List<string> headers = new List<string> { "No.", "Deal's Name", "Closing Date", "Amount", "Customer's Name", "Owner" };

            foreach (var header in headers)
            {
                var newHeaderCell = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK).Add(new Paragraph(header));
                table.AddHeaderCell(newHeaderCell);
            }

            var dealList = _dealService.GetDealList(pageSize: 99999, currentPage: 1, sort: new List<string> { "asc.name" });

            dealList.deals = dealList.deals.Where(c => c.createdAt.CompareTo(new DateTime(1, 1, 1)) != 0 || c.endOn.CompareTo(new DateTime(1, 1, 1)) != 0 || c.expectedDate.CompareTo(new DateTime(1, 1, 1)) != 0).ToList();
            if (fromDate != null && toDate != null)
            {
                dealList.deals = dealList.deals.Where(c => (c.endOn >= fromDate && c.endOn <= toDate) && (c.stage == "Won")).ToList();
            }

            foreach (var deal in dealList.deals)
            {
                var x = deal.expectedDate.CompareTo(new DateTime(1, 1, 1)) != 0;
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(currentItem.ToString())));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.name ?? "")));

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.endOn.CompareTo(new DateTime(1, 1, 1)) != 0 ? deal.endOn.ToString("dd'/'MM'/'yyyy") : "")));

                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.amount.ToString() ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.accountName ?? "")));
                table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph(deal.owner ?? "")));
                currentItem++;
            }
            long totalAmount = dealList.deals.Aggregate((long) 0 , (total, next) => total += next.amount, res => res);

            table.AddCell(new Cell(1, 3).SetTextAlignment(TextAlignment.CENTER).SetFontSize(fontSize).Add(new Paragraph("Sum")));
            table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetBorderRight(Border.NO_BORDER).SetFontSize(fontSize).Add(new Paragraph(totalAmount.ToString())));
            table.AddCell(new Cell(1, 2).SetTextAlignment(TextAlignment.CENTER).SetBorderLeft(Border.NO_BORDER).SetFontSize(fontSize).Add(new Paragraph("")));
            document.Add(table);

            document.Add(new Paragraph(new Text("\n")));

            var dateSignature = new Paragraph("Hanoi, " + DateTime.Now.ToString("D")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(30);
            document.Add(dateSignature);
            document.Add(new Paragraph(new Text("\n\n")));
            document.Add(new Paragraph(new Text("Signature: ________________________")).SetTextAlignment(TextAlignment.RIGHT).SetMarginRight(0));


            int numberOfPages = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= numberOfPages; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i.ToString())),
                    559, PageSize.A4.GetBottom() + 20, i, TextAlignment.RIGHT,//806
                    VerticalAlignment.BOTTOM, 0);
            }
            document.Close();
            return this;
        }
    }
}