using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Avatar { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string RememberMeToken { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? GROUP_ID { get; set; }
    }
}