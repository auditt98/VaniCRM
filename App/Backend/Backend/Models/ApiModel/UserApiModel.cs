﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{

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
        //public List<A> accounts

        public class G
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool selected { get; set; }

            public G() { }

            public G(int id, string name, bool selected)
            {
                this.id = id;
                this.name = name;
                this.selected = selected;
            }
        }

        public class C
        {
            public string name { get; set; }
            public int id { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string mobile { get; set; }
            public string skype { get; set; }
            public bool isOwner { get; set; }
            public bool isCollaborator { get; set; }

            public C() { }
        }

        public class A
        {
            public string name { get; set; }
            public int id { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string website { get; set; }
            public string taxCode { get; set; }
            public bool isOwner { get; set; }
            public bool isCollaborator { get; set; }

            public A() { }
        }

        public class L
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public string companyName { get; set; }
            public string skype { get; set; }
        }

    }

}