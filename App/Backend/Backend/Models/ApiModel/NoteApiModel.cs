using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class NoteApiModel
    {
        public int id { get; set; }
        public UserLinkApiModel createdBy { get; set; }
        public DateTime createdAt { get; set; }
        public string body { get; set; }
        public int account { get; set; }
        public int contact { get; set; }
        public int lead { get; set; }
        public int deal { get; set; }
        public int campaign { get; set; }
        public int taskTemplate { get; set; }
        public List<FileApiModel> files { get; set; }
    }
}