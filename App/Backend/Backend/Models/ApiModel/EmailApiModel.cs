using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class EmailApiModel
    {
        public string title { get; set; }
        public string content { get; set; }
        public List<EmailRecipient> recipients { get; set; }

    }
}