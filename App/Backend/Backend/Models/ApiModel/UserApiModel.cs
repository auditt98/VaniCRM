using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class UserApiModel
    {
        public class BasicInfo
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string skype { get; set; }
            public string phone { get; set; }
            public DateTime createdAt { get; set; }
            public string createdByName { get; set; }
            public string createdByID { get; set; }

        }



    }

    public class ChangePasswordApiModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

}