//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class User
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Avatar { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Hash { get; set; }
        [JsonRequired]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public int? CreatedBy { get; set; }
        public int? GROUP_ID { get; set; }
        public string Password { get; set; }

    }

}