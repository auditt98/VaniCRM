using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class UserApiModel
    {

    }

    public class ChangePasswordApiModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class UserDetailApiModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string skype { get; set; }
        public string phone { get; set; }
        public DateTime createdAt { get; set; }
        public string createdByName { get; set; }
        public int createdById { get; set; }
        public List<G> groups { get; set; }


        public class G
        {
            public int groupId { get; set; }
            public string groupName { get; set; }
            public bool selected { get; set; }

            public G() { }

            public G(int id, string name, bool selected)
            {
                this.groupId = id;
                this.groupName = name;
                this.selected = selected;
            }
        }

    }

}