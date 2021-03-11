using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Contact
    {
        public int ID { get; set; }

        public int? ContactOwner { get; set; }

        public int? ContactCollaborator { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string DepartmentName { get; set; }

        public DateTime? Birthday { get; set; }

        public string AssistantName { get; set; }

        public string AssistantPhone { get; set; }

        public bool? NoEmail { get; set; }

        public bool? NoCall { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? ConvertFrom { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? PRIORITY_ID { get; set; }

        public int? ACCOUNT_ID { get; set; }

        public string Skype { get; set; }
    }
}