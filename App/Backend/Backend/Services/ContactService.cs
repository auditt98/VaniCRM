﻿using Backend.Domain;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class ContactService
    {
        ContactRepository _contactRepository = new ContactRepository();


        public List<CONTACT> GetUserContacts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _contactRepository.GetUserContacts(userID, q, currentPage, pageSize).ToList();

        }

    }
}