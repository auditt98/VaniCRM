using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class DashboardApiModel
    {
        public S qualified { get; set; }
        public S valueProposition { get; set; }
        public S findKeyContacts { get; set; }
        public S sendProposal { get; set; }
        public S review { get; set; }
        public S negotiate { get; set; }
        public S won { get; set; }
        public S lost { get; set; }

        public DashboardApiModel()
        {
            qualified = new S();
            valueProposition = new S();
            findKeyContacts = new S();
            sendProposal = new S();
            review = new S();
            negotiate = new S();
            won = new S();
            lost = new S();
        }


        public class S
        {
            public int stageID { get; set; }
            public string stageName { get; set; }
            public int probability { get; set; }
            public List<D> deals { get; set; }
            public S()
            {
                deals = new List<D>();
            }
        }

        public class D
        {
            public int dealID { get; set; }
            public string dealName { get; set; }
            public int accountID { get; set; }
            public string accountName { get; set; }
            public int ownerID { get; set; }
            public string ownerUsername { get; set; }
            public List<T> tags { get; set; }

            public D()
            {
                tags = new List<T>();
            }

        }

        public class T
        {
            public int tagID { get; set; }
            public string tagName { get; set; }
        }

    }
}