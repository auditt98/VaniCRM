using Backend.Domain;
using Backend.Models.ApiModel;
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

        public List<ContactListApiModel.ContactInfo> GetContactList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbContacts = _contactRepository.GetAllContacts(query, pageSize, currentPage);
            return dbContacts.Select(c => new ContactListApiModel.ContactInfo() { id = c.ID, contactName = c.Name, accountName = c.ACCOUNT.Name, email = c.Email, phone = c.Phone, owner = c.Owner.FirstName + " " + c.Owner.LastName }).ToList();
        }
    }
}