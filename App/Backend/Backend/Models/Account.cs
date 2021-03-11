using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Account
    {
        public int ID { get; set; }

        public int? AccountOwner { get; set; }

        public int? AccountCollaborator { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Website { get; set; }

        public int? ACCOUNT_TYPE_ID { get; set; }

        public int? INDUSTRY_ID { get; set; }

        public string TaxCode { get; set; }

        public int? NoEmployees { get; set; }

        public long? AnnualRevenue { get; set; }

        public string BankName { get; set; }

        public string BankAccountName { get; set; }

        public string BankAccount { get; set; }

        public string Avatar { get; set; }

        public string Email { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? ConvertFrom { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}